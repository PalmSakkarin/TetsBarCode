# 📦 BarcodeDB & Angular Frontend Setup

โปรเจกต์นี้ประกอบด้วย Script สำหรับสร้างฐานข้อมูล `BarcodeDB` และส่วน Frontend ที่พัฒนาโดยใช้ **Angular**  Backend  ที่พัฒนาโดยใช้ **C#**

---

## 🧩 Database Script (SQL Server)

## 🔹 ScriptSQL `Barcodes`

```sql
CREATE TABLE [dbo].[Barcodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	  NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED ([Id] ASC),
UNIQUE NONCLUSTERED ([Code] ASC)
)
GO

ALTER TABLE [dbo].[Barcodes] 
ADD DEFAULT (GETDATE()) FOR [CreatedAt]
GO

