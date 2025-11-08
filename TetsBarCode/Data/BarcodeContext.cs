using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TetsBarCode.Model;

namespace TetsBarCode.Data
{
    public class BarcodeContext : DbContext
    {
        public BarcodeContext(DbContextOptions<BarcodeContext> options) : base(options) { }
        public DbSet<BarcodeModel> Barcodes { get; set; }
    }
}
