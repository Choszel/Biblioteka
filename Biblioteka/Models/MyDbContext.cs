using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;


namespace Biblioteka.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        //public MyDbContext() : base("MyCS") { }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().ToTable("Authors");      // <- nie działa idk why
        }
    }
}
