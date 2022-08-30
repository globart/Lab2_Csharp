using lab2.Entities;
using lab2.FilePaths;
using lab2.XmlProcessors;

namespace lab2.ConsoleProcessors
{
    public class Reader
    {
        private readonly EntityXmlReader _xmlReader;
        private readonly Paths _paths;

        public Reader()
        {
            _xmlReader = new EntityXmlReader();
            _paths = new Paths();
        }

        public Dish ReadDish()
        {
            Console.Write("Please, enter the name: ");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("The name must consist of symbols. Please, enter the name: ");
                name = Console.ReadLine();
            }

            var dish = _xmlReader.GetDishes(_paths.DishesXml).MaxBy(d => d.Id);

            var id = dish?.Id ?? 1;

            return new Dish()
            {
                Id = id+1,
                Name = name,
            };
        }

        public Supplier ReadSupplier()
        {
            Console.Write("Please, enter the name: ");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("The name must consist of symbols. Please, enter the name: ");
                name = Console.ReadLine();
            }

            var supplier = _xmlReader.GetSuppliers(_paths.SuppliersXml).MaxBy(d => d.Id);

            int id = supplier?.Id ?? 1;

            return new Supplier()
            {
                Id = id+1,
                Name = name,
            };
        }

        public Product ReadProduct()
        {
            Console.Write("Please, enter the name: ");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("The name must consist of symbols. Please, enter the name: ");
                name = Console.ReadLine();
            }

            var product = _xmlReader.GetProducts(_paths.ProductsXml).MaxBy(d => d.Id);

            var id = product?.Id ?? 1;

            Console.Write("Please, enter calories per 100 grams: ");
            var caloriesStr = Console.ReadLine();
            double calories;

            while(!double.TryParse(caloriesStr, out calories) || calories <= 0)
            {
                Console.Write("The value must be a real number. Please, enter calories per 100 grams: ");
                caloriesStr = Console.ReadLine();
            }

            Console.Write("Please, enter supplier's id: ");
            var supplierIdStr = Console.ReadLine();
            int supplierId;

            var supplierIds = _xmlReader.GetSuppliers(_paths.SuppliersXml).Select(s => s.Id);            

            while (!int.TryParse(supplierIdStr, out supplierId) ||
                     supplierIds.FirstOrDefault(i => i == supplierId) == 0)
            {
                Console.Write("The value is incorrect. Please, enter supplier's id: ");
                supplierIdStr = Console.ReadLine();
            }


            return new Product()
            {
                Id = id+1,
                Name = name,
                CaloriesPer100Grams = calories,
                SupplierId = supplierId,
            };
        }

        public Menu ReadMenu()
        {
            Console.Write("Please, enter the date: ");
            var dateStr = Console.ReadLine();
            DateTime date;

            while (!DateTime.TryParse(dateStr, out date))
            {
                Console.Write("The value is incorrect. Please, enter the date: ");
                dateStr = Console.ReadLine();
            }

            var menu = _xmlReader.GetMenus(_paths.MenusXml).MaxBy(d => d.Id);

            var id = menu?.Id ?? 1;

            return new Menu()
            {
                Id = id+1,
                Date = date,
            };
        }

        public Ingredient ReadIngredient()
        {
            Console.Write("Please, enter dish id: ");
            var dishIdStr = Console.ReadLine();
            int dishId;

            var dishIds = _xmlReader.GetDishes(_paths.DishesXml).Select(d => d.Id);

            while (!int.TryParse(dishIdStr, out dishId) ||
                     dishIds.FirstOrDefault(i => i == dishId) == 0)
            {
                Console.Write("The value is incorrect. Please, enter dish id: ");
                dishIdStr = Console.ReadLine();
            }

            Console.Write("Please, enter product id: ");
            var productIdStr = Console.ReadLine();
            int productId;

            var productIds = _xmlReader.GetProducts(_paths.ProductsXml).Select(s => s.Id);

            while (!int.TryParse(productIdStr, out productId) ||
                     productIds.FirstOrDefault(i => i == productId) == 0)
            {
                Console.Write("The value is incorrect. Please, enter product id: ");
                productIdStr = Console.ReadLine();
            }

            var ingredient = _xmlReader.GetIngredients(_paths.IngredientsXml)
                                                          .Where(i => i.DishId == dishId)
                                                          .FirstOrDefault(i => i.ProductId == productId);
            if (ingredient is not null)
            {
                Console.WriteLine("\tSuch ingredient already exist. Try again.");
                ReadIngredient();
            }

            Console.Write("Please, enter grams: ");
            var gramsStr = Console.ReadLine();
            double grams;

            while (!double.TryParse(gramsStr, out grams) || grams <= 0)
            {
                Console.Write("The value must be a real number and bigger than zero. Please, enter grams: ");
                gramsStr = Console.ReadLine();
            }   

            return new Ingredient()
            {
                DishId = dishId,
                ProductId = productId,
                Grams = grams,
            };
        }

        public MenuItem ReadMenuItem()
        {
            Console.Write("Please, enter dish id: ");
            var dishIdStr = Console.ReadLine();
            int dishId;

            var dishIds = _xmlReader.GetDishes(_paths.DishesXml).Select(d => d.Id);

            while (!int.TryParse(dishIdStr, out dishId) ||
                     dishIds.FirstOrDefault(i => i == dishId) == 0)
            {
                Console.Write("The value is incorrect. Please, enter dish id: ");
                dishIdStr = Console.ReadLine();
            }

            Console.Write("Please, enter menu id: ");
            var menuIdStr = Console.ReadLine();
            int menuId;

            var menuIds = _xmlReader.GetMenus(_paths.MenusXml).Select(s => s.Id);

            while (!int.TryParse(menuIdStr, out menuId) ||
                     menuIds.FirstOrDefault(i => i == menuId) == 0)
            {
                Console.Write("The value is incorrect. Please, enter menu id: ");
                menuIdStr = Console.ReadLine();
            }

            var menuItem = _xmlReader.GetMenuItems(_paths.MenuItemsXml)
                                     .Where(mi => mi.DishId == dishId)
                                     .FirstOrDefault(mi => mi.MenuId == menuId);

            if (menuItem is not null)
            {
                Console.WriteLine("\tSuch menu item already exist. Try again.");
                ReadMenuItem();
            }

            Console.Write("Please, enter price: ");
            var priceStr = Console.ReadLine();
            decimal price;

            while (!decimal.TryParse(priceStr, out price) || price <= 0)
            {
                Console.Write("The value is incorrect. Please, enter price: ");
                priceStr = Console.ReadLine();
            }

            return new MenuItem()
            {
                DishId = dishId,
                MenuId = menuId,
                Price = price,
            };
        }

    }
}
