using ProductManager_New.Data;
using ProductManager_New.Domain;
using static System.Console;

namespace ProductManager;

class Program
{
    static ApplicationContext context = new ApplicationContext();

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
        string name, sku, description, imageUrl, price;
        bool productExists;

        do
        {
            Clear();
            productExists = false; // Initialize the productExists flag

            Write("Namn: ");
            name = ReadLine();

            Write("SKU: ");
            sku = ReadLine();

            Write("Beskrivning: ");
            description = ReadLine();

            Write("Bild (URL): ");
            imageUrl = ReadLine();

            Write("Pris: ");
            price = ReadLine();

            var product = new Product(
                name,
                sku,
                description,
                imageUrl,
                price);

            WriteLine(); // Add a line break for spacing

            WriteLine("Är detta korrekt? (J)a (N)ej");

            var keyInfo = ReadKey(intercept: true); // Read a single key without echoing

            if (keyInfo.Key == ConsoleKey.J)
            {
                try
                {
                    // Check if the product already exists before adding it
                    if (ProductExists(sku))
                    {
                        productExists = true;
                        WriteLine("Produkt finns redan registrerad");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        AddProductToDatabase(product);
                        Clear();
                        WriteLine("Produkt sparad");
                        Thread.Sleep(2000);
                        break; // Exit the loop and return to Main
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Fel vid sparande av produkt: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                // Continue the loop to re-enter product details
            }

        } while (true && !productExists); // Continue only if the product doesn't exist
    }

    private static bool ProductExists(string sku)
    {
        // Check if the product already exists in the database
        // Return true if it exists, false otherwise
        return GetProduct(sku) != null;
    }

    private static void ShowSearchProductView()
    {
        Write("SKU: ");

        string sku = ReadLine();

        Clear();

        var product = GetProduct(sku);

        if (product != null)
        {
            while (true)
            {
                WriteLine($"Namn : {product.Name}");
                WriteLine($"SKU : {product.SKU}");
                WriteLine($"Beskrivning : {product.Description}");
                WriteLine($"Bild (URL) : {product.ImageURL}");
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
                        // Delete the product from the database
                        
                        Clear();
                        DeleteProductFromDatabase(product);
                        WriteLine("Produkt raderad");
                        Thread.Sleep(2000);
                        return; // Exit the method after deletion
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
            WriteLine("Produkt finns ej");
            Thread.Sleep(2000);
        }
    }

    private static void DeleteProductFromDatabase(Product? product)
    {
        context.Product.Remove(product);

        context.SaveChanges();
    }

    private static Product? GetProduct(string sku)
        => context.Product.FirstOrDefault(x => x.SKU == sku);

    private static void AddProductToDatabase(Product product)
    {
        context.Product.Add(product);

        context.SaveChanges();
    }
}
