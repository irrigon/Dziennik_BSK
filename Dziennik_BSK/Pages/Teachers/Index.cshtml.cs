using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages_Teachers
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public IndexModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public PaginatedList<Teacher> Teacher { get;set; }
        public string SurnameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchName,
            int? pageIndex)
        {
            CurrentSort = sortOrder;
            SurnameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchName is null)
                searchName = CurrentFilter;
            else
                pageIndex = 1;

            CurrentFilter = searchName;

            IQueryable<Teacher> teacherQuery = _context.Teachers.Select(x => x);
            if (!String.IsNullOrEmpty(searchName))
                teacherQuery = teacherQuery.Where(x => x.FullName.Contains(searchName));

            switch (sortOrder)
            {
                case "name_desc":
                    teacherQuery = teacherQuery.OrderByDescending(x => x.Surname);
                    break;
                default:
                    teacherQuery = teacherQuery.OrderBy(x => x.Surname);
                    break;
            }

            int pageSize = 5;
            Teacher = await PaginatedList<Teacher>.CreateAsync(teacherQuery.AsNoTracking(),
                pageIndex ?? 1, pageSize);
        }
    }
}
