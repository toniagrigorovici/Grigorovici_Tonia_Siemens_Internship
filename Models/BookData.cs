namespace Grigorovici_Tonia_Siemens_Internship.Models
{
    public class BookData
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Borrowing> Borrowings { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<BookCategory> BookCategories { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
