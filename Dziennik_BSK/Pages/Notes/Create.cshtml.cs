using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages.Notes
{
    public class CreateModel : NoteAddsPageView
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public CreateModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateStudentDropDown(_context);
            PopulateTeacherDropDown(_context);
            return Page();
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyNote = new Note();

            if (await TryUpdateModelAsync<Note>(emptyNote, "note",
                x => x.Id, x => x.AddDate, x => x.Content, x => x.IsNegative,
                x => x.StudentId, x => x.TeacherId)) {


                _context.Notes.Add(Note);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");

            }

            PopulateStudentDropDown(_context);
            PopulateTeacherDropDown(_context);

            return Page();
        }
    }
}