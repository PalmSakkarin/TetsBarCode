# 📦 BarcodeDB & Angular Frontend Setup

โปรเจกต์นี้ประกอบด้วย Script สำหรับสร้างฐานข้อมูล `BarcodeDB` และส่วน Frontend ที่พัฒนาโดยใช้ **Angular**  Backend  ที่พัฒนาโดยใช้ **C#**

---

## 🧩 Database Script (SQL Server)

## 🔹 ScriptSQL `Barcodes`

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




