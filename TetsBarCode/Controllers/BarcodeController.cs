using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TetsBarCode.Data;
using TetsBarCode.Model;

namespace TetsBarCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodeController : ControllerBase  // ✅ ต้องลงท้ายด้วย ControllerBase
    {
        private readonly BarcodeContext _context;

        public BarcodeController(BarcodeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBarcodes()
        {
            var list = await _context.Barcodes.ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddBarcode([FromBody] BarcodeModel barcode)
        {
            if (barcode == null || string.IsNullOrWhiteSpace(barcode.Code))
                return BadRequest("Invalid barcode.");

            _context.Barcodes.Add(barcode);
            await _context.SaveChangesAsync();
            return Ok(barcode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarcode(int id)
        {
            var barcode = await _context.Barcodes.FindAsync(id);
            if (barcode == null)
                return NotFound();

            _context.Barcodes.Remove(barcode);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
