using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    [Keyless]
    public class RentalBook
    {
        public RentalBook(int rentalId, int bookId)
        {
            this.rentalId = rentalId;
            this.bookId = bookId;
        }

        public int rentalId { get; set; }
        public int bookId { get; set; }

        [ForeignKey("rentalId")]
        public virtual Rental rental { get; set; } 
        
        [ForeignKey("bookId")]
        public virtual Book book { get; set; }
    }
}
