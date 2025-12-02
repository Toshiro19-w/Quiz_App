-- Migration: Add Course Moderation Fields
-- Date: 2024
-- Description: Thêm các trường kiểm duyệt cho bảng Courses

-- 1. Kiểm tra và thêm các cột mới vào bảng Courses nếu chưa tồn tại
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND name = 'ModerationStatus')
BEGIN
    ALTER TABLE Courses
    ADD 
        ModerationStatus NVARCHAR(20) NOT NULL DEFAULT 'Pending',
        SubmittedForReviewAt DATETIME2 NULL,
        ReviewedBy INT NULL,
        ReviewedAt DATETIME2 NULL,
        RejectionReason NVARCHAR(MAX) NULL,
        AutoCheckResults NVARCHAR(MAX) NULL;
    
    PRINT 'Added moderation columns to Courses table';
END
ELSE
BEGIN
    PRINT 'Moderation columns already exist in Courses table';
END
GO

-- 2. Thêm khóa ngoại liên kết với Users (Reviewer) nếu chưa tồn tại
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Courses_ReviewedBy_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
BEGIN
    ALTER TABLE Courses
    ADD CONSTRAINT FK_Courses_ReviewedBy_Users 
    FOREIGN KEY (ReviewedBy) REFERENCES Users(UserId) ON DELETE SET NULL;
    
    PRINT 'Added FK_Courses_ReviewedBy_Users foreign key';
END
ELSE
BEGIN
    PRINT 'FK_Courses_ReviewedBy_Users foreign key already exists';
END
GO

-- 3. Thêm index cho tìm kiếm nhanh nếu chưa tồn tại
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_ModerationStatus' AND object_id = OBJECT_ID(N'[dbo].[Courses]'))
BEGIN
    CREATE INDEX IX_Courses_ModerationStatus ON Courses(ModerationStatus);
    PRINT 'Created IX_Courses_ModerationStatus index';
END
ELSE
BEGIN
    PRINT 'IX_Courses_ModerationStatus index already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_SubmittedForReviewAt' AND object_id = OBJECT_ID(N'[dbo].[Courses]'))
BEGIN
    CREATE INDEX IX_Courses_SubmittedForReviewAt ON Courses(SubmittedForReviewAt);
    PRINT 'Created IX_Courses_SubmittedForReviewAt index';
END
ELSE
BEGIN
    PRINT 'IX_Courses_SubmittedForReviewAt index already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_ReviewedBy' AND object_id = OBJECT_ID(N'[dbo].[Courses]'))
BEGIN
    CREATE INDEX IX_Courses_ReviewedBy ON Courses(ReviewedBy);
    PRINT 'Created IX_Courses_ReviewedBy index';
END
ELSE
BEGIN
    PRINT 'IX_Courses_ReviewedBy index already exists';
END
GO

-- 4. Thêm check constraint cho ModerationStatus nếu chưa tồn tại
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Courses_ModerationStatus' AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
BEGIN
    ALTER TABLE Courses
    ADD CONSTRAINT CK_Courses_ModerationStatus 
    CHECK (ModerationStatus IN ('Pending', 'Approved', 'Rejected', 'NeedsRevision'));
    
    PRINT 'Added CK_Courses_ModerationStatus check constraint';
END
ELSE
BEGIN
    PRINT 'CK_Courses_ModerationStatus check constraint already exists';
END
GO

-- 5. Cập nhật các khóa học hiện tại
-- Đặt các khóa học đã publish thành Approved
UPDATE Courses 
SET ModerationStatus = 'Approved'
WHERE IsPublished = 1 AND ModerationStatus = 'Pending';

PRINT 'Updated existing published courses to Approved status';
GO

-- 6. Hiển thị thống kê
SELECT 
    ModerationStatus,
    COUNT(*) as Count
FROM Courses
GROUP BY ModerationStatus
ORDER BY ModerationStatus;
GO

PRINT 'Course Moderation Migration completed successfully!';
GO

-- ==============================================================
-- ROLLBACK SCRIPT (Comment out khi chạy migration)
-- ==============================================================
/*
-- Bước 1: Xóa khóa ngoại
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Courses_ReviewedBy_Users')
BEGIN
    ALTER TABLE Courses DROP CONSTRAINT FK_Courses_ReviewedBy_Users;
    PRINT 'Dropped FK_Courses_ReviewedBy_Users';
END
GO

-- Bước 2: Xóa check constraint
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Courses_ModerationStatus')
BEGIN
    ALTER TABLE Courses DROP CONSTRAINT CK_Courses_ModerationStatus;
    PRINT 'Dropped CK_Courses_ModerationStatus';
END
GO

-- Bước 3: Xóa indexes
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_ModerationStatus')
BEGIN
    DROP INDEX IX_Courses_ModerationStatus ON Courses;
    PRINT 'Dropped IX_Courses_ModerationStatus';
END
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_SubmittedForReviewAt')
BEGIN
    DROP INDEX IX_Courses_SubmittedForReviewAt ON Courses;
    PRINT 'Dropped IX_Courses_SubmittedForReviewAt';
END
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_ReviewedBy')
BEGIN
    DROP INDEX IX_Courses_ReviewedBy ON Courses;
    PRINT 'Dropped IX_Courses_ReviewedBy';
END
GO

-- Bước 4: Xóa các cột
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND name = 'ModerationStatus')
BEGIN
    ALTER TABLE Courses DROP COLUMN ModerationStatus;
    ALTER TABLE Courses DROP COLUMN SubmittedForReviewAt;
    ALTER TABLE Courses DROP COLUMN ReviewedBy;
    ALTER TABLE Courses DROP COLUMN ReviewedAt;
    ALTER TABLE Courses DROP COLUMN RejectionReason;
    ALTER TABLE Courses DROP COLUMN AutoCheckResults;
    
    PRINT 'Dropped all moderation columns';
END
GO

PRINT 'Rollback completed successfully!';
*/
