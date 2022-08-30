using lab2.Entities;

namespace lab2.Seeders;

public class MenuData
{
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public void Seed()
    {
        Products = SeedProducts();
        Dishes = SeedDishes();
        Ingredients = SeedIngredients();
        Menus = SeedMenus();
        MenuItems = SeedMenuItems();
        Suppliers = SeedSuppliers();
    }

    private ICollection<Product> SeedProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Name = "Orange",
                CaloriesPer100Grams = 47.1,
                SupplierId = 1
            },
            new Product()
            {
                Id = 2,
                Name = "Beef",
                CaloriesPer100Grams = 250.5,
                SupplierId = 1
            },
            new Product()
            {
                Id = 3,
                Name = "Egg",
                CaloriesPer100Grams = 155.1,
                SupplierId = 1
            },
            new Product()
            {
                Id = 4,
                Name = "Milk",
                CaloriesPer100Grams = 42.3,
                SupplierId = 2
            },
            new Product()
            {
                Id = 5,
                Name = "Potato",
                CaloriesPer100Grams = 76.7,
                SupplierId = 2
            }
        };
    }

    private ICollection<Dish> SeedDishes()
    {
        return new List<Dish>()
        {
            new Dish()
            {
                Id = 1,
                Name = "Orange juice"
            },
            new Dish()
            {
                Id = 2,
                Name = "Steak"
            },
            new Dish()
            {
                Id = 3,
                Name = "Omelet"
            },
            new Dish()
            {
                Id = 4,
                Name = "Mashed potatoes"
            },
            new Dish()
            {
                Id = 5,
                Name = "Fried eggs"
            }
        };
    }

    private ICollection<Ingredient> SeedIngredients()
    {
        return new List<Ingredient>()
        {
            new Ingredient()
            {
                DishId = 1,
                ProductId = 1,
                Grams = 80
            },
            new Ingredient()
            {
                DishId = 2,
                ProductId = 2,
                Grams = 100
            },
            new Ingredient()
            {
                DishId = 3,
                ProductId = 3,
                Grams = 100
            },
            new Ingredient()
            {
                DishId = 3,
                ProductId = 4,
                Grams = 50
            },
            new Ingredient()
            {
                DishId = 4,
                ProductId = 5,
                Grams = 100
            },
            new Ingredient()
            {
                DishId = 5,
                ProductId = 3,
                Grams = 75
            }
        };
    }

    private ICollection<Menu> SeedMenus()
    {
        return new List<Menu>()
        {
            new Menu()
            {
                Id = 1,
                Date = DateTime.Now.Date.Subtract(TimeSpan.FromDays(7))
            },
            new Menu()
            {
                Id = 2,
                Date = DateTime.Now.Date.Subtract(TimeSpan.FromDays(30))
            }
        };
    }

    private ICollection<MenuItem> SeedMenuItems()
    {
        return new List<MenuItem>()
        {
            new MenuItem()
            {
                MenuId = 1,
                DishId = 1,
                Price = 20.50m,
            },
            new MenuItem()
            {
                MenuId = 1,
                DishId = 2,
                Price = 100m,
            },
            new MenuItem()
            {
                MenuId = 2,
                DishId = 1,
                Price = 50m,
            },
            new MenuItem()
            {
                MenuId = 2,
                DishId = 3,
                Price = 70.50m,
            },
            new MenuItem()
            {
                MenuId = 2,
                DishId = 4,
                Price = 60m,
            },
            new MenuItem()
            {
                MenuId = 2,
                DishId = 5,
                Price = 45m
            }
        };
    }

    private ICollection<Supplier> SeedSuppliers()
    {
        return new List<Supplier>()
        {
            new Supplier()
            {
                Id = 1,
                Name = "Gordon Food Service"
            },
            new Supplier()
            {
                Id = 2,
                Name = "McLane FoodService"
            }
        };
    }
}