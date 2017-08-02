using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace TravelBlog.Controllers
{
    public class PeopleController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            return View(db.People.Include(people => people.Experiences).ToList());
        }

        public IActionResult Details(int id)
        {
            People thisPeople = db.People.Include(people => people.Experiences)
                                         .FirstOrDefault(person => person.PeopleId == id);
            return View(thisPeople);
        }

        public IActionResult Create()
        {
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Description");
            return View();
        }

        [HttpPost]
        public IActionResult Create(People people)
        {
            db.People.Add(people);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            People thisPeople = db.People.FirstOrDefault(experiences => experiences.PeopleId == id);
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Description");
            return View(thisPeople);
        }

        [HttpPost]
        public IActionResult Edit(People people)
        {
            db.Entry(people).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            People thisPeople = db.People.FirstOrDefault(people => people.PeopleId == id);
            return View(thisPeople);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            People thisPeople = db.People.FirstOrDefault(people => people.PeopleId == id);
            db.People.Remove(thisPeople);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
