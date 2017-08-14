using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Controllers
{
    public class LocationController : Controller
    {
        private ILocationRepository locationRepo;

        public LocationController(ILocationRepository thisRepo = null)
        {
            if(thisRepo == null)
            {
                this.locationRepo = new EFLocationRepository();
            }
            else
            {
                this.locationRepo = thisRepo;
            }
        }

        public ViewResult Index()
        {
            return View(locationRepo.Locations.ToList());
        }
        public IActionResult Details(int id)
        {
            Location thisLocation = locationRepo.Locations.FirstOrDefault(x => x.LocationId == id);
            return View(thisLocation);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Location location)
        {
            locationRepo.Save(location);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Location thisLocation = locationRepo.Locations.FirstOrDefault(x => x.LocationId == id);
            return View(thisLocation);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            locationRepo.Edit(location);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Location thisLocation = locationRepo.Locations.FirstOrDefault(x => x.LocationId == id);
            return View(thisLocation);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Location thisLocation = locationRepo.Locations.FirstOrDefault(x => x.LocationId == id);
            locationRepo.Remove(thisLocation);
            return RedirectToAction("Index");
        }
    }
}