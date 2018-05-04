using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;
using Dziennik_BSK.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Dziennik_BSK.Pages_Students
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

        public PaginatedList<Student> Student { get;set; }
        public Roles Role { get; set; }
        public string SurnameSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilterName { get; set; }
        public string CurrentFilterClass { get; set; }
        public string CurrentFilterYear { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchName,
            string searchClass, string searchYear, int? pageIndex)
        {
            CurrentSort = sortOrder;
            SurnameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null || user.Role == Roles.Student)
                return Forbid();
            Role = user.Role;

            if (searchName is null)
                searchName = CurrentFilterName;
            if (searchClass is null)
                searchClass = CurrentFilterClass;
            if (searchYear is null)
                searchYear = CurrentFilterYear;
            if (searchClass != null || searchName != null || searchYear != null)
                pageIndex = 1;

            CurrentFilterClass = searchClass;
            CurrentFilterName = searchName;
            CurrentFilterYear = searchYear;
            
            IQueryable<Student> studentQuery = _context.Students.Select(x => x);
            
            if (!String.IsNullOrEmpty(searchYear))
                studentQuery = studentQuery.Where(x => x.Year == searchYear.ToInt32());
            if (!String.IsNullOrEmpty(searchName))
                studentQuery = studentQuery.Where(x => x.FullName.Contains(searchName));
            if (!String.IsNullOrEmpty(searchClass))
                studentQuery = studentQuery.Where(x => x.Class.Contains(searchClass));

            switch (sortOrder) {
                case "name_desc":
                    studentQuery = studentQuery.OrderByDescending(x => x.Surname);
                    break;
                default:
                    studentQuery = studentQuery.OrderBy(x => x.Surname);
                    break;
            }

            int pageSize = 5;
            Student = await PaginatedList<Student>.CreateAsync(studentQuery.AsNoTracking(),
                pageIndex ?? 1, pageSize);
            return Page();
        }
        
    }
}
