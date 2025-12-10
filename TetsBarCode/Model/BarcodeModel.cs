using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TetsBarCode.Model
{
    public class BarcodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class ErrorLog
    {
        [Key]
        public int ErrorID { get; set; }
        public DateTime ErrorDate { get; set; } = DateTime.Now;
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string ErrorProcedure { get; set; }
        public int? ErrorLine { get; set; }
        public int? Severity { get; set; }
        public string UserName { get; set; }
        public string InputData { get; set; }
        public string ServerName { get; set; }
        public string AppName { get; set; }
    }


}
