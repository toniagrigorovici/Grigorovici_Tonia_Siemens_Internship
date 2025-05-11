using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;
using Microsoft.Data.SqlClient;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public IndexModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;
        public int SelectedBookID { get; set; }
        public int CategoryID { get; set; }
        public BookData BookD { get; set; }
        public string CurrentFilter { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public async Task OnGetAsync(int? id, string searchString, string sortOrder, bool showAvailableOnly)
        {
            BookD = new BookData();

            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .ToListAsync();

            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(b => b.Title.Contains(searchString) || b.Author.FullName.Contains(searchString) || b.BookCategories.Any(bc => bc.Category.CategoryName.Contains(searchString)));
            }

            if (id != null)
            {
                SelectedBookID = id.Value;
                Book book = BookD.Books.Where(i => i.ID == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }

            TitleSort = "title";
            AuthorSort = "author";

            switch (sortOrder)
            {
                case "title":
                    BookD.Books = BookD.Books.OrderBy(s => s.Title);
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author.FullName);
                    break;
            }

            if (showAvailableOnly)
            {
                BookD.Books = BookD.Books.Where(b => b.AvailableQuantity > 0);
            }
        }
    }
}
