using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetcoreapp.Pages
{    

    public class UploadModel : PageModel
    {            
         [BindProperty] // Bind on Post
        public IFormFile file { get; set; }

        private IWebHostEnvironment _hostingEnvironment;
        
        public UploadModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var uploadsDirectoryPath = Path.Combine(path1: _hostingEnvironment.WebRootPath, path2: "Uploads");
            var uploadedfilePath = Path.Combine(uploadsDirectoryPath, file.FileName);

            using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }            

            return RedirectToPage("/Index");
        }        
    }
}