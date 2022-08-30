using lab2.Entities;
using lab2.Parsers;
using System.Xml;

namespace lab2.XmlProcessors;

public class EntityXmlReader
{
    public ICollection<Dish> GetDishes(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filename);

        var result = new List<Dish>();

        if(xmlDoc.DocumentElement == null)
            throw new ArgumentNullException("The root element cannot be null.");

        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            result.Add(new Dish()
            {
                Id = node["Id"] is null ? 0 : node["Id"].ToInt(),
                Name = node["Name"] is null ? string.Empty : node["Name"].InnerText
            });
        }

        return result;
    }

    public ICollection<Ingredient> GetIngredients(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filename);

        var result = new List<Ingredient>();

        if (xmlDoc.DocumentElement == null)
            throw new ArgumentNullException("The root element cannot be null.");

        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            result.Add(new Ingredient()
            {
                DishId = node["DishId"] is null ? 0 : node["DishId"].ToInt(),
                ProductId = node["ProductId"] is null ? 0 : node["ProductId"].ToInt(),
                Grams = node["Grams"] is null ? 0 : node["Grams"].ToDouble(),
            });
        }

        return result;
    }

    public ICollection<Menu> GetMenus(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filename);

        var result = new List<Menu>();

        if (xmlDoc.DocumentElement == null)
            throw new ArgumentNullException("The root element cannot be null.");

        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            result.Add(new Menu()
            {
                Id = node["Id"] is null ? 0 : node["Id"].ToInt(),
                Date = node["Date"] is null ? 
                       DateTime.MinValue : node["Date"].ToDateTime(),
            });
        }

        return result;
    }

    public ICollection<MenuItem> GetMenuItems(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filename);

        var result = new List<MenuItem>();

        if (xmlDoc.DocumentElement == null)
            throw new ArgumentNullException("The root element cannot be null.");

        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            result.Add(new MenuItem()
            {
                DishId = node["DishId"] is null ? 0 : node["DishId"].ToInt(),
                MenuId = node["MenuId"] is null ? 0 : node["MenuId"].ToInt(),
                Price = node["Price"] is null ? 0 : node["Price"].ToDecimal(),
            });
        }

        return result;
    }

    public ICollection<Product> GetProducts(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filename);

        var result = new List<Product>();

        if (xmlDoc.DocumentElement == null)
            throw new ArgumentNullException("The root element cannot be null.");

        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            result.Add(new Product()
            {
                Id = node["Id"] is null ? 0 : node["Id"].ToInt(),
                Name = node["Name"] is null ? string.Empty : node["Name"].InnerText,
                CaloriesPer100Grams = node["CaloriesPer100Grams"] is null ? 
                                      0 : node["CaloriesPer100Grams"].ToDouble(),
                SupplierId = node["SupplierId"] is null ? 0 : node["SupplierId"].ToInt(),
            });
        }

        return result;
    }

    public ICollection<Supplier> GetSuppliers(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(filename);

        var result = new List<Supplier>();

        if (xmlDoc.DocumentElement == null)
            throw new ArgumentNullException("The root element cannot be null.");

        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            result.Add(new Supplier()
            {
                Id = node["Id"] is null ? 0 : node["Id"].ToInt(),
                Name = node["Name"] is null ? string.Empty : node["Name"].InnerText
            });
        }

        return result;
    }
}
