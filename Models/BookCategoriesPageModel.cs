using Grigorovici_Tonia_Siemens_Internship.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grigorovici_Tonia_Siemens_Internship.Models
{
    public class BookCategoriesPageModel: PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;

        public void PopulateAssignedCategoryData(Grigorovici_Tonia_Siemens_InternshipContext context, Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(book.BookCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var category in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = category.ID,
                    Name = category.CategoryName,
                    Assigned = bookCategories.Contains(category.ID)
                });
            }
        }

        public void UpdateBookCategories(Grigorovici_Tonia_Siemens_InternshipContext context, string[] selectedCategories, Book bookToUpdate)
        {
            if(selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(bookToUpdate.BookCategories.Select(c => c.CategoryID));
            foreach (var category in context.Category)
            {
                if (selectedCategoriesHS.Contains(category.ID.ToString()))
                {
                    if (!bookCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory { BookID = bookToUpdate.ID, CategoryID = category.ID });
                    }
                }
                else
                {
                    if (bookCategories.Contains(category.ID))
                    {
                        BookCategory courseToRemove = bookToUpdate.BookCategories.SingleOrDefault(i => i.CategoryID == category.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
