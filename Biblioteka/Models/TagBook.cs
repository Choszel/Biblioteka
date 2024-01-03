using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    [Keyless]
    public class TagBook
    {
        public int tagId { get; set; }
        public int bookId { get; set; }

        [ForeignKey("tagId")]
        public virtual Tag tag { get; set; }

        [ForeignKey("bookId")]
        public virtual Book book { get; set; }
    }
}
