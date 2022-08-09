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
    public class DoctorController : Controller
    {
        private readonly IImageInterface ImageService;
        private readonly ApplicationDbContext _context;
        private Doctor _doctor; 
        private readonly UserManager<IdentityUser> _userManager;
        private string Image=null;
        private RoleManager<IdentityRole> _roleManager;
        public DoctorController(ApplicationDbContext Context,
           IImageInterface ImageService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.ImageService = ImageService;
            _context = Context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var AllDoctors = _context.Doctors.Include(a => a.Categories);
            return View(AllDoctors);
        }

        public async Task<JsonResult> ChangeDoctorStatus(int id)
        {
            var doc = _context.Doctors.Find(id);
            doc.Status = !doc.Status;
            _context.SaveChanges();
            return Json("Success");
        }

        public async Task<JsonResult> ChangeDoctorOnlineStatus(int id)
        {
            var doc = _context.Doctors.Find(id);
            doc.OnlineStatus = !doc.OnlineStatus;
            _context.SaveChanges();
            return Json("Success");
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


        public IActionResult Create(int id)
        {
            ViewBag.CategoryId = new SelectList(_context.Categories.Where(x => x.Status), "CategoryId", "CategoryName");
           
            if (id == 0)
            {
                _doctor = new Doctor();
                ViewData["CategoryId"] = new SelectList(_context.Categories.Where(a => a.Status == true && a.CategoryType == "Doctor").ToList(), "CategoryId", "CategoryTitle");

            }
            else
            {
               // _doctor = _context.Doctors.Find(id);
                _doctor = _context.Doctors.Include(x => x.Categories).Where(x => x.DoctorId == id).FirstOrDefault();
                this.Image = _doctor.ProfilePhoto;
                ViewData["CategoryId"] = new SelectList(_context.Categories.Where(a => a.Status == true && a.CategoryType=="Doctor").ToList(), "CategoryId", "CategoryTitle");

            }
            return View(_doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor, IFormFile ProfilePhoto)
        {
            
            //UserId = HttpContext.Session.GetObject<string>("UserId");
            string uniqueFileName;
            if (doctor.DoctorId == 0)
            {

                var result = await MakeIdentity(doctor.DoctorEmail, doctor.Password, "Doctor");
                if (result.Succeeded != true)
                {
                    
                    return View(doctor);
                }
                    //var user = new IdentityUser { UserName = doctor.DoctorEmail, Email = doctor.DoctorEmail };
                    //var result = await _userManager.CreateAsync(user, doctor.Password);
                    //if (!result.Succeeded)
                    //{
                    //    foreach (var errors in result.Errors)
                    //    {
                    //        ModelState.AddModelError(string.Empty, errors.Description);
                    //    }
                    //    return View(doctor);
                    //}
                    //var getResult = await _userManager.AddToRoleAsync(user, "Doctors");
                    //if (!getResult.Succeeded)
                    //{
                    //    foreach (var errors in getResult.Errors)
                    //    {
                    //        ModelState.AddModelError(string.Empty, errors.Description);
                    //    }
                    //    return View(doctor);
                    //}
                    uniqueFileName = ImageService.AddImage(ProfilePhoto);
                doctor.ProfilePhoto = uniqueFileName;
               
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
            }
            else
            {
                
                if (ProfilePhoto != null) //Logo is edited(changed)
                {
                    ImageService.RemoveImage(doctor.ProfilePhoto);
                    uniqueFileName = ImageService.AddImage(ProfilePhoto);
                    doctor.ProfilePhoto = uniqueFileName;
                }
              
               
                _context.Entry(doctor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            doctor.Status = false;
            _context.Entry(doctor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
