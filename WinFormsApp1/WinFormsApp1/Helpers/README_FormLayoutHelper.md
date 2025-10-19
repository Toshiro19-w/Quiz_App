# FormLayoutHelper - H??ng d?n s? d?ng

## T?ng quan
FormLayoutHelper là m?t class static giúp qu?n lý layout và c?n ch?nh các controls trong WinForms m?t cách t? ??ng và nh?t quán.

## Các ph??ng th?c chính

### 1. C?n ch?nh Search Controls
```csharp
// S? d?ng cho form ch? có search controls (TextBox + Button)
FormLayoutHelper.SetupSearchControlsAutoCenter(this, panel1, searchTB, searchButton);
```

### 2. C?n ch?nh FlowLayoutPanels
```csharp
// C?n ch?nh m?t FlowLayoutPanel
FormLayoutHelper.SetupFlowLayoutPanelsAutoCenter(this, panelMain, new FlowLayoutPanel[] { flowPanel1, flowPanel2 });

// Ho?c c?n ch?nh th? công
FormLayoutHelper.CenterFlowLayoutPanel(panelMain, flowPanel1, keepYPosition: true);
```

### 3. Layout ??y ?? cho formHome
```csharp
// Cho form có c? search controls và FlowLayoutPanels
FormLayoutHelper.SetupCompleteAutoLayout(
    this, 
    panelMain, 
    searchTB, 
    searchButton, 
    new FlowLayoutPanel[] { flowLayoutPanel1, flowLayoutPanel2 }
);
```

### 4. Layout chuyên bi?t cho formShop
```csharp
// Cho formShop v?i search, tags, courses và labels
FormLayoutHelper.SetupShopCompleteLayout(
    this,           // form
    panel1,         // container panel
    searchTB,       // search textbox
    searchButton,   // search button
    label1,         // title label
    tagPanel,       // tag buttons panel
    coursesPanel    // courses panel
);
```

### 5. Layout chuyên bi?t cho formTest
```csharp
// Cho formTest v?i các title labels và test panels
FormLayoutHelper.SetupTestLayoutAutoCenter(
    this,           // form
    mainPanel,      // container panel
    label1,         // "Bài t?p ?ã giao"
    label2,         // "Bài t?p s?p t?i h?n"  
    label17,        // "Bài t?p quá h?n"
    AssignedPanel,  // assigned tests panel
    dueDatePanel,   // due date tests panel
    overDuePanel    // overdue tests panel
);
```

### 6. Layout tùy ch?nh cho form khác
```csharp
// C?n ch?nh b?t k? controls nào
Control[] controlsToCenter = { button1, label1, textBox1, panel2 };
FormLayoutHelper.SetupCustomLayout(this, containerPanel, controlsToCenter, horizontalCenter: true, verticalCenter: false);

// Ho?c c?n ch?nh th? công t?ng control
FormLayoutHelper.CenterControl(containerPanel, button1, horizontalCenter: true, verticalCenter: false);
```

## Các tính n?ng ??c bi?t

### Responsive Courses Panel
```csharp
// T? ??ng ?i?u ch?nh s? c?t course cards theo chi?u r?ng màn hình
FormLayoutHelper.MakeCoursePanelResponsive(coursesPanel, cardWidth: 273, minSpacing: 10);
```

### C?n ch?nh Labels
```csharp
// C?n ch?nh label v?i các tùy ch?n
FormLayoutHelper.CenterLabel(containerPanel, titleLabel, horizontalCenter: true, keepYPosition: true);
```

## Cách s? d?ng cho form m?i

### B??c 1: Import namespace
```csharp
using WinFormsApp1.Helpers;
```

### B??c 2: Trong form_Load event
```csharp
private void newForm_Load(object sender, EventArgs e)
{
    // Ch?n ph??ng th?c phù h?p v?i layout c?a form
    FormLayoutHelper.SetupSearchControlsAutoCenter(this, containerPanel, textBox, button);
    
    // Ho?c
    FormLayoutHelper.SetupCustomLayout(this, containerPanel, new Control[] { control1, control2 });
}
```

## L?i ích

1. **Gi?m code trùng l?p**: Không c?n vi?t l?i logic c?n ch?nh cho m?i form
2. **Nh?t quán**: T?t c? form có cách c?n ch?nh gi?ng nhau
3. **T? ??ng resize**: T? ??ng c?n ch?nh khi form thay ??i kích th??c
4. **D? b?o trì**: Ch? c?n s?a logic ? m?t n?i
5. **Linh ho?t**: Có th? tùy ch?nh cho t?ng tr??ng h?p c? th?
6. **Responsive**: H? tr? responsive design cho các panel ch?a nhi?u items

## Các form ?ã áp d?ng

- ? **formHome**: `SetupCompleteAutoLayout` - Search controls + FlowLayoutPanels
- ? **formSearch**: `SetupSearchControlsAutoCenter` - Ch? search controls  
- ? **formShop**: `SetupShopCompleteLayout` - Layout ph?c t?p v?i responsive courses panel
- ? **formTest**: `SetupTestLayoutAutoCenter` - Layout v?i ba sections: Bài t?p ?ã giao, S?p t?i h?n, Quá h?n

## Form ti?p theo có th? áp d?ng

- **formMenu**: Có th? s? d?ng `SetupCustomLayout` ho?c `SetupFlowLayoutPanelsAutoCenter`
- **formLesson**: Tùy thu?c vào layout, có th? dùng các ph??ng th?c có s?n
- **dangnhap**: Có th? dùng `SetupCustomLayout` ?? c?n gi?a login controls

Ch? c?n g?i ph??ng th?c phù h?p trong `form_Load` event và t?t c? logic c?n ch?nh s? ???c x? lý t? ??ng!