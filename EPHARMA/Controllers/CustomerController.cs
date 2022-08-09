using EPHARMA.Data;
using EPHARMA.Models;
using EPHARMA.Services;
using EPHARMA.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPHARMA.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IImageInterface _imageService;
        private readonly ApplicationDbContext _context;
        private Customer _customer;
        //private readonly UserManager<IdentityUser> _userManager;
        //private string UserId;
        public CustomerController(ApplicationDbContext Context,
           IImageInterface ImageService)
        {
            _imageService = ImageService;
            _context = Context;
            //_userManager = userManager;
        }
        public IActionResult Index()
        {
            var AllCustomer = _context.Customers.Where(x => x.Status);
            return View(AllCustomer);
        }
        public IActionResult Create(int id)
        {
            ViewBag.CityId = new SelectList(_context.Cities.Where(x => x.Status), "CityId", "CityName");
            if (id == 0)
            {
                _customer = new Customer();
            }
            else
            {
                _customer = _context.Customers.Find(id);
                _customer = _context.Customers.Include(x => x.Cities).Where(x => x.CustomerId == id).FirstOrDefault();
               
            }
            return View(_customer);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer, IFormFile ProfilePhoto)
        {
            string uniqueFileName;
            if (customer.CustomerId == 0)
            {
                //var user = new IdentityUser { UserName = customer.CustomerEmail, Email = customer.CustomerEmail };
                //var result = await _userManager.CreateAsync(user, customer.Password);
                //if (!result.Succeeded)
                //{
                //    foreach (var errors in result.Errors)
                //    {
                //        ModelState.AddModelError(string.Empty, errors.Description);
                //    }
                //    return View(customer);
                //}
                uniqueFileName = _imageService.AddImage(ProfilePhoto);
                customer.ProfilePhoto = uniqueFileName;
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            else
            {
                if (ProfilePhoto != null) //Logo is edited(changed)
                {
                    _imageService.RemoveImage(customer.ProfilePhoto);
                    uniqueFileName = _imageService.AddImage(ProfilePhoto);
                    customer.ProfilePhoto = uniqueFileName;
                }
                _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Status = false;
            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
