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
    public class DetailsModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public DetailsModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

        public Participation Participation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Participation = await _context.Participations.Include(d => d.Lesson).
                Include(d => d.Student).SingleOrDefaultAsync(m => m.Id == id);
            
            if (Participation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
