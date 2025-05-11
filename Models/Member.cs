using System.ComponentModel.DataAnnotations;

namespace Grigorovici_Tonia_Siemens_Internship.Models
{
    public class Member
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
        public string? Adress { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }

    }
}
