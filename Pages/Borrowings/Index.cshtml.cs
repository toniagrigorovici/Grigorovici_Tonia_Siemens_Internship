using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Borrowings
{
    public class IndexModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public IndexModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        public IList<Borrowing> Borrowing { get;set; } = default!;
        public int SelectedBorrowingId { get; set; }
        public BookData BorrowingD { get; set; }
        public string CurrentFilter { get; set; }
        public string BorrowingDateSort { get; set; }
        public string ReturnDateSort { get; set; }

        public async Task OnGetAsync(int? id, string searchString, string sortOrder, bool showBorrowedOnly)
        {
            BorrowingD = new BookData();

            BorrowingD.Borrowings = await _context.Borrowing
                .Include(b => b.Book)
                    .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .OrderBy(b => b.BorrowDate)
                    .ThenBy(b => b.ReturnDate)
                .AsNoTracking()
                .ToListAsync();

            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                BorrowingD.Borrowings = BorrowingD.Borrowings.Where(b => b.Book.Title.Contains(searchString) || b.Book.Author.FullName.Contains(searchString) || b.Member.FullName.Contains(searchString));
            }

            if (id != null)
            {
                SelectedBorrowingId = id.Value;
                Book book = BorrowingD.Books
                    .Where(i => i.ID == id.Value).Single();
            }

            if (showBorrowedOnly)
            {
                BorrowingD.Borrowings = BorrowingD.Borrowings.Where(b => b.Returned == false);
            }
        }
    }
}
