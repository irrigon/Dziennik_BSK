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

namespace Dziennik_BSK.Pages_Students
{
    public class EditModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(UserManager<ApplicationUser> userManager,
            Dziennik_BSK.Data.SchoolContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null || user.Role != Roles.Admin)
                return Forbid();

            Student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.Id))
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

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
