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

namespace Dziennik_BSK.Pages.Participations
{
    public class EditModel : ParticipationAdds
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
        public Participation Participation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();
            else if (user.Role != Roles.Admin && user.Role != Roles.Teacher)
                return Forbid();

            Participation = await _context.Participations.Include(x => x.Lesson).
                Include(x => x.Student).SingleOrDefaultAsync(m => m.Id == id);

            if (user.Role == Roles.Teacher &&
                user.TeacherId != Participation.Lesson.TeacherId)
                return Forbid();

            if (Participation == null)
            {
                return NotFound();
            }

            PopulateLessonDropDownList(_context);
            PopulateStudentsDropDownList(_context);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Participation).State = EntityState.Modified;

            var objToUpdate = await _context.Participations.FindAsync(id);

            if (await TryUpdateModelAsync<Participation>(objToUpdate,
                "participation", s => s.IsPresent, s => s.LessonId,
                s => s.StudentId)) {

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipationExists(Participation.Id))
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

            PopulateLessonDropDownList(_context);
            PopulateStudentsDropDownList(_context);
            return RedirectToPage("./Index");
        }

        private bool ParticipationExists(int id)
        {
            return _context.Participations.Any(e => e.Id == id);
        }
    }
}
