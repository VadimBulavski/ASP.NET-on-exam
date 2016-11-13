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

        public ActionResult CreateRace()//Race race)
        {
            //if(race != null)
            //{
            //    return View(race);
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }

        [HttpPost, ActionName("CreateRace")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRaceConfirmed(Race race)
        {
            if(ModelState.IsValid)
            {
                db.Races.Add(race);
                db.SaveChanges();
                return RedirectToAction("ListOfRaces");
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
            return View();
        }

        //
        // POST: /Admin/Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar(Car car)
        {
            if(ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("CreateRace");
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
