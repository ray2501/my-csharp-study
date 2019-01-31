using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using pefnotes.Models;
using pefnotes.Services;

namespace pefnotes.Controllers
{
    public class HomeController : Controller
    {

        private readonly INoteService _service;

        public HomeController(INoteService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            IEnumerable<Note> notes = _service.GetAll();
            return View(new NotesViewModel { Notes = notes });
        }

        [HttpPost]
        public IActionResult AddNote(Note note)
        {
            if (note == null || !ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            _service.AddNote(note);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditNote(Note note)
        {
            if (note == null || !ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            _service.UpdateNote(note);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            Note curnote = _service.GetNoteById(id);
            return View(curnote);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            _service.DeleteNoteById(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public bool OkCookie()
        {
            var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            consentFeature.GrantConsent();

            return true;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
