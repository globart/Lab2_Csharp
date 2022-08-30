using lab2.Contexts;
using lab2.Entities;
using lab2.FilePaths;
using lab2.Repositories;
using lab2.XmlProcessors;

namespace lab2.ConsoleProcessors;

public class Printer
{
    private readonly EntityXmlWriter _xmlWriter;
    private readonly EntityXmlReader _xmlReader;
    private readonly Paths _paths;
    private readonly Reader _reader;

    public Printer()
    {
        _xmlWriter = new EntityXmlWriter();
        _xmlReader = new EntityXmlReader();
        _paths = new Paths();
        _reader = new Reader();
    }


    public void PrintAllFiles()
    {
        try
        {
            PrintList<Dish>(_xmlReader.GetDishes(_paths.DishesXml));
            PrintList<Ingredient>(_xmlReader.GetIngredients(_paths.IngredientsXml));
            PrintList<Menu>(_xmlReader.GetMenus(_paths.MenusXml));
            PrintList<MenuItem>(_xmlReader.GetMenuItems(_paths.MenuItemsXml));
            PrintList<Product>(_xmlReader.GetProducts(_paths.ProductsXml));
            PrintList<Supplier>(_xmlReader.GetSuppliers(_paths.SuppliersXml));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void PrintList<T>(ICollection<T> list)
    {
        var type = typeof(T);
        var name = type.Name.Equals("Dish") ? $"{type.Name}es" : $"{type.Name}s";
        Console.WriteLine($"{name}:");

        if (list != null && list.Any())
        {
            foreach (var item in list)
                Console.WriteLine($"\t> {item}");
            Console.WriteLine();
        }
        else
            Console.WriteLine("No one.\n");
    }

    public void PrintEnteringEntities()
    {
        try
        {
            Console.WriteLine("\tCreate new Dish");
            var dish = _reader.ReadDish();
            _xmlWriter.AddElement(dish, _paths.DishesXml);
            Console.WriteLine();

            Console.WriteLine("\tCreate new Supplier");
            var supplier = _reader.ReadSupplier();
            _xmlWriter.AddElement(supplier, _paths.SuppliersXml);
            Console.WriteLine();

            Console.WriteLine("\tCreate new Product");
            var product = _reader.ReadProduct();
            _xmlWriter.AddElement(product, _paths.ProductsXml);
            Console.WriteLine();

            Console.WriteLine("\tCreate new Menu");
            var menu = _reader.ReadMenu();
            _xmlWriter.AddElement(menu, _paths.MenusXml);
            Console.WriteLine();

            Console.WriteLine("\tCreate new Ingredient");
            var ingredient = _reader.ReadIngredient();
            _xmlWriter.AddElement(ingredient, _paths.IngredientsXml);
            Console.WriteLine();

            Console.WriteLine("\tCreate new MenuItem");
            var menuItem = _reader.ReadMenuItem();
            _xmlWriter.AddElement(menuItem, _paths.MenuItemsXml);
            Console.WriteLine();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }        
    }


    public void PrintQueries()
    {
        var context = new Context();
        var queries = new QueriesRepository(context);
        var printer = new QueriesPrinter(queries);

        Console.WriteLine("1. All menus");
        printer.PrintAllMenus();

        Console.WriteLine("\n2. The newest menu\n");
        printer.PrintNewestMenu();

        Console.WriteLine("\n3. All dishes with calories\n");
        printer.PrintDishesWithCalories();

        Console.WriteLine("\n4. All dishes with ingredients\n");
        printer.PrintDishesWithIngredients();

        Console.WriteLine("\n5. The most caloric product\n");
        printer.PrintMostCaloricProduct();

        Console.WriteLine("\n6. All dishes are sorted by most expensive price ever\n");
        printer.PrintDishesSortedByMostExpensivePriceEver();

        Console.WriteLine("\n7. All products are sorted by usages\n");
        printer.PrintProductsSortedByUsages();

        Console.WriteLine("\n8. The least caloric dish\n");
        printer.PrintTheLeastCaloricDish();

        Console.WriteLine("\n9. All dishes with calories less than 100\n");
        printer.PrintDishesWithCaloriesLessThan(100);

        Console.WriteLine("\n10. All products with dishes where they are used\n");
        printer.PrintProductsWithDishesWhereUsed();

        Console.WriteLine("\n11. All products with suppliers\n");
        printer.PrintProductsWithSuppliers();

        Console.WriteLine("\n12. The most used product\n");
        printer.PrintMostUsedProduct();

        Console.WriteLine("\n13. The average product calories\n");
        printer.PrintAverageProductCalories();

        Console.WriteLine("\n14. All menus have been created since two weeks ago\n");
        printer.PrintMenusCreatedAfter(DateTime.Now.Date.Subtract(TimeSpan.FromDays(14)));

        Console.WriteLine("\n15. The dish with lowest price ever\n");
        printer.PrintDishWithLowestPriceEver();

        Console.WriteLine("\n16. All product and supplier names");
        printer.PrintProductAndSupplierNames();

        Console.WriteLine("\n17. All dishes sorted by name");
        printer.PrintDishesSortedByName();

        Console.WriteLine();
    }
}
