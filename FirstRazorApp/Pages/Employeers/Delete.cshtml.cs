using FirstRazorApp.AppRepository;
using FirstRazorApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace FirstRazorApp.Pages.Employeers
{
    public class DeleteModel : PageModel
    {
        private readonly IEmpoyeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(IEmpoyeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Employee Employee { get; set; }


        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);

            if (Employee == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            Employee deletedEmployee = _employeeRepository.Delete(Employee.Id);

            if (deletedEmployee.PotoPath != null)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", deletedEmployee.PotoPath);
                if (deletedEmployee.PotoPath != "noimage.png")
                    System.IO.File.Delete(filePath);
            }
            if (deletedEmployee == null)
                return RedirectToPage("/NotFound");

            return RedirectToPage("Index");
        }
    }
}