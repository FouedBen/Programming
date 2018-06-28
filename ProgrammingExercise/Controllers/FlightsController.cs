using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgrammingExercise.Models;

namespace ProgrammingExercise.Controllers
{
    public class FlightsController : Controller
    {
        private FlightContext db = new FlightContext();

        // GET: Flights
        public ActionResult Index()
        {

           
            return View(db.V_ListFlights.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_ListFlights flight = db.V_ListFlights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {

            var data = from p in db.Airoport.OrderBy(a => a.Name)
                       select new
                       {
                           IdAirport = p.IdAirport,
                           Name = p.Name
                       };

            SelectList listAirport = new SelectList(data, "IdAirport", "Name");
            ViewBag.Airport = listAirport;
            return View();
        }

        // POST: Flights/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFlight,IdDepartureAirport,IdDestinationAirport,FlightTime")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            var data = from p in db.Airoport.OrderBy(a => a.Name)
                       select new
                       {
                           IdAirport = p.IdAirport,
                           Name = p.Name
                       };

            SelectList listAirport = new SelectList(data, "IdAirport", "Name");
            ViewBag.Airport = listAirport;
            flight.AiroportDeparture = db.Airoport.Where(a => a.IdAirport == flight.IdDepartureAirport).FirstOrDefault();
            flight.AiroportDestination = db.Airoport.Where(a => a.IdAirport == flight.IdDestinationAirport).FirstOrDefault();
            return View(flight);
        }

        // POST: Flights/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFlight,IdDepartureAirport,IdDestinationAirport,FlightTime")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
                        flight.AiroportDeparture = db.Airoport.Where(a => a.IdAirport == flight.IdDepartureAirport).FirstOrDefault();
            flight.AiroportDestination = db.Airoport.Where(a => a.IdAirport == flight.IdDestinationAirport).FirstOrDefault();
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
