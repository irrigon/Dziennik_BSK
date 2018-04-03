using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages_Lessons
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public IndexModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public PaginatedList<Lesson> Lesson { get;set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilterSubject { get; set; }
        public string CurrentFilterClass { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchSubject,
            string searchClass, int? pageIndex)
        {
            CurrentSort = sortOrder;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchClass is null)
                searchClass = CurrentFilterClass;
            if (searchSubject is null)
                searchSubject = CurrentFilterSubject;
            if (searchClass != null || searchSubject != null)
                pageIndex = 1;

            CurrentFilterClass = searchClass;
            CurrentFilterSubject = searchSubject;

            IQueryable<Lesson> lessonQuery = _context.Lessons.Select(x => x);
            if (!String.IsNullOrEmpty(searchSubject))
                lessonQuery = lessonQuery.Where(x => x.Subject.Contains(searchSubject));
            if (!String.IsNullOrEmpty(searchClass))
                lessonQuery = lessonQuery.Where(x => x.Class.Contains(searchClass));

            switch (sortOrder) {
                case "date_desc":
                    lessonQuery = lessonQuery.OrderByDescending(x => x.LessonDate);
                    break;
                default:
                    lessonQuery = lessonQuery.OrderBy(x => x.LessonDate);
                    break;
            }

            int pageSize = 5;
            Lesson = await PaginatedList<Lesson>.CreateAsync(lessonQuery, 
                pageIndex ?? 1, pageSize);
        }
    }
}
