using System.ComponentModel.DataAnnotations;

namespace Grigorovici_Tonia_Siemens_Internship.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Display(Name = "Book Title")]
        public string Title { get; set; }
        public int? AuthorID { get; set; }
        public Author? Author { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Availability")]
        public int AvailableQuantity { get; set; }
        [Display(Name = "Categories")]
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
