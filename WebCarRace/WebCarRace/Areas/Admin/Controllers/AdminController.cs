using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using RaceContext;

namespace WebCarRace.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        EntityRepository _service = new EntityRepository();
        RaceCarContext db = new RaceCarContext();
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
                Race race = db.Races.Where(r => r.RaceID == id).FirstOrDefault();
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
                db.Races.Add(race);
                db.SaveChanges();
                if (action == "Create")
                {
                    return RedirectToAction("CreateRac", race.RaceID);
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
        public ActionResult CreateCar(int id)
        {
            //if(id != null)
            //{
            //    db.Races.Where(r => r.RaceID == id);
            //}
            return View(id);
        }

        //
        // POST: /Admin/Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar(Car car, int id)
        {
            if(ModelState.IsValid)
            {
                Race race = db.Races.Where(r => r.RaceID == id).FirstOrDefault();
                race.Cars.Add(car);
                //db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("CreateRace", race.RaceID);
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
