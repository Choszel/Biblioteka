using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Biblioteka.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id wypożyczenia")]
        public int rentalId { get; set; }

        [BindProperty(SupportsGet = true),
            Display(Name = "Id użytkownika"),      
            Required(ErrorMessage = "Pole \"Id użytkownika\" jest wymagane!")]
        public int? userId { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Data wypożyczenia"),
            Required(ErrorMessage = "Pole \"Data wypożyczenia\" jest wymagane!")]
        public DateTime? rentalDate { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Stan wypożyczenia"),
            MaxLength(15, ErrorMessage = "Stan wypożyczenia nie może zawierać więcej niż 15 znaków"),
            Required(ErrorMessage = "Pole \"Stan wypożyczenia\" jest wymagane!")]
        public string? rentalState { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Data zmiany stanu"),
            Required(ErrorMessage = "Pole \"Data zmiany stanu\" jest wymagane!")]
        public DateTime? stateDate { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Miasto"),
            MaxLength(30, ErrorMessage = "Miasto nie może zawierać więcej niż 30 znaków"),
            Required(ErrorMessage = "Pole \"Miasto\" jest wymagane!")]
        public string? rentalCity { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Ulica"),
            MaxLength(40, ErrorMessage = "Ulica nie może zawierać więcej niż 40 znaków"),
            Required(ErrorMessage = "Pole \"Ulica\" jest wymagane!")]
        public string? rentalStreet { get; set; }

        [BindProperty(SupportsGet = true),
            Column(TypeName = "NUMERIC(3)"),
            Display(Name = "Nr mieszkania"),
            Range(0, 999, ErrorMessage = "Numer mieszkania nie może być większy niż 999")]
        public int? houseNumber { get; set; }

        [BindProperty(SupportsGet = true),
           Required(ErrorMessage = "Pole \"Kod pocztowy\" jest wymagane!"),
           Display(Name = "Kod pocztowy"),
           MaxLength(6, ErrorMessage = "Kod pocztowy nie może zawierać więcej niż 6 znaków")]
        public string? zipCode { get; set; }

        [ForeignKey("userId"),
            Display(Name = "Użytkownik")]
        public virtual Reader user { get; set; }

        [ForeignKey("Book"),
            Display(Name = "Książki")]        
        public List<Book>? book { get; set; }
    }
}
