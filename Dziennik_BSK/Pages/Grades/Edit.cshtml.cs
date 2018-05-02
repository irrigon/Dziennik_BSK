using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;
using Microsoft.AspNetCore.Identity;

namespace Dziennik_BSK.Pages.Grades
{
    public class EditModel : GradeAddsPageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public EditModel(UserManager<ApplicationUser> userManager,
            Dziennik_BSK.Data.SchoolContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Grade Grade { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();
            else if (user.Role != Roles.Teacher && user.Role != Roles.Admin)
                return Forbid();
            else if (user.Role == Roles.Teacher && user.TeacherId != Grade.TeacherId)
                return Forbid();

            Grade = await _context.Grades.Include(x => x.Student).
                Include(x => x.Teacher).SingleOrDefaultAsync(m => m.Id == id);

            if (Grade == null)
            {
                return NotFound();
            }

            PopulateStudentDropDown(_context);
            PopulateTacherDropDown(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Grade).State = EntityState.Modified;

            var gradeToUpdate = await _context.Grades.FindAsync(id);

            if (await TryUpdateModelAsync(gradeToUpdate, "grade",
                x => x.Rate, x => x.Subject, x => x.Weight, x => x.Comment,
                x => x.AddDate, x => x.StudentId, x => x.TeacherId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(Grade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }

            PopulateStudentDropDown(_context);
            PopulateTacherDropDown(_context);
            return Page();
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
