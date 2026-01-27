/* =========================================================
   SUBSCRIPTION PLANS MIGRATION
   Bảng mức giá các gói đăng ký theo tháng
   ========================================================= */

USE [LearningPlatform];
GO

-- Kiểm tra và xóa bảng cũ nếu tồn tại (chỉ dùng khi development)
-- IF OBJECT_ID('dbo.SubscriptionPlans', 'U') IS NOT NULL
--     DROP TABLE dbo.SubscriptionPlans;
-- GO

/* =========================================================
   Tạo bảng SubscriptionPlans
   ========================================================= */
CREATE TABLE dbo.SubscriptionPlans (
    PlanId           INT IDENTITY(1,1) PRIMARY KEY,
    DurationMonths   INT            NOT NULL,
    Price            DECIMAL(12,2)  NOT NULL,
    
    CONSTRAINT CK_SubscriptionPlans_Duration CHECK (DurationMonths > 0),
    CONSTRAINT CK_SubscriptionPlans_Price CHECK (Price >= 0)
);
GO

-- Tạo Index
CREATE INDEX IX_SubscriptionPlans_Duration 
    ON dbo.SubscriptionPlans(DurationMonths);
GO

/* =========================================================
   Thêm dữ liệu mặc định
   ========================================================= */
INSERT INTO dbo.SubscriptionPlans (DurationMonths, Price)
VALUES 
    (1, 249000),      -- 1 tháng: 249,000đ
    (6, 1349000),     -- 6 tháng: 1,349,000đ
    (12, 2390000);    -- 1 năm: 2,390,000đ
GO

/* =========================================================
   Kết thúc migration
   ========================================================= */
PRINT 'SubscriptionPlans migration completed successfully!';
GO

-- Hiển thị dữ liệu
SELECT 
    PlanId,
    DurationMonths AS 'Thời hạn (tháng)',
    FORMAT(Price, 'N0') + 'đ' AS 'Giá tiền'
FROM dbo.SubscriptionPlans
ORDER BY DurationMonths;
GO
