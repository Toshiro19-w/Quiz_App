/* =========================================================
   USER SUBSCRIPTION MIGRATION
   Thêm bảng UserSubscription để quản lý đăng ký dịch vụ theo tháng
   ========================================================= */

USE [LearningPlatform];
GO

-- Kiểm tra và xóa bảng cũ nếu tồn tại (chỉ dùng khi development)
-- IF OBJECT_ID('dbo.UserSubscriptions', 'U') IS NOT NULL
--     DROP TABLE dbo.UserSubscriptions;
-- GO

/* =========================================================
   Tạo bảng UserSubscriptions
   ========================================================= */
CREATE TABLE dbo.UserSubscriptions (
    SubscriptionId    INT IDENTITY(1,1) PRIMARY KEY,
    UserId            INT            NOT NULL,
    Status            VARCHAR(20)    NOT NULL,  -- Active, Expired, Cancelled, Suspended
    SubscribedAt      DATETIME2(7)   NOT NULL CONSTRAINT DF_UserSubscriptions_SubscribedAt DEFAULT SYSUTCDATETIME(),
    ExpiresAt         DATETIME2(7)   NOT NULL,
    
    CONSTRAINT FK_UserSubscriptions_User FOREIGN KEY (UserId) 
        REFERENCES dbo.Users(UserId) ON DELETE CASCADE,
    
    CONSTRAINT CK_UserSubscriptions_Status 
        CHECK (Status IN ('Active', 'Expired', 'Cancelled', 'Suspended')),
    
    CONSTRAINT CK_UserSubscriptions_ExpiresAt 
        CHECK (ExpiresAt > SubscribedAt)
);
GO

-- Tạo Index để tăng hiệu suất truy vấn
CREATE INDEX IX_UserSubscriptions_UserId 
    ON dbo.UserSubscriptions(UserId);
GO

CREATE INDEX IX_UserSubscriptions_Status 
    ON dbo.UserSubscriptions(Status);
GO

CREATE INDEX IX_UserSubscriptions_ExpiresAt 
    ON dbo.UserSubscriptions(ExpiresAt);
GO

-- Tạo Index cho việc tìm kiếm subscription còn hiệu lực
CREATE INDEX IX_UserSubscriptions_Active 
    ON dbo.UserSubscriptions(UserId, Status, ExpiresAt)
    WHERE Status = 'Active';
GO

/* =========================================================
   Thêm dữ liệu mẫu (Optional)
   ========================================================= */
-- INSERT INTO dbo.UserSubscriptions (UserId, Status, SubscribedAt, ExpiresAt)
-- VALUES 
--     (1, 'Active', SYSUTCDATETIME(), DATEADD(MONTH, 1, SYSUTCDATETIME())),
--     (2, 'Active', SYSUTCDATETIME(), DATEADD(MONTH, 1, SYSUTCDATETIME()));
-- GO

/* =========================================================
   Tạo Stored Procedure để kiểm tra subscription còn hiệu lực
   ========================================================= */
CREATE OR ALTER PROCEDURE dbo.sp_CheckUserSubscription
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 1
        SubscriptionId,
        UserId,
        Status,
        SubscribedAt,
        ExpiresAt,
        CASE 
            WHEN Status = 'Active' AND ExpiresAt > SYSUTCDATETIME() THEN 1
            ELSE 0
        END AS IsValid
    FROM dbo.UserSubscriptions
    WHERE UserId = @UserId
    ORDER BY ExpiresAt DESC;
END
GO

/* =========================================================
   Tạo Stored Procedure để gia hạn subscription
   ========================================================= */
CREATE OR ALTER PROCEDURE dbo.sp_RenewUserSubscription
    @UserId INT,
    @Months INT = 1,
    @Amount DECIMAL(10,2) = 99000
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CurrentExpiry DATETIME2(7);
    DECLARE @NewExpiry DATETIME2(7);
    
    -- Lấy ngày hết hạn hiện tại (nếu có subscription đang active)
    SELECT TOP 1 @CurrentExpiry = ExpiresAt
    FROM dbo.UserSubscriptions
    WHERE UserId = @UserId 
        AND Status = 'Active'
        AND ExpiresAt > SYSUTCDATETIME()
    ORDER BY ExpiresAt DESC;
    
    -- Tính ngày hết hạn mới
    IF @CurrentExpiry IS NULL
        SET @NewExpiry = DATEADD(MONTH, @Months, SYSUTCDATETIME());
    ELSE
        SET @NewExpiry = DATEADD(MONTH, @Months, @CurrentExpiry);
    
    -- Thêm subscription mới
    INSERT INTO dbo.UserSubscriptions (UserId, Status, SubscribedAt, ExpiresAt, Amount, Currency)
    VALUES (@UserId, 'Active', SYSUTCDATETIME(), @NewExpiry, @Amount, 'VND');
    
    SELECT SCOPE_IDENTITY() AS NewSubscriptionId;
END
GO

/* =========================================================
   Tạo Job tự động cập nhật status của subscription hết hạn
   (Cần SQL Server Agent hoặc có thể chạy thủ công)
   ========================================================= */
CREATE OR ALTER PROCEDURE dbo.sp_UpdateExpiredSubscriptions
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.UserSubscriptions
    SET Status = 'Expired',
        UpdatedAt = SYSUTCDATETIME()
    WHERE Status = 'Active'
        AND ExpiresAt <= SYSUTCDATETIME();
    
    SELECT @@ROWCOUNT AS UpdatedCount;
END
GO

/* =========================================================
   Kết thúc migration
   ========================================================= */
PRINT 'UserSubscription migration completed successfully!';
GO
