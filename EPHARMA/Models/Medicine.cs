using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId  { get; set; }
        [Required]
        [Display(Name = "Pharmacy")]
        public int PharmacyId { get; set; }
        [ForeignKey("PharmacyId")]
        public virtual Pharmacy Pharmacies { get; set; }
        [Display(Name = "Medicine Name")]
        public string MedicineName { get; set; }
        public string Storage { get; set; }
        public string PackageSize { get; set; }
        public string KeyBenefits { get; set; }
        public string Precautions { get; set; }
        public bool AvailabilityStatus { get; set; } = true;
        public string MedicineType { get; set; }
        public string MedicineBrand { get; set; }
        public float MedicinePrice { get; set; }
        public string MedicineImage { get; set; }
        public string MedicineDescription { get; set; }
        public string MedicineIngredients { get; set; }
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
