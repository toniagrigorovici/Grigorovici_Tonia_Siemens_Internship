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

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public EditModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            Book = book;
            PopulateAssignedCategoryData(_context, Book);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            int oldQuantity = bookToUpdate.Quantity;
            int oldAvailable = bookToUpdate.AvailableQuantity;
            int borrowed = oldQuantity - oldAvailable;

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                b => b.Title, b => b.AuthorID, b => b.Quantity, b => b.AvailableQuantity))
            {
                if (bookToUpdate.Quantity < borrowed)
                {
                    ModelState.AddModelError(string.Empty, "Quantity cannot be less than available quantity.");
                    PopulateAssignedCategoryData(_context, bookToUpdate);
                    ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");
                    return Page();
                }

                bookToUpdate.AvailableQuantity = bookToUpdate.Quantity - borrowed;

                UpdateBookCategories(_context, selectedCategories, bookToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }


        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
