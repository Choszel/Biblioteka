using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace Biblioteka.Models
{
    [Resource]
    public class User : Identifiable<int>
    {
        [Attr]
        public List<Product> products { get; set; } = null!;
    }

    public class Product
    {
        public int productId { get; set; }
        public int quantity { get; set; }

        public Product(int productId, int quantity)
        {
            this.productId = productId;
            this.quantity = quantity;
        }
    }
}
