using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }
        public string ReviewName { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public float Rating { get; set; }
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
