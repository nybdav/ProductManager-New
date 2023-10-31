namespace ProductManager_New.DTO;

public class CreateProductRequest
{
    public string ProductName { get; set; }

    public string SKU { get; set; }

    public string ProductDescription { get; set; }

    public string ImageURL { get; set; }

    public string Price { get; set; }

}
