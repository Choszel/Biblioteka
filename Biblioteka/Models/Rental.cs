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

        [DisplayName("Id użytkownika")]
        public int userId { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Data wypożyczenia"),
            Required(ErrorMessage = "Pole \"Data wypożyczenia\" jest wymagane!")]
        public DateTime rentalDate { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Stan wypożyczenia"),
            MaxLength(15, ErrorMessage = "Stan wypożyczenia nie może zawierać więcej niż 15 znaków"),
            Required(ErrorMessage = "Pole \"Stan wypożyczenia\" jest wymagane!")]
        public string rentalState { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Data zmiany stanu"),
            Required(ErrorMessage = "Pole \"Data zmiany stanu\" jest wymagane!")]
        public DateTime stateDate { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Miasto"),
            MaxLength(30, ErrorMessage = "Miasto nie może zawierać więcej niż 30 znaków"),
            Required(ErrorMessage = "Pole \"Miasto\" jest wymagane!")]
        public string rentalCity { get; set; }

        [BindProperty(SupportsGet = true),
            DisplayName("Ulica, numer domu"),
            MaxLength(50, ErrorMessage = "Ulica nie może zawierać więcej niż 50 znaków"),
            Required(ErrorMessage = "Pole \"Ulica, numer domu\" jest wymagane!")]
        public string rentalStreet { get; set; }


        //[ForeignKey("userId")]
        //public virtual ??? user { get; set; }

        [ForeignKey("Book"),
            Display(Name = "Książki"),
            BindProperty]
        public List<Book>? book { get; set; }
    }
}
