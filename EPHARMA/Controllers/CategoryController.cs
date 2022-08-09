using EPHARMA.Data;
using EPHARMA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Category _category;
        public CategoryController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            var AllCategories = _context.Categories.Where(x => x.Status);
            return View(AllCategories);
        }
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                _category = new Category();
            }
            else
            {
                _category = _context.Categories.Find(id);
            }
            return View(_category);
        }
        [HttpPost]
        public IActionResult  Create(Category NewCategory)
        {
            if (NewCategory.CategoryId == 0)
            {
                _context.Categories.Add(NewCategory);
            }
            else
            {
                _context.Entry(NewCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Status = false;
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
