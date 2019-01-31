using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bnotes.Services;
using bnotes.Data;
using Microsoft.AspNetCore.Http.Features;

namespace bnotes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INoteService _noteService;
        public IndexModel(INoteService mynoteService)
        {  
            this._noteService = mynoteService;  
        }

        public void OnPostOkcookie()
        {
            var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            consentFeature.GrantConsent();
        }        

        public IEnumerable<Note> Notes;
        public void OnGet()
        {
            var _noteList = _noteService.GetAll();
            Notes = _noteList;
        }

        public IActionResult OnPostDelete(Guid id)
        {
            _noteService.DeleteNoteById(id);

            return RedirectToPage();
        }
    }
}