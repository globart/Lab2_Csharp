using lab2.Entities;
using lab2.FilePaths;
using System.Xml;

namespace lab2.Seeders;

public class DataSeeder
{
    private readonly Paths _paths;
    private readonly MenuData _menuData;

    public DataSeeder()
    {
        _paths = new Paths();
        _menuData = new MenuData();
        _menuData.Seed();
    }

    public void Seed()
    {
        SeedList<Dish>(_paths.DishesXml, _menuData.Dishes);
        SeedList<Ingredient>(_paths.IngredientsXml, _menuData.Ingredients);
        SeedList<Menu>(_paths.MenusXml, _menuData.Menus);
        SeedList<MenuItem>(_paths.MenuItemsXml, _menuData.MenuItems);
        SeedList<Product>(_paths.ProductsXml, _menuData.Products);
        SeedList<Supplier>(_paths.SuppliersXml, _menuData.Suppliers);
    }

    private static void SeedList<T>(string path, ICollection<T> list) where T : class
    {
        var settings = new XmlWriterSettings
        {
            Indent = true
        };

        var type = typeof(T);
        var properties = type.GetProperties();

        using (var xmlWriter = XmlWriter.Create(path, settings))
        {
            var root = type.Name.Equals("Dish") ? $"{type.Name}es" : $"{type.Name}s";
            xmlWriter.WriteStartElement(root);

            foreach (var item in list)
            {
                xmlWriter.WriteStartElement($"{type.Name}");

                foreach (var property in properties)
                {
                    var value = property.GetValue(item)?.ToString();
                    xmlWriter.WriteElementString(property.Name, value);
                }

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }
    }
}
