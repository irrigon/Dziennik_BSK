using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Data;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Pages_Lessons
{
    public class DetailsModel : PageModel
    {
        private readonly Dziennik_BSK.Data.SchoolContext _context;

        public DetailsModel(Dziennik_BSK.Data.SchoolContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
