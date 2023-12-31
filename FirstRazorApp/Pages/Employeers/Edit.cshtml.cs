﻿using FirstRazorApp.AppRepository;
using FirstRazorApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;

namespace FirstRazorApp.Pages.Employeers
{
    public class EditModel : PageModel
    {
        private readonly IEmpoyeeRepository _empoyeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public EditModel(IEmpoyeeRepository empoyeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _empoyeeRepository = empoyeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }




        public IActionResult OnGet(int? id)
        {

            if (id.HasValue)
                Employee = _empoyeeRepository.GetEmployee(id.Value);
            else
                Employee = new Employee();

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }


        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {

                if (Photo != null)
                {
                    if (Employee.PotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Employee.PotoPath);

                        if (Employee.PotoPath != "noimage.png")
                            System.IO.File.Delete(filePath);
                    }

                    Employee.PotoPath = ProcessUploadedFile();
                }

                if (Employee.Id > 0)
                {

                    Employee = _empoyeeRepository.Update(Employee);

                    TempData["SuccessMessage"] = $"Update {Employee.Name} success!";
                }
                else
                {
                    Employee = _empoyeeRepository.Add(Employee);


                    TempData["SuccessMessage"] = $"Adding {Employee.Name} success!";
                }


                return RedirectToPage("/Employeers/Index");
            }

            return Page();
        }

        //Methode zur Verarbeitung hochgeladener Bilder

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        // Benachrichtigungsformular
        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notifications";
            }
            else
            {
                Message = "You have turned off email notifications";
            }

            Employee = _empoyeeRepository.GetEmployee(id);
        }
    }
}