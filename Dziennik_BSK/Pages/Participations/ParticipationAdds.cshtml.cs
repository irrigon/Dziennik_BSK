using System;
using Dziennik_BSK.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dziennik_BSK.Pages.Participations
{
    public class ParticipationAdds : PageModel
    {
        public SelectList LessonSL { get; set; }
        public SelectList StudentSL { get; set; }

        public void PopulateLessonDropDownList(SchoolContext context,
            DateTime? lessonDate = null, string subject = null ,
            object selectedLesson = null)
        {
            var lessonQuery = context.Lessons.Select(x => x);
            if (!(lessonDate is null))
                lessonQuery = lessonQuery.Where(x => x.LessonDate == lessonDate);
            if (!(subject is null))
                lessonQuery = lessonQuery.Where(x => x.Subject == subject);

            LessonSL = new SelectList(lessonQuery.AsNoTracking(), "Id", 
                "LessonName" ,selectedLesson);
        }

        public void PopulateStudentsDropDownList(SchoolContext context, string firstName = null,
            string surname = null, string cls = null, object selectedStudent = null)
        {
            var studentQuery = context.Students.Select(x => x);
            if (!(firstName is null))
                studentQuery = studentQuery.Where(x => x.FirstName == firstName);
            if (!(surname is null))
                studentQuery = studentQuery.Where(x => x.Surname == surname);
            if (!(cls is null))
                studentQuery = studentQuery.Where(x => x.Class == cls);

            StudentSL = new SelectList(studentQuery.AsNoTracking(), "Id",
                "FullName", selectedStudent);

        }
    }
}
 