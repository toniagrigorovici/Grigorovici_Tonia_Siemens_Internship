using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public EditModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            Borrowing = borrowing;

            var availableBooks = await _context.Book
                .Include(b => b.Author)
                .Where(b => b.AvailableQuantity > 0)
                .Select(b => new
                {
                    b.ID,
                    BookFullName = b.Title + " - " + b.Author.FullName
                })
                .ToListAsync();

            ViewData["BookID"] = new SelectList(availableBooks, "ID", "BookFullName");
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var originalBorrowing = await _context.Borrowing
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ID == Borrowing.ID);

            if (originalBorrowing.BookID != Borrowing.BookID)
            {
                var oldBook = await _context.Book.FindAsync(originalBorrowing.BookID);
                oldBook.AvailableQuantity += 1;

                var newBook = await _context.Book.FindAsync(Borrowing.BookID);
                newBook.AvailableQuantity -= 1;
            }

            if (Borrowing.Returned)
            {
                var book = await _context.Book.FindAsync(Borrowing.BookID);
                book.AvailableQuantity += 1;
            }

            if (Borrowing.ReturnDate < Borrowing.BorrowDate)
            {
                ModelState.AddModelError(string.Empty, "Return date must be later than borrow date.");
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
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

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
