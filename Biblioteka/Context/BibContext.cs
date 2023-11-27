using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Context
{
	public class BibContext : DbContext
	{
		public BibContext(DbContextOptions<BibContext> options) : base(options)
		{

		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
	}
}
