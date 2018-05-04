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

namespace Dziennik_BSK.Pages.Participations
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager,
            Dziennik_BSK.Data.SchoolContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public PaginatedList<Participation> Participation { get;set; }
        public Roles Role { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilterIsPresent { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchPresent,
            int? pageIndex)
        {
            CurrentSort = sortOrder;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchPresent is null)
                searchPresent = CurrentFilterIsPresent;
            else
                pageIndex = 1;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();
            Role = user.Role;

            CurrentFilterIsPresent = searchPresent;

            IQueryable<Participation> participationQuery = _context.Participations.
                Include(x => x.Lesson).Select(x => x);
            if (user.Role == Roles.Student)
                participationQuery = participationQuery.Where(x => x.StudentId == user.StudentId);
            else if (user.Role == Roles.Teacher) 
                participationQuery = participationQuery.Where(x => x.Lesson.TeacherId == user.TeacherId);
                
            if (!String.IsNullOrEmpty(searchPresent))
                participationQuery = participationQuery.Where(x => x.IsPresent.Contains(searchPresent));

            switch (sortOrder) {
                case "date_desc":
                    participationQuery = participationQuery.OrderByDescending(x => x.Lesson.LessonDate);
                    break;
                default:
                    participationQuery = participationQuery.OrderBy(x => x.Lesson.LessonDate);
                    break;
            }

            int pageSize = 5;
            Participation = await PaginatedList<Participation>.CreateAsync(participationQuery,
                pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
