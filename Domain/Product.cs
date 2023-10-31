
using System.ComponentModel.DataAnnotations;

namespace ProductManager_New.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(25)]
        public string SKU { get; set; }

        [MaxLength(100)]
        public string ProductDescription { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [MaxLength(10)]
        public string Price { get; set; }

        public Product()
        {
        }

        public Product(string productName, string sku, string productDescription, string imageUrl, string price)
        {
            // Guard cluases för att användaren
            // inte skall kunna lämna tomma strängar eller white-space vid registrering:

            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Name cannot be empty or null.", nameof(productName));

            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU cannot be empty or null.", nameof(sku));

            if (string.IsNullOrWhiteSpace(productDescription))
                throw new ArgumentException("Description cannot be empty or null.", nameof(productDescription));

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("ImageURL cannot be empty or null.", nameof(imageUrl));

            if (string.IsNullOrWhiteSpace(price))
                throw new ArgumentException("Price cannot be empty or null.", nameof(price));

            ProductName = productName;
            SKU = sku;
            ProductDescription = productDescription;
            ImageUrl = imageUrl;
            Price = price;
        }
    }
}