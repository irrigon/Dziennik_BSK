using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dziennik_BSK.Data;

namespace Dziennik_BSK.Pages.Notes
{
    public class NoteAddsPageView : PageModel
    {
        public SelectList StudentSL { get; set; }
        public SelectList TeacherSL { get; set; }

        public void PopulateStudentDropDown(SchoolContext context,
            object selectedItem = null, string firstName = null,
            string surname = null, string cls = null)
        {
            var studentQuery = context.Students.Select(x => x);
            if (!(firstName is null))
                studentQuery = studentQuery.Where(x => x.FirstName == firstName);
            if (!(surname is null))
                studentQuery = studentQuery.Where(x => x.Surname == surname);
            if (!(cls is null))
                studentQuery = studentQuery.Where(x => x.Class == cls);

            StudentSL = new SelectList(studentQuery, "Id", "FullName", selectedItem);
        }

        public void PopulateTeacherDropDown(SchoolContext context,
            object selectedItem = null, string firstName = null,
            string surname = null)
        {
            var teacherQuery = context.Teachers.Select(x => x);
            if (!(firstName is null))
                teacherQuery = teacherQuery.Where(x => x.FirstName == firstName);
            if (!(surname is null))
                teacherQuery = teacherQuery.Where(x => x.Surname == surname);

            TeacherSL = new SelectList(teacherQuery, "Id", "FullName", selectedItem);
        }
    }
}
