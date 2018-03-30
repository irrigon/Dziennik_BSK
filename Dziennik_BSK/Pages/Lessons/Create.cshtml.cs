using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages.Lessons
{
    public class CreateModel : TeacherPageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public CreateModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateTeacherDropDown(_context);
            return Page();
        }

        [BindProperty]
        public Lesson Lesson { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyLesson = new Lesson();

            if(await TryUpdateModelAsync<Lesson>(emptyLesson, "lesson", 
                x => x.Id, x => x.LessonDate, x => x.Subject, x => x.Topic,
                x => x.Class, x => x.TeacherId)){

                _context.Lessons.Add(Lesson);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateTeacherDropDown(_context);
            return Page();
        }
    }
}