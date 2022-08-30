using lab2.Contexts;
using lab2.Entities;
using lab2.Parsers;
using lab2.ViewModels;

namespace lab2.Repositories;

public class QueriesRepository
{
    private readonly Context _context;

    public QueriesRepository(Context menuContext)
    {
        _context = menuContext;
    }

    public IEnumerable<MenuWithDishes>? GetAllMenus()
    {
        if (_context.MenuItemsXml.Root == null || 
            _context.DishesXml.Root == null) 
            return null;

        var query = _context.MenusXml.Root?.Elements()
                    .GroupJoin(_context.MenuItemsXml.Root.Elements(),
                        m => m.Element("Id")?.Value,
                        i => i.Element("MenuId")?.Value,
                        (menu, items) => new MenuWithDishes()
                        {
                            Menu = menu.ToMenu(),
                            Dishes = items.Join(_context.DishesXml.Root.Elements(),
                                i => i.Element("DishId")?.Value,
                                d => d.Element("Id")?.Value,
                                (item, dish) => new DishWithPrice()
                                {
                                    Dish = dish.ToDish(),
                                    Price = item.Element("Price").ToDecimal()
                                })
                        });
        return query;
    }

    public MenuWithDishes? GetNewestMenu()
    {
        return GetAllMenus()?
            .MaxBy(m => m.Menu?.Date);
    }

    public IEnumerable<DishWithCalories>? GetDishesWithCalories()
    {
        if (_context.IngredientsXml.Root == null || 
            _context.ProductsXml.Root == null) 
            return null;

        var query = _context.DishesXml.Root?.Elements()
            .GroupJoin(_context.IngredientsXml.Root.Elements(),
                d => d.Element("Id")?.Value,
                i => i.Element("DishId")?.Value,
                (dish, ingredients) => new DishWithCalories()
                {
                    Dish = dish.ToDish(),
                    Calories = ingredients.Join(_context.ProductsXml.Root.Elements(),
                            i => i.Element("ProductId")?.Value,
                            p => p.Element("Id")?.Value,
                            (ingredient, product) => new
                            {
                                ingredient,
                                product
                            })
                        .Sum(r => r.ingredient.Element("Grams").ToDouble() *
                                  r.product.Element("CaloriesPerGram").ToDouble())
                });
        
        return query;
    }

    public IEnumerable<DishWithProducts>? GetDishesWithIngredients()
    {
        if (_context.IngredientsXml.Root == null || 
            _context.ProductsXml.Root == null)
            return null;

        var query = _context.DishesXml.Root?.Elements()
            .GroupJoin(_context.IngredientsXml.Root.Elements(),
                d => d.Element("Id")?.Value,
                i => i.Element("DishId")?.Value,
                (dish, ingredients) => new DishWithProducts()
                {
                    Dish = dish.ToDish(),
                    Products = ingredients.Join(_context.ProductsXml.Root.Elements(),
                        i => i.Element("ProductId")?.Value,
                        p => p.Element("Id")?.Value,
                        (ingredient, product) => product.ToProduct())
                });

        return query;
    }

    public Product? GetMostCaloricProduct()
    {
        return _context.ProductsXml.Root?.Elements()
            .MaxBy(p => p.Element("CaloriesPer100Grams").ToDouble())
            .ToProduct();
    }

    public IEnumerable<Dish?>? GetDishesSortedByMostExpensivePriceEver()
    {
        if(_context.DishesXml.Root == null) return null;

        var query = _context.MenuItemsXml.Root?.Elements()
                    .Join(_context.DishesXml.Root.Elements(),
                        i => i.Element("DishId")?.Value,
                        d => d.Element("Id")?.Value,
                        (item, dish) => new
                        {
                            item,
                            dish
                        })
                    .OrderByDescending(r => r.item.Element("Price").ToDecimal())
                    .Select(r => r.dish.ToDish())
                    .DistinctBy(d => d?.Id);
        return query;
    }

    public IEnumerable<Product?>? GetProductsSortedByUsages()
    {
        if(_context.IngredientsXml.Root == null)
            return null;

        var query = _context.ProductsXml.Root?.Elements()
                    .GroupJoin(_context.IngredientsXml.Root.Elements(),
                        p => p.Element("Id")?.Value,
                        i => i.Element("ProductId")?.Value,
                        (product, ingredients) => new
                        {
                            product,
                            UsagesQuantity = ingredients.Count()
                        })
                    .OrderByDescending(r => r.UsagesQuantity)
                    .Select(r => r.product.ToProduct());

        return query;
    }

    public Dish? GetTheLeastCaloricDish()
    {
        return GetDishesWithCalories()?
            .OrderBy(d => d.Calories)
            .Select(d => d.Dish)
            .FirstOrDefault();
    }

    public IEnumerable<Dish> GetDishesWithCaloriesLessThan(double calories)
    {
        return from dish in GetDishesWithCalories()
               where dish.Calories < calories
               select dish.Dish;
    }

    public IEnumerable<Dish> GetDishesSortedByName()
    {
        return from dish in _context.DishesXml.Root?.Elements()
               orderby dish.Name
               select dish.ToDish();
    }

    public IEnumerable<ProductWithDishes>? GetProductsWithDishesWhereUsed()
    {
        if(_context.ProductsXml.Root == null ||
            _context.DishesXml.Root == null)
            return null;

        return _context.IngredientsXml.Root?.Elements()
            .GroupBy(i => i.Element("ProductId")?.Value)
            .Select(r => new ProductWithDishes()
            {
                Product = _context.ProductsXml.Root.Elements()
                                  .FirstOrDefault(p => p.Element("Id")?.Value == r.Key)
                                  .ToProduct(),
                Dishes = r.Join(_context.DishesXml.Root.Elements(),
                    i => i.Element("DishId")?.Value,
                    d => d.Element("Id")?.Value,
                    (ingredient, dish) => dish.ToDish())
            });
    }

    public IEnumerable<ProductWithSupplier>? GetProductsWithSuppliers()
    {
        if (_context.SuppliersXml.Root == null)
            return null;

        return from product in _context.ProductsXml.Root?.Elements()
               join supplier in _context.SuppliersXml.Root.Elements()
                   on product.Element("SupplierId")?.Value equals supplier.Element("Id")?.Value
               select new ProductWithSupplier()
               {
                   Product = product.ToProduct(),
                   Supplier = supplier.ToSupplier()
               };
    }

    public Product? GetMostUsedProduct()
    {
        return GetProductsWithDishesWhereUsed()?
            .OrderByDescending(p => p.Dishes.Count())
            .Select(p => p.Product)
            .FirstOrDefault();
    }

    public double GetAverageProductCalories()
    {
        if (_context.ProductsXml.Root == null) return 0;

        return _context.ProductsXml.Root.Elements()
                .Average(p => p.Element("CaloriesPer100Grams").ToDouble());
    }

    public IEnumerable<MenuWithDishes> GetMenusCreatedAfter(DateTime dateTime)
    {
        return from menu in GetAllMenus()
               where menu.Menu?.Date > dateTime
               select menu;
    }

    public Dish? GetDishWithLowestPriceEver()
    {
        return GetDishesSortedByMostExpensivePriceEver()?
            .LastOrDefault();
    }

    public IEnumerable<string?>? GetProductAndSupplierNames()
    {
        if (_context.SuppliersXml.Root == null)
            return null;

        var query =  _context.ProductsXml.Root?.Elements()
                    .Select(p => p.Element("Name")?.Value)
                    .Concat(_context.SuppliersXml.Root.Elements()
                        .Select(s => s.Element("Name")?.Value));

        return query;
    }
}