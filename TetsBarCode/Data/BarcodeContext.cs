using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TetsBarCode.Model;

namespace TetsBarCode.Data
{
    public class BarcodeContext : DbContext
    {
        public BarcodeContext(DbContextOptions<BarcodeContext> options) : base(options) { }
        public DbSet<BarcodeModel> Barcodes { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasKey(e => e.ErrorID);   // 👈 บังคับ EF ให้รู้ว่า ErrorID คือ PK
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
