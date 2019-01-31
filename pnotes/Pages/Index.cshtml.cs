using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pnotes.Services;
using pnotes.Data;
using Microsoft.AspNetCore.Http.Features;

namespace pnotes.Pages
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
        public async Task OnGetAsync()
        {
            var _noteList = await _noteService.GetAllAsync();
            Notes = _noteList;
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _noteService.DeleteNoteByIdAsync(id);

            return RedirectToPage();
        }
    }
}