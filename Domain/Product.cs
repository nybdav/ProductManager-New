
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
            Name = name;
            SKU = sku;
            Description = description;
            ImageURL = imageUrl;
            Price = price;
        }
    }
}