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

namespace Dziennik_BSK.Pages.Notes
{
    public class EditModel : NoteAddsPageView
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public EditModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Note = await _context.Notes.SingleOrDefaultAsync(m => m.Id == id);

            if (Note == null)
            {
                return NotFound();
            }

            PopulateStudentDropDown(_context);
            PopulateTeacherDropDown(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Note).State = EntityState.Modified;

            var noteToUpdate = await _context.Notes.FindAsync(id);

            if (await TryUpdateModelAsync<Note>(noteToUpdate, "note",
                x => x.AddDate, x => x.Content, x => x.IsNegative,
                x => x.StudentId, x => x.TeacherId)) {

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(Note.Id))
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

            PopulateStudentDropDown(_context);
            PopulateTeacherDropDown(_context);

            return Page();
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
