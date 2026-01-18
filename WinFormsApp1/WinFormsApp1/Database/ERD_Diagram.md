# MÔ HÌNH ERD - LEARNING PLATFORM

## TỔNG QUAN HỆ THỐNG
Hệ thống Learning Platform là nền tảng học tập trực tuyến với các chức năng:
- Quản lý khóa học (Course Management)
- Quản lý flashcard (Flashcard System)
- Hệ thống kiểm tra (Test/Quiz System)
- Thanh toán và đơn hàng (Payment & Order)
- Quản lý người dùng (User Management)

---

## CÁC THỰC THỂ CHÍNH (ENTITIES)

### 1. USER MANAGEMENT (Quản lý người dùng)

#### **User** (Người dùng)
- UserId (PK)
- Username
- Email
- PasswordHash
- FullName
- AvatarUrl
- Phone
- RoleId (FK → Role)
- Status
- CreatedAt
- LastLoginAt
- PasswordResetToken
- PasswordResetTokenExpiry

#### **Role** (Vai trò)
- RoleId (PK)
- Name

#### **UserProfile** (Hồ sơ người dùng)
- ProfileId (PK)
- UserId (FK → User)
- Bio
- DateOfBirth
- Gender
- Location
- Website
- SocialLinks

#### **UserSetting** (Cài đặt người dùng)
- SettingId (PK)
- UserId (FK → User)
- EmailNotifications
- PushNotifications
- Language
- Theme

#### **UserInterest** (Sở thích người dùng)
- InterestId (PK)
- UserId (FK → User)
- CategoryId (FK → CourseCategory)

---

### 2. COURSE SYSTEM (Hệ thống khóa học)

#### **Course** (Khóa học)
- CourseId (PK)
- OwnerId (FK → User)
- CategoryId (FK → CourseCategory)
- Title
- Slug
- Summary
- CoverUrl
- Price
- IsPublished
- AverageRating
- TotalReviews
- CreatedAt
- UpdatedAt

#### **CourseCategory** (Danh mục khóa học)
- CategoryId (PK)
- Name
- Slug
- Description
- ParentId (FK → CourseCategory)
- IconUrl

#### **CourseChapter** (Chương học)
- ChapterId (PK)
- CourseId (FK → Course)
- Title
- Description
- OrderIndex

#### **Lesson** (Bài học)
- LessonId (PK)
- ChapterId (FK → CourseChapter)
- Title
- Description
- OrderIndex
- Visibility
- CreatedAt
- UpdatedAt

#### **LessonContent** (Nội dung bài học)
- ContentId (PK)
- LessonId (FK → Lesson)
- Type (Video, Text, Quiz, Flashcard)
- Title
- ContentData
- MediaId (FK → File)
- OrderIndex
- CreatedAt
- UpdatedAt

#### **CourseProgress** (Tiến độ học tập)
- ProgressId (PK)
- UserId (FK → User)
- CourseId (FK → Course)
- LessonId (FK → Lesson)
- Status
- CompletedAt
- LastAccessedAt

#### **CourseReview** (Đánh giá khóa học)
- ReviewId (PK)
- CourseId (FK → Course)
- UserId (FK → User)
- Rating
- Comment
- CreatedAt
- UpdatedAt

#### **Certificate** (Chứng chỉ)
- CertificateId (PK)
- UserId (FK → User)
- CourseId (FK → Course)
- IssuedAt
- CertificateUrl
- VerificationCode

---

### 3. FLASHCARD SYSTEM (Hệ thống flashcard)

#### **FlashcardSet** (Bộ flashcard)
- SetId (PK)
- OwnerId (FK → User)
- Title
- Description
- Visibility (Public, Private, Course)
- CoverUrl
- TagsText
- Language
- CreatedAt
- UpdatedAt
- IsDeleted

#### **Flashcard** (Thẻ flashcard)
- CardId (PK)
- SetId (FK → FlashcardSet)
- FrontText
- BackText
- FrontMediaId (FK → File)
- BackMediaId (FK → File)
- Hint
- OrderIndex
- CreatedAt
- UpdatedAt

#### **FlashcardPracticeLog** (Lịch sử luyện tập flashcard)
- LogId (PK)
- UserId (FK → User)
- SetId (FK → FlashcardSet)
- CardId (FK → Flashcard)
- Confidence (1-5)
- PracticedAt

---

### 4. TEST/QUIZ SYSTEM (Hệ thống kiểm tra)

#### **Test** (Bài kiểm tra)
- TestId (PK)
- OwnerId (FK → User)
- Title
- Description
- Visibility
- TimeLimitSec
- MaxAttempts
- ShuffleQuestions
- ShuffleOptions
- GradingMode
- MaxScore
- OpenAt
- CloseAt
- CreatedAt
- UpdatedAt
- IsDeleted

#### **Question** (Câu hỏi)
- QuestionId (PK)
- TestId (FK → Test)
- Type (MultipleChoice, TrueFalse, ShortAnswer, Essay, Cloze, Range)
- StemText
- StemMediaId (FK → File)
- Points
- OrderIndex
- Metadata

#### **QuestionOption** (Lựa chọn câu hỏi)
- OptionId (PK)
- QuestionId (FK → Question)
- OptionText
- MediaId (FK → File)
- IsCorrect
- OrderIndex

#### **QuestionClozeBlank** (Chỗ trống điền từ)
- BlankId (PK)
- QuestionId (FK → Question)
- BlankIndex
- CorrectAnswer
- CaseSensitive

#### **QuestionRangeAnswer** (Đáp án dạng khoảng)
- RangeId (PK)
- QuestionId (FK → Question)
- MinValue
- MaxValue

#### **TestAttempt** (Lần làm bài)
- AttemptId (PK)
- TestId (FK → Test)
- UserId (FK → User)
- StartedAt
- SubmittedAt
- Score
- MaxScore
- Status

#### **AttemptAnswer** (Câu trả lời)
- AnswerId (PK)
- AttemptId (FK → TestAttempt)
- QuestionId (FK → Question)
- UserId (FK → User)
- AnswerText
- SelectedOptionId (FK → QuestionOption)
- IsCorrect
- PointsAwarded
- AnsweredAt

---

### 5. PAYMENT & ORDER SYSTEM (Hệ thống thanh toán)

#### **Order** (Đơn hàng)
- OrderId (PK)
- BuyerId (FK → User)
- TotalAmount
- Currency
- Status (Pending, Paid, Cancelled)
- CreatedAt
- PaidAt

#### **OrderItem** (Chi tiết đơn hàng)
- ItemId (PK)
- OrderId (FK → Order)
- CourseId (FK → Course)
- Price
- Currency

#### **Payment** (Thanh toán)
- PaymentId (PK)
- OrderId (FK → Order)
- Provider (MoMo, VietQR, etc.)
- ProviderRef
- Amount
- Currency
- Status
- PaidAt
- RawPayload

#### **CoursePurchase** (Mua khóa học)
- PurchaseId (PK)
- UserId (FK → User)
- CourseId (FK → Course)
- OrderId (FK → Order)
- PurchasedAt
- ExpiresAt

#### **ShoppingCart** (Giỏ hàng)
- CartId (PK)
- UserId (FK → User)
- CreatedAt
- UpdatedAt

#### **CartItem** (Sản phẩm trong giỏ)
- ItemId (PK)
- CartId (FK → ShoppingCart)
- CourseId (FK → Course)
- AddedAt

---

### 6. MEDIA & FILE SYSTEM (Hệ thống file)

#### **File** (File media)
- FileId (PK)
- UploaderId (FK → User)
- FileName
- FilePath
- FileType
- FileSize
- MimeType
- UploadedAt

#### **Library** (Thư viện)
- LibraryId (PK)
- UserId (FK → User)
- Name
- Description
- CreatedAt

#### **Folder** (Thư mục)
- FolderId (PK)
- LibraryId (FK → Library)
- ParentId (FK → Folder)
- Name
- CreatedAt

---

### 7. SOCIAL & NOTIFICATION (Mạng xã hội & thông báo)

#### **ContentShare** (Chia sẻ nội dung)
- ShareId (PK)
- UserId (FK → User)
- ContentType (Course, FlashcardSet, Test)
- ContentId
- Platform
- SharedAt

#### **ContentTag** (Tag nội dung)
- ContentTagId (PK)
- TagId (FK → Tag)
- ContentType
- ContentId

#### **Tag** (Thẻ tag)
- TagId (PK)
- Name
- Slug

#### **Notification** (Thông báo)
- NotificationId (PK)
- UserId (FK → User)
- Type
- Title
- Message
- IsRead
- CreatedAt
- ReadAt

#### **Reminder** (Nhắc nhở)
- ReminderId (PK)
- UserId (FK → User)
- Title
- Description
- RemindAt
- IsCompleted
- CreatedAt

#### **SavedItem** (Mục đã lưu)
- SavedId (PK)
- UserId (FK → User)
- ContentType
- ContentId
- SavedAt

---

### 8. SYSTEM & AUDIT (Hệ thống & kiểm toán)

#### **AuditLog** (Nhật ký kiểm toán)
- LogId (PK)
- UserId (FK → User)
- Action
- EntityType
- EntityId
- OldValues
- NewValues
- IpAddress
- UserAgent
- CreatedAt

#### **ErrorLog** (Nhật ký lỗi)
- ErrorId (PK)
- Message
- StackTrace
- Source
- Severity
- UserId (FK → User)
- CreatedAt

#### **Permission** (Quyền hạn)
- PermissionId (PK)
- Name
- Description

#### **RolePermission** (Quyền của vai trò)
- RoleId (FK → Role)
- PermissionId (FK → Permission)

---

## QUAN HỆ GIỮA CÁC THỰC THỂ

### User Relations (1:N)
- User → Course (1 user tạo nhiều course)
- User → FlashcardSet (1 user tạo nhiều flashcard set)
- User → Test (1 user tạo nhiều test)
- User → Order (1 user có nhiều đơn hàng)
- User → CourseReview (1 user viết nhiều review)
- User → TestAttempt (1 user có nhiều lần làm bài)

### Course Relations
- Course → CourseChapter (1:N)
- CourseChapter → Lesson (1:N)
- Lesson → LessonContent (1:N)
- Course → CourseReview (1:N)
- Course → CoursePurchase (1:N)

### Flashcard Relations
- FlashcardSet → Flashcard (1:N)
- FlashcardSet → FlashcardPracticeLog (1:N)

### Test Relations
- Test → Question (1:N)
- Question → QuestionOption (1:N)
- Question → QuestionClozeBlank (1:N)
- Question → QuestionRangeAnswer (1:N)
- Test → TestAttempt (1:N)
- TestAttempt → AttemptAnswer (1:N)

### Order Relations
- Order → OrderItem (1:N)
- Order → Payment (1:N)
- Order → CoursePurchase (1:N)

### Many-to-Many Relations
- User ←→ Course (qua CourseProgress)
- User ←→ Course (qua CoursePurchase)
- Role ←→ Permission (qua RolePermission)

---

## SƠ ĐỒ ERD (TEXT-BASED)

```
┌─────────────┐
│    User     │
├─────────────┤
│ UserId (PK) │───┐
│ Username    │   │
│ Email       │   │
│ RoleId (FK) │   │
└─────────────┘   │
       │          │
       │ 1:N      │ 1:N
       ↓          ↓
┌─────────────┐ ┌──────────────┐
│   Course    │ │ FlashcardSet │
├─────────────┤ ├──────────────┤
│CourseId(PK) │ │ SetId (PK)   │
│OwnerId (FK) │ │OwnerId (FK)  │
│CategoryId   │ │ Title        │
│ Title       │ │ Visibility   │
│ Price       │ └──────────────┘
└─────────────┘        │
       │               │ 1:N
       │ 1:N           ↓
       ↓         ┌──────────────┐
┌─────────────┐ │  Flashcard   │
│CourseChapter│ ├──────────────┤
├─────────────┤ │ CardId (PK)  │
│ChapterId(PK)│ │ SetId (FK)   │
│CourseId(FK) │ │ FrontText    │
│ Title       │ │ BackText     │
└─────────────┘ └──────────────┘
       │
       │ 1:N
       ↓
┌─────────────┐
│   Lesson    │
├─────────────┤
│LessonId(PK) │
│ChapterId(FK)│
│ Title       │
└─────────────┘
       │
       │ 1:N
       ↓
┌──────────────┐
│LessonContent │
├──────────────┤
│ContentId(PK) │
│LessonId (FK) │
│ Type         │
│ ContentData  │
└──────────────┘

┌─────────────┐
│    Test     │
├─────────────┤
│ TestId (PK) │
│OwnerId (FK) │
│ Title       │
│TimeLimitSec │
└─────────────┘
       │
       │ 1:N
       ↓
┌─────────────┐
│  Question   │
├─────────────┤
│QuestionId   │
│ TestId (FK) │
│ Type        │
│ StemText    │
│ Points      │
└─────────────┘
       │
       │ 1:N
       ↓
┌──────────────┐
│QuestionOption│
├──────────────┤
│ OptionId(PK) │
│QuestionId(FK)│
│ OptionText   │
│ IsCorrect    │
└──────────────┘

┌─────────────┐
│   Order     │
├─────────────┤
│ OrderId(PK) │
│BuyerId (FK) │
│TotalAmount  │
│ Status      │
└─────────────┘
       │
       ├─────┬─────┐
       │ 1:N │ 1:N │
       ↓     ↓     ↓
┌──────────┐ ┌─────────┐
│OrderItem │ │ Payment │
├──────────┤ ├─────────┤
│ItemId(PK)│ │PaymentId│
│OrderId   │ │OrderId  │
│CourseId  │ │Provider │
│ Price    │ │ Amount  │
└──────────┘ └─────────┘
```

---

## CHỨC NĂNG CHÍNH CỦA HỆ THỐNG

### 1. Quản lý người dùng
- Đăng ký, đăng nhập, quên mật khẩu
- Phân quyền theo Role (Admin, Instructor, Student)
- Quản lý profile và settings

### 2. Quản lý khóa học
- Tạo và xuất bản khóa học
- Cấu trúc: Course → Chapter → Lesson → Content
- Đánh giá và review khóa học
- Theo dõi tiến độ học tập

### 3. Hệ thống Flashcard
- Tạo và quản lý bộ flashcard
- Luyện tập với flashcard
- Theo dõi lịch sử luyện tập

### 4. Hệ thống kiểm tra
- Tạo bài test với nhiều loại câu hỏi
- Làm bài và chấm điểm tự động
- Xem kết quả và phân tích

### 5. Thanh toán
- Giỏ hàng và đặt hàng
- Tích hợp MoMo, VietQR
- Quản lý đơn hàng và lịch sử mua

### 6. Thông báo & Nhắc nhở
- Thông báo hệ thống
- Nhắc nhở học tập
- Lưu nội dung yêu thích

### 7. Báo cáo & Phân tích
- Audit log cho admin
- Error logging
- Báo cáo doanh thu và học tập
