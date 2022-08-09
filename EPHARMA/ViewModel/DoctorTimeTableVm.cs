using EPHARMA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.ViewModel
{
    public class DoctorTimeTableVm
    {
        public int DoctorID { get; set; }
        public List<DoctorTimeTable> DoctorTimeTables { get; set; } = new List<DoctorTimeTable>();
    }
}
