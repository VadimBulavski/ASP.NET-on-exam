using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using RaceContext;
using Service;

namespace WebCarRace.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        RaceCarContext db;
        private IService _service = null;

       
        public AdminController(IService service, RaceCarContext context)
        {
            _service = service;
            db = context;
        }
        //
        // GET: /Admin/Admin/
        public ActionResult ListOfRaces()
        {
            return View(_service.GetAllRaces());
        }

        public ActionResult CreateRace(int? id)//Race race)
        {
            if (id != null)
            {
                Race race = db.Races.FirstOrDefault(r => r.RaceID == id);
                return View(race);
            }
            else
            {
                return View();
            }
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRace(Race race, string action)
        {
            if(ModelState.IsValid)
            {
                if (action == "Create")
                {
                    db.Races.Add(race);
                    db.SaveChanges();
                    return RedirectToAction("CreateRace", new { id = race.RaceID });
                }
                else if (action == "Start Race")
                {
                    return RedirectToAction("ListOfRaces");
                } 
            }
            return View(race);
        }

        [HttpPost]
        public ActionResult FindRace(string nameRace)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("SearchNewsView", _service.GetNameRace(nameRace));
            }
            else
            {
                return View("Index", _service.GetAllRaces());
            }

        }


        //
        // GET: /Admin/Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Admin/Create
        public ActionResult CreateCar()
        {
            //if(id != null)
            //{
            //    db.Races.Where(r => r.RaceID == id);
            //}
            return View();
        }

        //
        // POST: /Admin/Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar(Car car, int id)
        {
            if(ModelState.IsValid)
            {
                Race race = db.Races.FirstOrDefault(r => r.RaceID == id);
                if(race.Cars == null)
                {
                    race.Cars = new List<Car>();
                }
                race.Cars.Add(car);
                //db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("CreateRace", new { id = race.RaceID });
            }
            return View(car);
        }

        //
        // GET: /Admin/Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
