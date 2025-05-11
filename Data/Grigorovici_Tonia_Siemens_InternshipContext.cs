using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Siemens_Internship.Models;

namespace Grigorovici_Tonia_Siemens_Internship.Data
{
    public class Grigorovici_Tonia_Siemens_InternshipContext : DbContext
    {
        public Grigorovici_Tonia_Siemens_InternshipContext (DbContextOptions<Grigorovici_Tonia_Siemens_InternshipContext> options)
            : base(options)
        {
        }

        public DbSet<Grigorovici_Tonia_Siemens_Internship.Models.Author> Author { get; set; } = default!;
        public DbSet<Grigorovici_Tonia_Siemens_Internship.Models.Book> Book { get; set; } = default!;
        public DbSet<Grigorovici_Tonia_Siemens_Internship.Models.Member> Member { get; set; } = default!;
        public DbSet<Grigorovici_Tonia_Siemens_Internship.Models.Borrowing> Borrowing { get; set; } = default!;
        public DbSet<Grigorovici_Tonia_Siemens_Internship.Models.Category> Category { get; set; } = default!;
    }
}
