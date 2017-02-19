using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using RaceContext;
using Service;
using System.Net;
using System.Threading;
using System.Data.Entity;

namespace WebCarRace.Areas.Admin.Controllers
{
    public class BackgroundThread
    {
        private static int _timerinterval;
        private static int increasinginterval;
        private static int _id;
        private static Timer timer;
        private static RaceCarContext _context;
        public BackgroundThread(int id, RaceCarContext context)
        {
            _id = id;
            _timerinterval = 5000;
            increasinginterval = _timerinterval;
            _context = context;
            
            TimerCallback tcb = new TimerCallback(CalculationOfIndicators);
            timer = new Timer(tcb, _id, 5000, _timerinterval);
        }
        
        public static void CalculationOfIndicators(object obj)
        {
            int id = (int)obj;
            Race race = _context.Races.Where(s => s.RaceID == id).FirstOrDefault();
            if(race.Cars != null)
            {
                for (int i = 0; i < race.Cars.Count; ++i)
                {
                    //----ДЛЯ БУДУЩЕ МОДИФИКАЦИИ-----

                    //int breakpoint = race.Cars[i].AccelerationInterval + race.Cars[i].DurationOfAcceleration;
                    //if (race.Cars[i].AccelerationInterval >= _timerinterval)
                    //{
                    //    race.Cars[i].Speed += race.Cars[i].Speed * race.Cars[i].DeltaAcceleration/100;
                    //}
                    //if (breakpoint <= _timerinterval)
                    //{
                    //    race.Cars[i].Speed -= race.Cars[i].Speed * race.Cars[i].DeltaAcceleration/100;
                    //    race.Cars[i].AccelerationInterval += race.Cars[i].AccelerationInterval;
                    //}


                    race.Cars[i].Distance = (race.Cars[i].Speed * increasinginterval)/3600000;
                    if (race.Cars[i].Distance >= race.Distance)
                    {
                        DisposeTimer();
                    }
                }
                increasinginterval += _timerinterval;
                _context.SaveChanges();    
            }   
        }
        
        
        public static void DisposeTimer()
        {
            _timerinterval = 0;
            timer.Dispose();
        }
    }

    public static class PathAction
    {
        private static string _requestSegmentUrl;
        private static int _id;

        public static int ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        public static string RequestSegmentUrl
        {
            set
            {
                _requestSegmentUrl = value;
            }

            get
            {
                return _requestSegmentUrl;
            }
        }

        public static void GetSegmentUrl(string segmentUrl)
        {
            string newSegmentUrl = segmentUrl.Replace('/', ' ').Trim();
            RequestSegmentUrl = newSegmentUrl;
        }

        public static void GetSegmentUrlId(string segmentUrl)
        {
            string newSegmentUrlId = segmentUrl.Replace('/', ' ').Trim();
            ID = Int32.Parse(newSegmentUrlId);
        }

    }

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
            PathAction.GetSegmentUrl(@Request.Url.Segments[3]);
            return View(_service.GetAllRaces());
        }

        public ActionResult CreateRace(int? id)
        {
            PathAction.GetSegmentUrl(@Request.Url.Segments[3]);
            if (id != null)
            {
                PathAction.GetSegmentUrlId(@Request.Url.Segments[4]);
                Race race = db.Races.FirstOrDefault(r => r.RaceID == id);
                return View(race);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRace(Race race, string action)
        {
            PathAction.GetSegmentUrl(@Request.Url.Segments[3]);

            if (ModelState.IsValid)
            {
                if (action == "Create")
                {
                    db.Races.Add(race);
                    db.SaveChanges();
                    return RedirectToAction("CreateRace", new { id = race.RaceID });
                }
                else if (action == "Start Race")
                {
                    BackgroundThread bct = new BackgroundThread(PathAction.ID, db);
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
                return PartialView("SearchRaceView", _service.GetNameRace(nameRace));
            }
            else
            {
                return View("ListOfRaces", _service.GetAllRaces());
            }

        }


        //
        [HttpPost]
        public ActionResult DetailsCar(int? carID)
        {
            if (carID != null)
            {
                return PartialView(_service.GetCar((int)carID));
            }
            else
            {
                return PartialView();
            }

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
        public ActionResult CreateCar(Car car, int id)
        {
            if (ModelState.IsValid)
            {
                Race race = _service.GetRace(id); 
                if (race.Cars == null)
                {
                    race.Cars = new List<Car>();
                }
                race.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("CreateRace", new { id = race.RaceID });
            }
            return View(car);
        }

        //
        // GET: /Admin/Admin/Edit/5
        public ActionResult EditCar(int? carID)
        {
            if (carID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car nextCar = _service.GetCar((int)carID);
            if (nextCar == null)
            {
                return HttpNotFound();
            }
            return View(nextCar);
        }

        //
        // POST: /Admin/Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car nextCar)
        {
            if (ModelState.IsValid)
            {
                Race race = _service.GetRace(PathAction.ID);
                var newCar = db.Cars.Where(s => s.CarID == nextCar.CarID).FirstOrDefault();
                newCar.NameCar = nextCar.NameCar;
                newCar.Speed = nextCar.Speed;

                //----ДЛЯ МОДИФИКАЦИИ----
                //newCar.DeltaAcceleration = nextCar.DeltaAcceleration;
                //newCar.AccelerationInterval = nextCar.AccelerationInterval;
                //newCar.DurationOfAcceleration = nextCar.DurationOfAcceleration;


                db.SaveChanges();
                if (PathAction.RequestSegmentUrl == "CreateRace")
                {
                    return RedirectToAction("CreateRace", new { id = race.RaceID });
                }
                else if (PathAction.RequestSegmentUrl == "ListOfRaces")
                {
                    return RedirectToAction("ListOfRaces");
                }

            }
            return View(nextCar);
        }

        //
        // GET: /Admin/Admin/Delete/5
        public ActionResult DeleteCar(int? carID)
        {
            if (carID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _service.GetCar((int)carID);
            if (car == null)
            {
                return HttpNotFound();
            }
            return DeleteCarConfirmed(car.CarID);
        }


        [HttpPost, ActionName("DeleteCar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCarConfirmed(int id)
        {
            Race race = _service.GetRace(PathAction.ID);
            _service.RemoveCar(id);
            db.SaveChanges();
            if (PathAction.RequestSegmentUrl == "CreateRace")
            {
                return RedirectToAction("CreateRace", new { id = race.RaceID });
            }
            return RedirectToAction("ListOfRaces");
        }

        public ActionResult DeleteRace(int? raceID)
        {
            if (raceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = _service.GetRace((int)raceID);
            if (race == null)
            {
                return HttpNotFound();
            }
            return DeleteRaceConfirmed(race.RaceID);
        }


        [HttpPost, ActionName("DeleteRace")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRaceConfirmed(int id)
        {
            Race race = db.Races.Where(s => s.RaceID == id).FirstOrDefault();
            _service.RemoveRace(id);
            db.SaveChanges();
            return RedirectToAction("CreateRace", new { id = race.RaceID });
        }
    }
}
