using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
//using System.Data.Entity;


namespace Biblioteka.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        //public MyDbContext() : base("MyCS") { }      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().ToTable("Authors");      // <- nie działa idk why
        }
        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Book> Book { get; set; }
       
        public DbSet<Rental> Rental { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagBook> TagBook { get; set; }
        public DbSet<RentalBook> RentalBook { get; set; }
    }
}
