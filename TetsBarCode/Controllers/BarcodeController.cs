using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TetsBarCode.Data;
using TetsBarCode.Model;

namespace TetsBarCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // http://localhost:5222/api/Barcode
    public class BarcodeController : ControllerBase 
    {
        private readonly BarcodeContext _context;
        private readonly ErrorLogService _logService;
        public BarcodeController(BarcodeContext context, ErrorLogService logService)
        {
            _context = context;
            _logService = logService;
        }

        [HttpGet] //Get all barcodes Intable
        public async Task<IActionResult> GetBarcodes()
        {
            var list = await _context.Barcodes.ToListAsync();
            return Ok(list);
        }

        [HttpPost] //Add a new barcode
        public async Task<IActionResult> AddBarcode([FromBody] BarcodeModel barcode)
        {
            Exception error = null;
            try
            {
                if (barcode == null || string.IsNullOrWhiteSpace(barcode.Code))
                    return BadRequest("Invalid barcode.");

                _context.Barcodes.Add(barcode);
                FuncTestVoid();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _logService.WriteLog(ex, barcode, "AddBarcode");
                return StatusCode(500, "Error occurred");
            }
            return Ok(barcode);
        }

        [HttpDelete("{id}")] //Delete a barcode by ID
        public async Task<IActionResult> DeleteBarcode(int id)
        {
            var barcode = await _context.Barcodes.FindAsync(id);
            if (barcode == null)
                return NotFound();

            _context.Barcodes.Remove(barcode);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        public void FuncTestVoid() 
        {
            string Result = string.Empty;
            string Result2 = string.Empty;
        }


    }
    public class ErrorLogService
    {
        private readonly BarcodeContext _context;

        public ErrorLogService(BarcodeContext context)
        {
            _context = context;
        }

        public async Task WriteLog(Exception ex, object data, string appName)
        {
            var log = new ErrorLog
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.ToString(),
                ErrorProcedure = ex.TargetSite?.Name,
                ErrorLine = null,
                Severity = null,
                UserName = Environment.UserName,
                InputData = data != null ? System.Text.Json.JsonSerializer.Serialize(data) : null,
                ServerName = Environment.MachineName,
                AppName = appName
            };

            _context.ErrorLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }


}
