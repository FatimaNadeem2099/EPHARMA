using EPHARMA.Data;
using EPHARMA.Models;
using EPHARMA.Services.Interface;
using EPHARMA.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Controllers
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageInterface _imageService;
        private Medicine _medicine;
        public MedicineController(ApplicationDbContext Context, IImageInterface ImageService)
        {
            _context = Context;
            _imageService = ImageService;
        }
        public IActionResult Index()
        {
            var AllMedicines = _context.Medicines.Where(x => x.Status);
            return View(AllMedicines);
        }
        public IActionResult Create(int id)
        {
            MedicineViewModel medicine_ViewModel = new MedicineViewModel();
            ViewBag.PharmacyId = new SelectList(_context.Pharmacies.Where(x => x.Status), "PharmacyId", "PharmacyName");
            ViewBag.AllCategories = new SelectList(_context.Categories.Where(x => x.Status && x.CategoryType=="Medicine"), "CategoryId", "CategoryTitle");
            if (id == 0)
            {
                medicine_ViewModel.Medicines = new Medicine();
                return View(medicine_ViewModel);
            }
            else
            {
                medicine_ViewModel.Medicines = _context.Medicines.Find(id);
                medicine_ViewModel.CategoryIds = _context.MedicineCategories.Where(x => x.MedicineId == id).Select(x => x.CategoryId).ToList();
                return View(medicine_ViewModel);
            }
        }
        [HttpPost]
        public IActionResult Create(MedicineViewModel NewMedicine, IFormFile MedicineImage)
        {
            string uniqueFileName;
            _medicine = NewMedicine.Medicines;

            if (_medicine.MedicineId == 0)
            {
                uniqueFileName = _imageService.AddImage(MedicineImage);
                NewMedicine.Medicines.MedicineImage = uniqueFileName;
                _context.Medicines.Add(_medicine);
                _context.SaveChanges();
                //Add All Medicine Category
                MedicineCategory obj;
                foreach (var category in NewMedicine.CategoryIds)
                {
                    obj = new MedicineCategory();
                    obj.CategoryId = category;
                    obj.MedicineId = _medicine.MedicineId;
                    _context.MedicineCategories.Add(obj);
                }
                _context.SaveChanges();
            }
            else //In Case of Edit 
            {
                _context.MedicineCategories.RemoveRange(_context.MedicineCategories.Where(x => x.MedicineId == NewMedicine.Medicines.MedicineId));
                _context.SaveChanges();

                MedicineCategory obj;
                foreach (var category in NewMedicine.CategoryIds)
                {
                    obj = new MedicineCategory();
                    obj.CategoryId = category;
                    obj.MedicineId = NewMedicine.Medicines.MedicineId;
                    _context.MedicineCategories.Add(obj);
                }
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pharmacy = _context.Pharmacies.Where(x => x.PharmacyId == id);
            foreach (var item in pharmacy)
            {
                item.Status = false;
                _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            var medicine = _context.Medicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }
            medicine.Status = false;
            _context.Entry(medicine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
