using System.ComponentModel.DataAnnotations;

namespace Grigorovici_Tonia_Siemens_Internship.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Author")]
        public string FullName { get { return FirstName + " " + LastName; } }
        public ICollection<Book>? Books { get; set; }
    }
}
