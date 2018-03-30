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

namespace Dziennik_BSK.Pages_Students
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public IndexModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public PaginatedList<Student> Student { get;set; }
        public string SurnameSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilterName { get; set; }
        public string CurrentFilterClass { get; set; }
        public string CurrentFilterYear { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchName,
            string searchClass, string searchYear, int? pageIndex)
        {
            CurrentSort = sortOrder;
            SurnameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchName is null)
                searchName = CurrentFilterName;
            if (searchClass is null)
                searchClass = CurrentFilterClass;
            if (searchYear is null)
                searchYear = CurrentFilterYear;
            if (searchClass is null || searchName is null || searchYear is null)
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
        }
        
    }
}
