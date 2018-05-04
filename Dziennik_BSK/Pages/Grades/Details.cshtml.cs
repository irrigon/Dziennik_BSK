using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;
using Microsoft.AspNetCore.Identity;

namespace Dziennik_BSK.Pages_Grades
{
    public class DetailsModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager,
            Dziennik_BSK.Data.SchoolContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Grade Grade { get; set; }
        public Roles Role { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();

            Role = user.Role;

            Grade = await _context.Grades.Include(x => x.Student).
                Include(x => x.Teacher).SingleOrDefaultAsync(m => m.Id == id);

            if (user.Role == Roles.Student && user.StudentId != Grade.StudentId)
                return Forbid();

            if (Grade == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
