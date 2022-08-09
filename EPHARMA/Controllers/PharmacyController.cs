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
    public class PharmacyController : Controller
    {
        private readonly IImageInterface _imageService;
        private readonly ApplicationDbContext _context;
        private Pharmacy _pharmacy;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string UserId;
        public PharmacyController(ApplicationDbContext Context,
            IImageInterface ImageService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _imageService = ImageService;
            _context = Context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var AllPharmacies = _context.Pharmacies.Where(x => x.Status);
            return View(AllPharmacies);
        }

        public async Task<IdentityResult> MakeIdentity(string Email, string Password, string Role)
        {
            try
            {

                if (_roleManager != null)
                {
                    if (!await _roleManager.RoleExistsAsync(Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role));
                    }
                }

                var user = new IdentityUser { UserName = Email, Email = Email, EmailConfirmed = true };
                //    Password = "Ali@123*";
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {

                    //_logger.LogInformation("User created a new account with password.");
                    try
                    {
                        await _userManager.AddToRoleAsync(user, Role);
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.CityId = new SelectList(_context.Cities.Where(x => x.Status), "CityId", "CityName");
            ViewBag.VendorId = new SelectList(_context.Vendors.Where(x => x.Status), "VendorId", "VendorName");
            if (id == 0)
            {
                _pharmacy = new Pharmacy();
            }
            else
            {
                _pharmacy = _context.Pharmacies.Find(id);
            }
            return View(_pharmacy);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pharmacy pharmacy, IFormFile PharmacyLogo, IFormFile CoverImage)
        {
            //UserId = HttpContext.Session.GetObject<string>("UserId");
            string uniqueFileName;
            if (pharmacy.PharmacyId == 0)
            {
                var result = await MakeIdentity(pharmacy.Vendors.VendorEmail, pharmacy.Vendors.Password, "Vendor");
                if (result.Succeeded != true)
                {

                    return View(pharmacy);
                }


                uniqueFileName = _imageService.AddImage(PharmacyLogo);
                pharmacy.PharmacyLogo = uniqueFileName;
                uniqueFileName = _imageService.AddImage(CoverImage);
                pharmacy.CoverImage = uniqueFileName;
                _context.Vendors.Add(pharmacy.Vendors);
                pharmacy.VendorId = pharmacy.Vendors.VendorId;
                _context.Pharmacies.Add(pharmacy);
                _context.SaveChanges();
            }
            else
            {
                if (PharmacyLogo != null) //Logo is edited(changed)
                {
                    _imageService.RemoveImage(pharmacy.PharmacyLogo);
                    uniqueFileName = _imageService.AddImage(PharmacyLogo);
                    pharmacy.PharmacyLogo = uniqueFileName;
                }
                if (CoverImage != null)//image is edited(changed)
                {
                    _imageService.RemoveImage(pharmacy.CoverImage);
                    uniqueFileName = _imageService.AddImage(CoverImage);
                    pharmacy.CoverImage = uniqueFileName;
                }
                _context.Entry(pharmacy).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pharmacy = _context.Pharmacies.Find(id);
            if (pharmacy == null)
            {
                return NotFound();
            }
            pharmacy.Status = false;
            _context.Entry(pharmacy).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
