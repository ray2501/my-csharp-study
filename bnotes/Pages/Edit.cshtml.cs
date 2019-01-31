using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bnotes.Services;
using bnotes.Data;

namespace bnotes.Pages
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

        public IActionResult OnGetAsync(Guid id)
        {
            note = _noteService.GetNoteById(id);

            if (note == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }        

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _noteService.UpdateNote(note: note);
            return RedirectToPage("/Index");
        }
    }
}