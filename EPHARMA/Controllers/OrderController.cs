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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Order _order;
        public OrderController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            var AllOrders = _context.Orders.Where(x => x.Status);
            return View(AllOrders);
        }
        public IActionResult Create(int id)
        {
            ViewBag.Pharmacy = new SelectList(_context.Pharmacies.Where(x => x.Status), "PharmacyId", "PharmacyName");
            ViewBag.Customer = new SelectList(_context.Customers.Where(x => x.Status), "CustomerId", "CustomerName");
            if (id == 0)
            {
                _order = new Order();
            }
            else
            {
                _order = _context.Orders.Find(id);
            }
            return View(_order);
        }
        [HttpPost]
        public IActionResult Create(Order NewOrder)
        {
            if (NewOrder.OrderId == 0)
            {
                _context.Orders.Add(NewOrder);
            }
            else
            {
                _context.Entry(NewOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = false;
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
