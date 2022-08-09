using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class OrderMedicine
    {
        [Key]
        public int OrderMedicineId { get; set; }
        [Required]
        [Display(Name = "Medicine")]
        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicines { get; set; }
        [Required]
        [Display(Name = "Order")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Orders { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; } = true;
        public DateTime Date { get; set; } = getDate();
        public static DateTime getDate()
        {
            DateTime date1 = DateTime.UtcNow;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            DateTime date2 = TimeZoneInfo.ConvertTime(date1, tz);
            return date2;
        }
    }
}
