
using System.ComponentModel.DataAnnotations;

namespace ProductManager_New.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string SKU { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string ImageURL { get; set; }

        [MaxLength(10)]
        public string Price { get; set; }

        public Product()
        {
        }

        public Product(string name, string sku, string description, string imageUrl, string price)
        {
            // Guard cluases för att användaren
            // inte skall kunna lämna tomma fält vid registrering:

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty or null.", nameof(name));

            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU cannot be empty or null.", nameof(sku));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty or null.", nameof(description));

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("ImageURL cannot be empty or null.", nameof(imageUrl));

            if (string.IsNullOrWhiteSpace(price))
                throw new ArgumentException("Price cannot be empty or null.", nameof(price));

            Name = name;
            SKU = sku;
            Description = description;
            ImageURL = imageUrl;
            Price = price;
        }
    }
}