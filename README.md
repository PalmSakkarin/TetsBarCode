# 📦 BarcodeDB & Angular Frontend Setup

โปรเจกต์นี้ประกอบด้วย  
- 🗄️ **Script สำหรับสร้างฐานข้อมูล `BarcodeDB`**  
- 🌐 **Frontend ที่พัฒนาโดยใช้ Angular**  
- ⚙️ **Backend ที่พัฒนาโดยใช้ C# (.NET)**  

---

## 🧩 Database (SQL Server)

### 🔹 สร้างฐานข้อมูลและตาราง `Barcodes`

```sql
CREATE DATABASE BarcodeDB;
GO

USE BarcodeDB;
GO

CREATE TABLE Barcodes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(19) NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT GETDATE()
);
```


