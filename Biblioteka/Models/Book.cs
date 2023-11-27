using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
	public class Book
	{
		[Key,
			DatabaseGenerated(DatabaseGeneratedOption.Identity),
			Display(Name = "Id książki")]
		public int bookId { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Tytuł"),
			MaxLength(50, ErrorMessage = "Tytuł nie może zawierać więcej niż 50 znaków")]
		public string title { get; set; }

		[BindProperty(SupportsGet = true),
			Column(TypeName = "NUMERIC(13)"),
			Required,
			Display(Name = "Numer ISBN"),
			Range(1000000000000, 9999999999999, ErrorMessage = "Numer ISBN składa się z 13 cyfr")]
		public long ISBN { get; set; }

		[BindProperty(SupportsGet = true),
			Required,
			Display(Name = "Opis"),
			MaxLength(400, ErrorMessage = "Opis nie może zawierać więcej niż 400 znaków")]
		public string description { get; set; }

		//[BindProperty(SupportsGet = true),
		//	Required,
		//	Display(Name = "Data wydania")]
		//public DateTime releaseDate { get; set; }

		[ForeignKey("Author"),
			BindProperty(SupportsGet = true),
			Display(Name = "Autorzy")]
		public List<Author>? authors { get; set; }
	}
}
