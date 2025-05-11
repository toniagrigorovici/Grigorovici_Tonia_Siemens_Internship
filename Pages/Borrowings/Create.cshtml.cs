using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Borrowings
{
    public class CreateModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public CreateModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var bookList = _context.Book
                .Where(b => b.AvailableQuantity > 0)
                .Select(b => new
                {
                    b.ID,
                    BookFullName = b.Title + " - " + b.Author.FullName
                });
            ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Borrowing.Returned = false;
            Borrowing.ReturnDate = Borrowing.BorrowDate.AddMonths(1);

            var book = await _context.Book.FindAsync(Borrowing.BookID);
            book.AvailableQuantity -= 1;

            _context.Borrowing.Add(Borrowing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
