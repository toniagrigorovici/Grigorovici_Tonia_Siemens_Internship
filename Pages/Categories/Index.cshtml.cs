using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Data;
using Grigorovici_Tonia_Siemens_Internship.Models;

namespace Grigorovici_Tonia_Siemens_Internship.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext _context;

        public IndexModel(Grigorovici_Tonia_Siemens_Internship.Data.Grigorovici_Tonia_Siemens_InternshipContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!; public BookData CategoryData { get; set; } = default!;
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? bookID, string searchString)
        {
            CategoryData = new BookData();

            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(b => b.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                CategoryData.Categories = CategoryData.Categories
                    .Where(c => c.CategoryName.Contains(searchString));
            }

            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories.SingleOrDefault(c => c.ID == id.Value);

                CategoryData.Books = category.BookCategories
                    .Select(b => b.Book)
                    .ToList();
            }
        }

    }
}
