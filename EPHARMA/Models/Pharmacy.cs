using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class Pharmacy
    {
        [Key]
        public int PharmacyId { get; set; }
        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City Cities { get; set; }
        [Required]
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendors { get; set; }
        [Display(Name = "Pharmacy Name")]
        public string PharmacyName { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string Contact { get; set; }
        public bool OnlineStatus { get; set; } = true;
        public string PharmacyLocation { get; set; }
        public string PharmacyAddress { get; set; }
        [Display(Name = "Latitude")]
        public double PharmacyLatitude { get; set; }
        [Display(Name = "Longitude")]
        public double PharmacyLongitude { get; set; }
        public string CoverImage { get; set; }
        public string PharmacyLogo { get; set; }
        public string Overview { get; set; }
        public bool IsBestRated { get; set; } = true;
        public bool IsFeatured { get; set; } = true;
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
