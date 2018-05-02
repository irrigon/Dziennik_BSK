using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;
using Microsoft.AspNetCore.Identity;

namespace Dziennik_BSK.Pages.Grades
{
    public class CreateModel : GradeAddsPageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(UserManager<ApplicationUser> userManager,
            Dziennik_BSK.Data.SchoolContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();
            else if (user.Role != Roles.Admin && user.Role != Roles.Teacher)
                return Forbid();

            var emptyGrade = new Grade()
            {
                AddDate = DateTime.Today
            };
            if (user.Role == Roles.Teacher)
                emptyGrade.TeacherId = user.TeacherId ?? 0;
            Grade = emptyGrade;

            PopulateStudentDropDown(_context);
            PopulateTeacherDropDown(_context);
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

            var emptyGrade = Grade;

            if (await TryUpdateModelAsync(emptyGrade, "grade",
                x => x.Id, x => x.Rate, x => x.Subject, x => x.Weight,
                x => x.Comment, x => x.AddDate, x => x.StudentId, x => x.TeacherId)) {
                
                _context.Grades.Add(Grade);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateStudentDropDown(_context);
            PopulateTeacherDropDown(_context);
            return Page();
        }
    }
}