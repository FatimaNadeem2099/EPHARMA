using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class AppointmentBilling
    {
        [Key]
        public int AppointmentBillingId { get; set; }
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }
        [Required]
        [Display(Name = "Appointment")]
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointments { get; set; }
        public string CardName { get; set; }
        public string CardAccountTitle { get; set; }
        public string CardBankName { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CardExpiryDate { get; set; } = getDate();
        public int CardCvvNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int Tax { get; set; }
        public bool IsConfirmed { get; set; } = true;
        public float SubTotal { get; set; }
        public float TotalBill { get; set; }
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
