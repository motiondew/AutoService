using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assignment2.Data;
using assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using assignment2.Services;
using System.Text.Encodings.Web;
using MongoDB.Bson;

namespace assignment2.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private AppointmentService _service;
        public AppointmentsController(IMongoRepository<Appointment> repository)
        {
            _service = new AppointmentService(repository);
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = _service.Get();
            return View(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string SearchString, bool notUsed)
        {
            var appointments = _service.Search(SearchString);

            if (String.IsNullOrEmpty(SearchString))
            {
                return View(Enumerable.Empty<Appointment>());
            }
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _service.Get(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,date,clientName,telephoneNo,carBrand,description,status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (_service.Add(appointment) == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "That time is already reserved");
                }
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            var appointment = _service.Get(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BsonID,date,clientName,telephoneNo,carBrand,description,status")] Appointment appointment)
        {

            if (id != appointment.BsonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var appointment = _service.Get(id);
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Appointment appointment = _service.Get(id);
            _service.Delete(appointment.BsonID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Report(string date1 = "2020-03-01T11:11", string date2 = "2020-12-01T11:11")
        {
            var dateAppointments = _service.Get(DateTime.Parse(date1), DateTime.Parse(date2));
            return View(dateAppointments);
        }

        public FileContentResult Export(int type)
        {
            //TODO : edit
            Document document = _service.Export(type);
            return File(document.FileContent, document.FileType, document.DownloadName);
        }
        public FileContentResult ExportDate(int type, string date1 = "2020-03-01T11:11", string date2 = "2020-12-01T11:11")
        {
            //TODO : edit
            var dateAppointments = _service.Get(DateTime.Parse(date1), DateTime.Parse(date2));
            Document document = _service.ExportDate(dateAppointments, type);
            return File(document.FileContent, document.FileType, document.DownloadName);
        }


    }
}

