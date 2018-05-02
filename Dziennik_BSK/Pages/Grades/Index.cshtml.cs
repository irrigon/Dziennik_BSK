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

        public PaginatedList<Grade> Grade { get; set; }
        public Roles Role { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchSubject,
            int? pageIndex)
        {
            CurrentSort = sortOrder;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchSubject is null)
                searchSubject = CurrentFilter;
            else
                pageIndex = 1;

            CurrentFilter = searchSubject;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();

            IQueryable<Grade> gradeQuery = _context.Grades.Select(x => x);
            if (user.Role == Roles.Student)
                gradeQuery = gradeQuery.Where(x => x.StudentId == user.StudentId);
            else if (user.Role == Roles.Teacher)
                gradeQuery = gradeQuery.Where(x => x.TeacherId == user.TeacherId);

            if (!String.IsNullOrEmpty(searchSubject))
                gradeQuery = gradeQuery.Where(x => x.Subject.Contains(searchSubject));

            switch (sortOrder) {
                case "date_desc":
                    gradeQuery = gradeQuery.OrderByDescending(x => x.AddDate);
                    break;
                default:
                    gradeQuery = gradeQuery.OrderBy(x => x.AddDate);
                    break;
            }

            int pageSize = 5;
            Grade = await PaginatedList<Grade>.CreateAsync(gradeQuery.AsNoTracking(),
                pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
