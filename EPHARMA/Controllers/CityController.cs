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
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private City _city;
        public CityController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            var AllCities = _context.Cities.Where(x => x.Status);
            return View(AllCities);
        }
        public IActionResult Create(int id)
        {
            ViewBag.CountryId = new SelectList(_context.Countries.Where(x => x.Status), "CountryId", "CountryName");
            if (id == 0)
            {
                _city = new City();
            }
            else
            {
                _city = _context.Cities.Find(id);
            }
            return View(_city);
        }
        [HttpPost]
        public IActionResult Create(City NewCity)
        {
            if (NewCity.CityId == 0)
            {
                _context.Cities.Add(NewCity);
            }
            else
            {
                _context.Entry(NewCity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
           var city = _context.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }
            city.Status = false;
            _context.Entry(city).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
