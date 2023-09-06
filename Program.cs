using ProductManager_New.Data;
using ProductManager_New.Domain;
using static System.Console;

namespace ProductManager;

class Program
{
    static string connectionString = "Server=.;Database=ProductManager;Integrated Security=true;Encrypt=False";

    static ApplicationContext context = new ApplicationContext(connectionString);
    
    static void Main()
    {

        CursorVisible = false;
        Title = "Product Manager";

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

                    ShowAddNewProductView();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    ShowSearchProductView();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    private static void ShowAddNewProductView()
    {
        CursorVisible = true;

        Write("Namn: ");

        string name = ReadLine();

        Write("SKU: ");

        string sku = ReadLine();

        Write("Beskrivning: ");

        string description = ReadLine();

        Write("Bild (URL): ");

        string imageUrl = ReadLine();

        Write("Pris: ");

        string price = ReadLine();

        var product = new Product(
            name,
            sku,
            description,
            imageUrl,
            price);

        Clear();

        try
        {
            SaveProduct(product);

            WriteLine("Produkt sparad");
        }
        catch
        {
            WriteLine("Produkt finns redan registrerad");
        }

        Thread.Sleep(2000);
    }

    private static void ShowSearchProductView()
    {
        Write("SKU: ");

        string sku = ReadLine();

        Clear();

        var product = GetProduct(sku);

        if (product != null)
        {
            WriteLine($"Namn : {product.Name}");
            WriteLine($"SKU : {product.SKU}");
            WriteLine($"Beskrivning : {product.Description}");
            WriteLine($"Bild (URL) : {product.ImageURL}");
            WriteLine($"Pris : {product.Price}");

            while (ReadKey(true).Key != ConsoleKey.Escape);
        }
        else
        {
            WriteLine("Produkt finns ej");

            Thread.Sleep(2000);
        }
    }

    private static void ShowListProductsView()
    {
        Write("SKU: ");

        string sku = ReadLine();

        var registeredProducts = GetProductsBySKU(sku);

        foreach ( var product in registeredProducts )
        {
            WriteLine(
                $"{product.Name}" +
                $"{product.SKU}" +
                $"{product.Description}" +
                $"{product.ImageURL}" +
                $"{product.Price}");
        }

        while (ReadKey(true).Key != ConsoleKey.Escape) ;    
    }

    private static IEnumerable<Product> GetProductsBySKU(string sku)
        => context.Product.Where(x => x.SKU == sku);


    private static Product? GetProduct(string sku)
        => context.Product.FirstOrDefault(x => x.SKU == sku);
    

    private static void SaveProduct(Product product)
    {
        context.Product.Add(product);

        context.SaveChanges();
    }

    private static void WaitUntil(ConsoleKey escape)
    {
        while (ReadKey(true).Key != ConsoleKey.Escape) ;
    }
}









