using lab2.Entities;
using lab2.Repositories;
using lab2.ViewModels;

namespace lab2.ConsoleProcessors;

public class QueriesPrinter
{
    private readonly QueriesRepository _queries;

    public QueriesPrinter(QueriesRepository queries)
    {
        _queries = queries;
    }

    public void PrintAllMenus()
    {
        var collection = _queries.GetAllMenus();
        if (collection != null)
            PrintMenus(collection);
    }

    public void PrintProductAndSupplierNames()
    {
        var collection = _queries.GetProductAndSupplierNames();

        if (collection == null) return;

        foreach (var element in collection)
        {
            Console.WriteLine(element);
        }
    }

    public void PrintDishesSortedByName()
    {
        PrintDishes(_queries.GetDishesSortedByName());
    }

    public void PrintNewestMenu()
    {
        var menu = _queries.GetNewestMenu();
        if (menu is not null)
        {
            Console.WriteLine(menu.Menu);

            if (menu.Dishes == null)
            {
                Console.WriteLine("No dishes.");
                return;
            }

            foreach (var dish in menu.Dishes)
            {
                Console.WriteLine($"{dish.Dish} - {dish.Price}");
            }
        }
    }

    public void PrintDishesWithCalories()
    {
        var dishes = _queries.GetDishesWithCalories();

        if (dishes == null) return;

        foreach (var dish in dishes)
        {
            Console.WriteLine($"{dish.Dish} - {dish.Calories.ToString(".##")}");
        }
    }

    public void PrintDishesWithIngredients()
    {
        var dishes = _queries.GetDishesWithIngredients();

        if(dishes == null) return;  

        foreach (var dish in dishes)
        {
            Console.WriteLine("Dish:");
            Console.WriteLine(dish.Dish);
            Console.WriteLine("Ingredients:");

            if(dish.Products == null)
            {
                Console.WriteLine("No ingredients.");
                return;
            }

            foreach (var product in dish.Products)
            {
                Console.WriteLine(product);
            }
        }
    }

    public void PrintMostCaloricProduct()
    {
        if(_queries.GetMostCaloricProduct() == null) return;
        Console.WriteLine(_queries.GetMostCaloricProduct());
    }

    public void PrintDishesSortedByMostExpensivePriceEver()
    {
        var collection = _queries.GetDishesSortedByMostExpensivePriceEver();
        if (collection == null) return;
        PrintDishes(collection);
    }

    public void PrintProductsSortedByUsages()
    {
        var products = _queries.GetProductsSortedByUsages();

        if(products == null) return;

        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }

    public void PrintTheLeastCaloricDish()
    {
        Console.WriteLine(_queries.GetTheLeastCaloricDish());
    }

    public void PrintDishesWithCaloriesLessThan(double calories)
    {
        PrintDishes(_queries.GetDishesWithCaloriesLessThan(calories));
    }

    public void PrintProductsWithDishesWhereUsed()
    {
        var products = _queries.GetProductsWithDishesWhereUsed();

        if(products == null) return;

        foreach (var product in products)
        {
            Console.WriteLine("Product:");
            Console.WriteLine(product.Product);
            Console.WriteLine("Dishes:");

            if(product.Dishes == null)
            {
                Console.WriteLine("No dishes.");
                return;
            }

            foreach (var dish in product.Dishes)
            {
                Console.WriteLine(dish);
            }
        }
    }

    public void PrintProductsWithSuppliers()
    {
        var products = _queries.GetProductsWithSuppliers();

        if (products == null) return;

        foreach (var product in products)
        {
            Console.WriteLine(product.Product);
            Console.WriteLine($"Supplier: {product.Supplier}");
        }
    }

    public void PrintMostUsedProduct()
    {
        Console.WriteLine(_queries.GetMostUsedProduct());
    }

    public void PrintAverageProductCalories()
    {
        try
        {
            Console.WriteLine(_queries.GetAverageProductCalories());
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("It's not possible to calculate average product calories without products");
        }
    }

    public void PrintMenusCreatedAfter(DateTime dateTime)
    {
        PrintMenus(_queries.GetMenusCreatedAfter(dateTime));
    }

    public void PrintDishWithLowestPriceEver()
    {
        Console.WriteLine(_queries.GetDishWithLowestPriceEver());
    }

    private void PrintMenus(IEnumerable<MenuWithDishes> menus)
    {
        foreach (var menu in menus)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine(menu.Menu);

            if(menu.Dishes == null)
            {
                Console.WriteLine("Menu is empty.");
                return;
            }

            foreach (var dish in menu.Dishes)
            {
                Console.WriteLine($"{dish.Dish} - {dish.Price}");
            }
        }
    }

    private void PrintDishes(IEnumerable<Dish?> dishes)
    {
        foreach (var dish in dishes)
        {
            Console.WriteLine(dish);
        }
    }
}