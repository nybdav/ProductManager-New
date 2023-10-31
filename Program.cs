using ProductManager_New.Domain;
using ProductManager_New.DTO;
using System.Text;
using System.Text.Json;
using static System.Console;

namespace ProductManager;

class Program
{
    static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:8000/")
    };

    static void Main()
    {

        Title = "Product Manager";
        CursorVisible = false;

        while (true)
        {

            WriteLine("1. Ny Produkt");
            WriteLine("2. Sök Produkt");
            WriteLine("3. Avsluta");

            var keyPressed = ReadKey(true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    AddNewProductView();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    SearchProductView();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    private static void AddNewProductView()
    {

        do
        {
            Clear();

            Write("Namn: ");
            string productName = ReadLine();


            Write("SKU: ");
            string sku = ReadLine();


            Write("Beskrivning: ");
            string productDescription = ReadLine();


            Write("Bild (URL): ");
            string imageUrl = ReadLine();


            Write("Pris: ");
            string price = ReadLine();

            
            WriteLine(); // Add a line break for spacing


            WriteLine("Är detta korrekt? (J)a (N)ej");


            var keyInfo = ReadKey(true);

            if (keyInfo.Key == ConsoleKey.J)
            {
                try
                {
                    var product = new Product(productName, sku, productDescription, imageUrl, price);

                    if (ProductExists(sku))
                    {
                        WriteLine("Produkt finns redan registrerad");

                        Thread.Sleep(2000);
                    }
                    else
                    {
                        SaveProduct(product);

                        Clear();

                        WriteLine("Produkt sparad");

                        Thread.Sleep(2000);
                        break; // Exit the loop and return to Main
                    }
                }
                catch (ArgumentException ex)
                {
                    WriteLine($"Ogiltig information: {ex.Message}");
                    Thread.Sleep(7000);
                }

            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                // Continue the loop to re-enter product details
            }

        } while (true);
    }

    private static bool ProductExists(string sku)
    {
        // Check if the product already exists in the database
        // Return true if it exists, false otherwise
        return GetProduct(sku) != null;
    }

    private static void SearchProductView()
    {
        Write("SKU: ");

        string sku = ReadLine();

        Clear();

        var product = GetProduct(sku);

        if (product != null)
        {
            while (true)
            {
                WriteLine($"Namn : {product.ProductName}");
                WriteLine($"SKU : {product.SKU}");
                WriteLine($"Beskrivning : {product.ProductDescription}");
                WriteLine($"Bild (URL) : {product.ImageUrl}");
                WriteLine($"Pris : {product.Price}");

                CursorVisible = false;

                WriteLine();
                WriteLine("(R)adera");

                var key = ReadKey(true).Key;
                if (key == ConsoleKey.R)
                {
                    WriteLine();
                    WriteLine("Radera Produkt? (J)a (N)ej");
                    key = ReadKey(true).Key;
                    if (key == ConsoleKey.J)
                    {
                        
                        // Radera produkt från databas via API:et
                        Clear();
                        bool deleted = DeleteProduct(sku);
                        if (deleted)
                        {
                            WriteLine("Produkt raderad");
                        }
                        else
                        {
                            WriteLine("Misslyckades med att radera produkten.");
                        }
                        Thread.Sleep(2000);
                        return; // Avsluta metod efter radering
                    }
                    else if (key == ConsoleKey.N)
                    {
                        Clear();
                        // Continue to run the loop to view product details
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        else
        {
            WriteLine("Produkt saknas");
            Thread.Sleep(2000);
        }
    }

    // HTTP DELETE - https://localhost:8000/products/{sku}
    private static bool DeleteProduct(string sku)
    {
        var response = httpClient.DeleteAsync($"products/{sku}").Result;

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            WriteLine("Misslyckades med att radera produkt.");
            return false;
        }
     
    }

    // HTTP GET - https://localhost:8000/products/{sku}
    private static Product? GetProduct(string sku)
    {

        string apiUrl = $"products/{sku}";

        var response = httpClient.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            var json = response.Content.ReadAsStringAsync().Result;

            var serializeOptions = new JsonSerializerOptions

            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var productDto = JsonSerializer.Deserialize<ProductDTO>(json, serializeOptions);

            if (productDto != null)
            {
                var product = new Product
                {
                    Id = productDto.Id,
                    ProductName = productDto.ProductName,
                    SKU = productDto.SKU,
                    ProductDescription = productDto.ProductDescription,
                    ImageUrl = productDto.ImageURL,
                    Price = productDto.Price
                };

                return product;
            }
        }

        return null;
    }

    // HTTP POST - https://localhost:8000/products
    private static void SaveProduct(Product product)
    {
        var createVehicleRequest = new CreateProductRequest
        {
            ProductName = product.ProductName,
            SKU = product.SKU,
            ProductDescription = product.ProductDescription,
            ImageURL = product.ImageUrl,
            Price = product.Price,
        };

        var json = JsonSerializer.Serialize(createVehicleRequest);

        var body = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = httpClient.PostAsync("products", body).Result;


        // Kasta exception om statuskoden inte ligger inom 2xx-omfånget.
        response.EnsureSuccessStatusCode();
    }
}
