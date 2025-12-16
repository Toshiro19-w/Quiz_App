/* =========================================================
   DISCOUNT SYSTEM - SQL Server DDL
   Hệ thống mã giảm giá/voucher
   ========================================================= */

USE [LearningPlatform];
GO

-- =====================================================================
-- 1) DISCOUNTS TABLE - Bảng mã giảm giá
-- =====================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Discounts')
BEGIN
    CREATE TABLE dbo.Discounts (
        DiscountId          INT IDENTITY(1,1) PRIMARY KEY,
        Code                VARCHAR(50)    NOT NULL,
        Name                NVARCHAR(200)  NOT NULL,
        Description         NVARCHAR(500)  NULL,
        DiscountType        VARCHAR(20)    NOT NULL DEFAULT 'Percentage', -- Percentage / FixedAmount
        DiscountValue       DECIMAL(12,2)  NOT NULL,
        MinOrderAmount      DECIMAL(12,2)  NULL,
        MaxDiscountAmount   DECIMAL(12,2)  NULL,
        MaxUsageCount       INT            NULL, -- NULL = unlimited
        UsedCount           INT            NOT NULL DEFAULT 0,
        MaxUsagePerUser     INT            NULL, -- NULL = unlimited per user
        StartDate           DATETIME2(7)   NOT NULL,
        EndDate             DATETIME2(7)   NOT NULL,
        Status              VARCHAR(20)    NOT NULL DEFAULT 'Active', -- Active/Inactive/Expired
        ApplyToAllCourses   BIT            NOT NULL DEFAULT 1,
        CreatedBy           INT            NOT NULL,
        CreatedAt           DATETIME2(7)   NOT NULL DEFAULT SYSUTCDATETIME(),
        UpdatedAt           DATETIME2(7)   NULL,
        
        CONSTRAINT UQ_Discounts_Code UNIQUE (Code),
        CONSTRAINT CK_Discounts_Type CHECK (DiscountType IN ('Percentage', 'FixedAmount')),
        CONSTRAINT CK_Discounts_Status CHECK (Status IN ('Active', 'Inactive', 'Expired')),
        CONSTRAINT CK_Discounts_Value CHECK (DiscountValue > 0),
        CONSTRAINT CK_Discounts_Dates CHECK (EndDate > StartDate),
        CONSTRAINT FK_Discounts_Creator FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserId)
    );
    
    CREATE INDEX IX_Discounts_Code ON dbo.Discounts(Code);
    CREATE INDEX IX_Discounts_Status ON dbo.Discounts(Status);
    CREATE INDEX IX_Discounts_Dates ON dbo.Discounts(StartDate, EndDate);
    
    PRINT 'Created table: Discounts';
END
GO

-- =====================================================================
-- 2) DISCOUNT_USAGES TABLE - Lịch sử sử dụng mã giảm giá
-- =====================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DiscountUsages')
BEGIN
    CREATE TABLE dbo.DiscountUsages (
        UsageId         INT IDENTITY(1,1) PRIMARY KEY,
        DiscountId      INT            NOT NULL,
        UserId          INT            NOT NULL,
        OrderId         INT            NOT NULL,
        DiscountAmount  DECIMAL(12,2)  NOT NULL,
        UsedAt          DATETIME2(7)   NOT NULL DEFAULT SYSUTCDATETIME(),
        
        CONSTRAINT FK_DiscountUsage_Discount FOREIGN KEY (DiscountId) REFERENCES dbo.Discounts(DiscountId),
        CONSTRAINT FK_DiscountUsage_User FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId),
        CONSTRAINT FK_DiscountUsage_Order FOREIGN KEY (OrderId) REFERENCES dbo.Orders(OrderId)
    );
    
    CREATE INDEX IX_DiscountUsages_Discount ON dbo.DiscountUsages(DiscountId);
    CREATE INDEX IX_DiscountUsages_User ON dbo.DiscountUsages(UserId);
    CREATE INDEX IX_DiscountUsages_Order ON dbo.DiscountUsages(OrderId);
    
    PRINT 'Created table: DiscountUsages';
END
GO

-- =====================================================================
-- 3) DISCOUNT_COURSES TABLE - Mã giảm giá áp dụng cho khóa học cụ thể
-- =====================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DiscountCourses')
BEGIN
    CREATE TABLE dbo.DiscountCourses (
        DiscountCourseId INT IDENTITY(1,1) PRIMARY KEY,
        DiscountId       INT NOT NULL,
        CourseId         INT NOT NULL,
        
        CONSTRAINT UQ_DiscountCourse UNIQUE (DiscountId, CourseId),
        CONSTRAINT FK_DiscountCourse_Discount FOREIGN KEY (DiscountId) REFERENCES dbo.Discounts(DiscountId) ON DELETE CASCADE,
        CONSTRAINT FK_DiscountCourse_Course FOREIGN KEY (CourseId) REFERENCES dbo.Courses(CourseId) ON DELETE CASCADE
    );
    
    CREATE INDEX IX_DiscountCourses_Discount ON dbo.DiscountCourses(DiscountId);
    CREATE INDEX IX_DiscountCourses_Course ON dbo.DiscountCourses(CourseId);
    
    PRINT 'Created table: DiscountCourses';
END
GO

-- =====================================================================
-- 4) ADD COLUMNS TO ORDERS TABLE
-- =====================================================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Orders') AND name = 'OriginalAmount')
BEGIN
    ALTER TABLE dbo.Orders ADD OriginalAmount DECIMAL(12,2) NULL;
    PRINT 'Added column: Orders.OriginalAmount';
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Orders') AND name = 'DiscountAmount')
BEGIN
    ALTER TABLE dbo.Orders ADD DiscountAmount DECIMAL(12,2) NULL;
    PRINT 'Added column: Orders.DiscountAmount';
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Orders') AND name = 'DiscountId')
BEGIN
    ALTER TABLE dbo.Orders ADD DiscountId INT NULL;
    ALTER TABLE dbo.Orders ADD CONSTRAINT FK_Orders_Discount 
        FOREIGN KEY (DiscountId) REFERENCES dbo.Discounts(DiscountId) ON DELETE SET NULL;
    PRINT 'Added column: Orders.DiscountId with FK';
END
GO

-- =====================================================================
-- 5) SEED DATA - Mã giảm giá mẫu
-- =====================================================================
PRINT 'Seeding sample discount codes...';

-- Mã giảm 10% cho đơn hàng đầu tiên
IF NOT EXISTS (SELECT 1 FROM dbo.Discounts WHERE Code = 'WELCOME10')
BEGIN
    INSERT INTO dbo.Discounts (Code, Name, Description, DiscountType, DiscountValue, MaxUsagePerUser, StartDate, EndDate, CreatedBy)
    VALUES ('WELCOME10', N'Chào mừng học viên mới', N'Giảm 10% cho đơn hàng đầu tiên', 'Percentage', 10, 1, 
            DATEADD(DAY, -1, SYSUTCDATETIME()), DATEADD(YEAR, 1, SYSUTCDATETIME()), 1);
    PRINT 'Created discount: WELCOME10';
END

-- Mã giảm 20% tối đa 100k
IF NOT EXISTS (SELECT 1 FROM dbo.Discounts WHERE Code = 'SALE20')
BEGIN
    INSERT INTO dbo.Discounts (Code, Name, Description, DiscountType, DiscountValue, MaxDiscountAmount, MinOrderAmount, StartDate, EndDate, CreatedBy)
    VALUES ('SALE20', N'Flash Sale 20%', N'Giảm 20% tối đa 100.000đ cho đơn từ 300.000đ', 'Percentage', 20, 100000, 300000,
            DATEADD(DAY, -1, SYSUTCDATETIME()), DATEADD(MONTH, 1, SYSUTCDATETIME()), 1);
    PRINT 'Created discount: SALE20';
END

-- Mã giảm 50k cố định
IF NOT EXISTS (SELECT 1 FROM dbo.Discounts WHERE Code = 'GIAM50K')
BEGIN
    INSERT INTO dbo.Discounts (Code, Name, Description, DiscountType, DiscountValue, MinOrderAmount, MaxUsageCount, StartDate, EndDate, CreatedBy)
    VALUES ('GIAM50K', N'Giảm 50.000đ', N'Giảm 50.000đ cho đơn từ 200.000đ, giới hạn 100 lượt', 'FixedAmount', 50000, 200000, 100,
            DATEADD(DAY, -1, SYSUTCDATETIME()), DATEADD(MONTH, 3, SYSUTCDATETIME()), 1);
    PRINT 'Created discount: GIAM50K';
END

-- Mã giảm VIP 30%
IF NOT EXISTS (SELECT 1 FROM dbo.Discounts WHERE Code = 'VIP30')
BEGIN
    INSERT INTO dbo.Discounts (Code, Name, Description, DiscountType, DiscountValue, MaxDiscountAmount, MinOrderAmount, StartDate, EndDate, CreatedBy)
    VALUES ('VIP30', N'Ưu đãi VIP 30%', N'Giảm 30% tối đa 200.000đ cho đơn từ 500.000đ', 'Percentage', 30, 200000, 500000,
            DATEADD(DAY, -1, SYSUTCDATETIME()), DATEADD(MONTH, 6, SYSUTCDATETIME()), 1);
    PRINT 'Created discount: VIP30';
END

GO

-- =====================================================================
-- SUMMARY
-- =====================================================================
PRINT '';
PRINT '========================================';
PRINT 'DISCOUNT SYSTEM MIGRATION COMPLETED!';
PRINT '========================================';
PRINT '';

SELECT 'Discounts' AS TableName, COUNT(*) AS RecordCount FROM dbo.Discounts;
GO

PRINT '';
PRINT '=== SAMPLE DISCOUNT CODES ===';
SELECT Code, Name, DiscountType, DiscountValue, 
       CASE WHEN DiscountType = 'Percentage' THEN CONCAT(CAST(DiscountValue AS VARCHAR), '%')
            ELSE CONCAT(FORMAT(DiscountValue, 'N0'), 'đ') END AS DisplayValue,
       Status
FROM dbo.Discounts;
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Orders') AND name = 'OriginalAmount')
BEGIN
    ALTER TABLE dbo.Orders
    ADD OriginalAmount DECIMAL(12, 2) NULL;
    PRINT 'Added OriginalAmount column to Orders';
END
GO

-- Check and add DiscountAmount column
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Orders') AND name = 'DiscountAmount')
BEGIN
    ALTER TABLE dbo.Orders
    ADD DiscountAmount DECIMAL(12, 2) NULL;
    PRINT 'Added DiscountAmount column to Orders';
END
GO

-- Check and add DiscountId column
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Orders') AND name = 'DiscountId')
BEGIN
    ALTER TABLE dbo.Orders
    ADD DiscountId INT NULL;
    PRINT 'Added DiscountId column to Orders';
END
GO
