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
    public class CreateModel : PageModel
    {

        [BindProperty]
        public Note note { get; set; }

        private readonly INoteService _noteService;
        public CreateModel(INoteService mynoteService)
        {  
            this._noteService = mynoteService;  
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _noteService.AddNoteAsync(note: note);
            return RedirectToPage("/Index");
        }
    }
}