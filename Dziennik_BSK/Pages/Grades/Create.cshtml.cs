using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages.Grades
{
    public class CreateModel : GradeAddsPageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public CreateModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateStudentDropDown(_context);
            PopulateTacherDropDown(_context);
            return Page();
        }

        [BindProperty]
        public Grade Grade { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyGrade = new Grade();

            if (await TryUpdateModelAsync(emptyGrade, "grade",
                x => x.Id, x => x.Rate, x => x.Subject, x => x.Weight,
                x => x.Comment, x => x.AddDate, x => x.StudentId, x => x.TeacherId)) {
                
                _context.Grades.Add(Grade);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateStudentDropDown(_context);
            PopulateTacherDropDown(_context);
            return Page();
        }
    }
}