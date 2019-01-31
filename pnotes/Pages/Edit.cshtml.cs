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
    public class EditModel : PageModel
    {

        [BindProperty]
        public Note note { get; set; }

        private readonly INoteService _noteService;
        public EditModel(INoteService mynoteService)
        {  
            this._noteService = mynoteService;  
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            note = await _noteService.GetNoteByIdAsync(id);

            if (note == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _noteService.UpdateNoteAsync(note: note);
            return RedirectToPage("/Index");
        }
    }
}