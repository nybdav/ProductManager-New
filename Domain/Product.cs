using System.ComponentModel.DataAnnotations;

namespace ProductManager_New.Domain
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
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