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

namespace Dziennik_BSK.Pages_Notes
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

        public PaginatedList<Note> Note { get; set; }
        public Roles Role { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilterName { get; set; }
        public string CurrentFilterIsNeg { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchName,
            string searchIsNeg, int? pageIndex)
        {
            CurrentSort = sortOrder;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchIsNeg is null)
                searchIsNeg = CurrentFilterIsNeg;
            if (searchName is null)
                searchName = CurrentFilterName;
            if (searchName != null || searchIsNeg != null)
                pageIndex = 1;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                return Forbid();
            Role = user.Role;

            CurrentFilterIsNeg = searchIsNeg;
            CurrentFilterName = searchName;
            
            IQueryable<Note> noteQuery = _context.Notes.Include(
                x => x.Student).Select(x => x);
            if (user.Role == Roles.Student)
                noteQuery = noteQuery.Where(x => x.StudentId == user.StudentId);
            else if (user.Role == Roles.Teacher)
                noteQuery = noteQuery.Where(x => x.TeacherId == user.TeacherId);

            if (!String.IsNullOrEmpty(searchIsNeg))
                noteQuery = noteQuery.Where(x => x.IsNegative.Contains(searchIsNeg));
            if (!String.IsNullOrEmpty(searchName))
                noteQuery = noteQuery.Where(x => x.Student.FullName.
                Contains(searchName));

            switch (sortOrder) {
                case "date_desc":
                    noteQuery = noteQuery.OrderByDescending(x => x.AddDate);
                    break;
                default:
                    noteQuery = noteQuery.OrderBy(x => x.AddDate);
                    break;
            }

            int pageSize = 5;
            Note = await PaginatedList<Note>.CreateAsync(noteQuery.AsNoTracking(),
                pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
