using JCMdotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace JCMdotNet.Controllers
{
    public class Files1 : Controller
    {
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public Files1(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(String fileName = "")
        {
            FileClass fileObj = new FileClass();
            fileObj.Name = fileName;
            fileObj.Files = new List<FileClass>();  // Ensure Files property is initialized

            String path = $"{_hostingEnvironment.WebRootPath}\\files\\";
            int nId = 1;
            foreach (string pdfPath in Directory.EnumerateFiles(path, "*.pdf"))
            {
                fileObj.Files.Add(new FileClass()
                {
                    FileId = nId++,
                    Name = Path.GetFileName(pdfPath),
                    Path = pdfPath
                });
            }
            return View(fileObj);  // Pass fileObj to the view
        }
        //

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return Index();
        }

        public IActionResult PDFViewerNewTab(string fileName)
        {

            string path = _hostingEnvironment.WebRootPath + "\\files\\" + fileName;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");

        }
    }
}