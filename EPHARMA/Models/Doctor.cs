using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Categories { get; set; }
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string DoctorEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public int YearsOfExperience { get; set; }
        public string Services { get; set; }
        public bool OnlineStatus { get; set; } = true;
        public string CNIC { get; set; }
        public string Education { get; set; }
        public double ConsultancyCharges { get; set; }
        public string ProfilePhoto { get; set; }
      
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
