using System;
using Dziennik_BSK.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dziennik_BSK.Pages.Lessons
{
    public class TeacherPageModel : PageModel
    {
        public SelectList TeacherSL { get; set; }

        public void PopulateTeacherDropDown(SchoolContext context, 
            object selectedItem = null, string firstName = null,
            string surname = null) {
            
            var teacherQuery = context.Teachers.Select(x => x);
            
            if(!(firstName is null))
                teacherQuery = teacherQuery.Where(x => x.FirstName == firstName);
            if(!(surname is null))
                teacherQuery = teacherQuery.Where(x => x.Surname == surname);

            TeacherSL = new SelectList(teacherQuery, "Id", "FullName", selectedItem);
        }
    }
}
