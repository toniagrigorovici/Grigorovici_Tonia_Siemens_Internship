using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public DeleteModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FirstOrDefaultAsync(m => m.ID == id);

            if (author == null)
            {
                return NotFound();
            }
            else
            {
                Author = author;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author != null)
            {
                Author = author;
                _context.Author.Remove(Author);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
