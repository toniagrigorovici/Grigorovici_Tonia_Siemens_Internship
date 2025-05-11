using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;
using System.Security.Policy;
using System.Drawing;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public IndexModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        public IList<Author> Author { get;set; } = default!;
        public BookData AuthorData { get; set; }
        public int AuthorID { get; set; }
        public int BookID { get; set; }
        public string CurrentFilter { get; set; } 

        public async Task OnGetAsync(int? id, int? bookID, string searchString)
        {
            AuthorData = new BookData();

            AuthorData.Authors = await _context.Author
                .Include(b => b.Books)
                .OrderBy(b => b.FirstName)
                    .ThenBy(b => b.LastName)
                .ToListAsync();

            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                AuthorData.Authors = AuthorData.Authors
                    .Where(b => b.FullName.Contains(searchString));
            }

            if (id != null)
            {
                AuthorID = id.Value;
                Author author = AuthorData.Authors
                    .SingleOrDefault(i => i.ID == id.Value);

                if (author != null)
                {
                    AuthorData.Books = author.Books;
                }
            }
        }
    }
}
