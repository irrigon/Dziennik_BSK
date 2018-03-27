using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages.Participations
{
    public class DeleteModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public DeleteModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Participation Participation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Participation = await _context.Participations.SingleOrDefaultAsync(m => m.Id == id);

            if (Participation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Participation = await _context.Participations.FindAsync(id);

            if (Participation != null)
            {
                _context.Participations.Remove(Participation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
