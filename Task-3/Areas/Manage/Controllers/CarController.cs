using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_3.DAL;
using Task_3.Models;

namespace Task_3.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CarController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _web;

        public CarController(AppDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _web = web;
        }

        public IActionResult Index()
        {
            List<Car> cars = _context.Cars.ToList();
            return View(cars);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid)
            {

                return View(car);
            }
            bool IsExist = _context.Cars.Any(s => s.Name.ToLower().Trim().Contains(car.Name.ToLower().Trim()));
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Name Is Exist !!!");
            };
            _context.Add(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Car car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Car car, int id)
        {
            if (car.Id != id)
            {
                return BadRequest();
            }
            Car vehicle = _context.Cars.Find(car.Id);
            if (vehicle == null)
            {
                return NotFound();
            }
            vehicle.Name = car.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Car car = _context.Cars.Find(id);
            if(car==null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
