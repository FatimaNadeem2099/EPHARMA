using EPHARMA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.ViewModel
{
    public class MedicineViewModel
    {
        public Medicine Medicines { get; set; }

        [Required(ErrorMessage = "Please select Category Id")]
        public List<int> CategoryIds { get; set; }
        //public List<Category> Categories { get; set; }
    }
}
