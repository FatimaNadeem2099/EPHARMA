using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Pharmacy")]
        public int PharmacyId { get; set; }
        [ForeignKey("PharmacyId")]
        public virtual Pharmacy Pharmacies { get; set; }
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }
        public string OrderName { get; set; }
        public string OrderCode { get; set; }
        public string Location { get; set; }
        public string OrderAddress { get; set; }
        [Display(Name = "Latitude")]
        public double OrderLatitude { get; set; }
        [Display(Name = "Longitude")]
        public double OrderLongitude { get; set; }
        public string OrderStatus { get; set; }
        public bool IsFinished { get; set; } = true;
        public string Remarks { get; set; }
        public DateTime OrderDateTime { get; set; } = getDate();
        public float SubTotal { get; set; }
        public float TotalBill { get; set; }
        public string EndTime { get; set; }
        public string RecievingTime { get; set; }
        public string SpecialInstructions { get; set; }
        public int TotalQuantity { get; set; }
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
