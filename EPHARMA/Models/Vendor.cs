using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }
        [Required]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }
        public string VendorNumber { get; set; }
        public bool IsApproved { get; set; } = false;
        public string VendorUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string VendorCNIC { get; set; }
        public string VendorProfilePicture { get; set; }
        public string VendorEmail { get; set; }
        public bool Status { get; set; } = true;
        public DateTime Date { get; set; } = getDate();
        public static DateTime getDate()
        {
            DateTime date1 = DateTime.UtcNow;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            DateTime date2 = TimeZoneInfo.ConvertTime(date1, tz);
            return date2;
        }

        internal IEnumerable<object> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
