using System.ComponentModel.DataAnnotations;

namespace Grigorovici_Tonia_Siemens_Internship.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
