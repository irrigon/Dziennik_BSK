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

namespace Dziennik_BSK.Pages.Lessons
{
    public class EditModel : TeacherPageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public EditModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lesson = await _context.Lessons.Include(x => x.Teacher).
                SingleOrDefaultAsync(m => m.Id == id);

            if (Lesson == null)
            {
                return NotFound();
            }

            PopulateTeacherDropDown(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Lesson).State = EntityState.Modified;

            var lessonToUpdate = await _context.Lessons.FindAsync(id);

            if (await TryUpdateModelAsync<Lesson>(lessonToUpdate, "lesson",
                x => x.Class, x => x.LessonDate, x => x.Subject, x => x.Topic,
                x => x.TeacherId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(Lesson.Id))
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

            PopulateTeacherDropDown(_context);
            return Page();
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
