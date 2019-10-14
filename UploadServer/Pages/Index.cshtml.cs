using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetcoreapp.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _hostingEnvironment;
        public List<FileInfo> Files {get; set;}

        public IndexModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnPostOkcookie()
        {
            var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            consentFeature.GrantConsent();
        }

        public void OnGet()
        {
            var uploadsDirectoryPath = Path.Combine(path1: _hostingEnvironment.WebRootPath, path2: "Uploads");
            DirectoryInfo d = new DirectoryInfo(uploadsDirectoryPath.ToString());
            FileInfo[] files = d.GetFiles("*"); //Getting files
            Files = files.OfType<FileInfo>().OrderBy(x => x.Name).ToList();
        }
    }
}
