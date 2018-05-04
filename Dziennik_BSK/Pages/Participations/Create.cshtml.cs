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

namespace Dziennik_BSK.Pages.Participations
{
    public class CreateModel : ParticipationAdds
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

            

            PopulateLessonDropDownList(_context);
            PopulateStudentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Participation Participation { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyPar = new Participation();

            if (await TryUpdateModelAsync<Participation>(emptyPar, 
                "participation", s => s.Id, s => s.IsPresent, s => s.LessonId, 
                s => s.StudentId)) {

                _context.Participations.Add(Participation);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateLessonDropDownList(_context);
            PopulateStudentsDropDownList(_context);
            return Page();
        }
    }
}