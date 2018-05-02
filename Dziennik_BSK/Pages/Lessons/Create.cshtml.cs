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

namespace Dziennik_BSK.Pages.Lessons
{
    public class CreateModel : TeacherPageModel
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

            var emptyLesson = new Lesson()
            {
                LessonDate = DateTime.Today
            };
            if (user.Role == Roles.Teacher)
                emptyLesson.TeacherId = user.TeacherId ?? 0;
            Lesson = emptyLesson;

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


            var emptyLesson = Lesson;

            if(await TryUpdateModelAsync<Lesson>(emptyLesson, "lesson", 
                x => x.Id, x => x.LessonDate, x => x.Subject, x => x.Topic,
                x => x.Class, x => x.TeacherId)){

                _context.Lessons.Add(Lesson);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.Role == Roles.Teacher)
            {
                var teacher = _context.Teachers.First(x => x.Id == user.TeacherId);
                PopulateTeacherDropDown(_context, teacher);
            }
            else
                PopulateTeacherDropDown(_context);
            return Page();
        }
    }
}