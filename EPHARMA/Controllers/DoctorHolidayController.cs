using EPHARMA.Data;
using EPHARMA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Controllers
{
    public class DoctorHolidayController : Controller
    {
        private readonly ApplicationDbContext _context;
        private DoctorHoliday _doctorHolyday;
        public DoctorHolidayController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            var doctorHoliday = _context.DoctorHolidays.Where(x => x.Status);
            return View(doctorHoliday);
        }
        public IActionResult Create(int id)
        {
            ViewBag.DoctorId = new SelectList(_context.Doctors.Where(x => x.Status), "DoctorId", "DoctorName");
            ViewBag.CustomerId = new SelectList(_context.Customers.Where(x => x.Status), "CustomerId", "CustomerName");
            if (id == 0)
            {
                _doctorHolyday = new DoctorHoliday();
            }
            else
            {
                _doctorHolyday = _context.DoctorHolidays.Find(id);
            }
            return View(_doctorHolyday);
        }
        [HttpPost]
        public IActionResult Create(DoctorHoliday doctorHoliday)
        {
            if (doctorHoliday.DoctorHolidayId == 0)
            {
                _context.DoctorHolidays.Add(doctorHoliday);
            }
            else
            {
                _context.Entry(doctorHoliday).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var doctorHoliday = _context.DoctorHolidays.Find(id);
            if (doctorHoliday == null)
            {
                return NotFound();
            }
            doctorHoliday.Status = false;
            _context.Entry(doctorHoliday).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
