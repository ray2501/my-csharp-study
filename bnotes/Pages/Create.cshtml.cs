using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bnotes.Services;
using bnotes.Data;

namespace bnotes.Pages
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _noteService.AddNote(note: note);
            return RedirectToPage("/Index");
        }
    }
}