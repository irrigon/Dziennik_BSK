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

namespace Dziennik_BSK.Pages_Lessons
{
    public class DetailsModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager,
            Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public Lesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();

            Lesson = await _context.Lessons.Include(x => x.Teacher).
                SingleOrDefaultAsync(m => m.Id == id);

            if (user.StudentId is null) {
                var student = await _context.Students.FirstAsync(x => x.Id == user.StudentId);
                if (user.Role == Roles.Student && Lesson.Class != student.Class)
                    return Forbid();
            }
            
            if (Lesson == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
