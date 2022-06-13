using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Task_3.DAL;
using Task_3.Extentions;
using Task_3.Models;

namespace Task_3.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CarInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _web;

        public CarInfoController(AppDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _web = web;
        }

        public IActionResult Index()
        {
            List<CarInfo> carInfos = _context.CarInfos.ToList();
            return View(carInfos);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarInfo info)
        {
            if (!ModelState.IsValid)
            {

                return View(info);
            }
            _context.Add(info);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            CarInfo carInfo = _context.CarInfos.Find(id);
            if (carInfo == null)
            {
                return NotFound();
            }
            return View(carInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Update(CarInfo info, int id)
        {
            if (info.id != id)
            {
                return BadRequest();
            }
            CarInfo inf = _context.CarInfos.Find(info.id);
            if (inf == null)
            {
                return NotFound();
            }
            if (!info.Photos.CheckTypeImage("/image"))
            {
                ModelState.AddModelError("Photos", " File Formati düzgün deyil !");
                return View();
            }
            if (info.Photos.CheckImageLength(2))
            {
                ModelState.AddModelError("Photos", "Fayl həcmi 2 mbnı keçir !");
                return View();
            }
            info.ImgUrl = await info.Photos.SaveFile(Path.Combine(_web.WebRootPath,"admin", "images", "screenshots"));
            inf.HeaderText = info.HeaderText;
            inf.Model = info.Model;
            inf.MinAge = info.MinAge;
            inf.Luggage = info.Luggage;
            inf.Seats = info.Seats;
            inf.AirCondition = info.AirCondition;
            inf.Doors = info.Doors;
            _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            CarInfo info = _context.CarInfos.Find(id);
            if (info == null) return NotFound();
            Extention.Delete(Path.Combine(_web.WebRootPath, "admin", "images", "screenshots", info.ImgUrl));
            _context.CarInfos.Remove(info);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
