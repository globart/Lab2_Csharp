using lab2.Entities;
using System.Xml.Linq;

namespace lab2.Parsers;

public static class EntityParser
{
    public static Dish? ToDish(this XElement xElement)
    {

        var model = new Dish()
        {
            Id = xElement.Element("Id").ToInt(),
            Name = xElement.Element("Name")?.Value,
        };

        return model;
    }

    public static Ingredient? ToIngredient(this XElement xElement)
    {

        var model = new Ingredient()
        {
            DishId = xElement.Element("DishId").ToInt(),
            ProductId = xElement.Element("ProductId").ToInt(),
            Grams = xElement.Element("Grams").ToDouble(),
        };

        return model;
    }

    public static Menu? ToMenu(this XElement xElement)
    {

        var model = new Menu()
        {
            Id = xElement.Element("Id").ToInt(),
            Date = xElement.Element("Date").ToDateTime(),
        };

        return model;
    }

    public static MenuItem? ToMenuItem(this XElement xElement)
    {
        if (xElement == null) return null;

        var model = new MenuItem()
        {
            DishId = xElement.Element("DishId").ToInt(),
            MenuId = xElement.Element("MenuId").ToInt(),
            Price = xElement.Element("Price").ToDecimal(),
        };

        return model;
    }

    public static Product? ToProduct(this XElement xElement)
    {
        if (xElement == null) return null;

        var model = new Product()
        {
            Id = xElement.Element("Id").ToInt(),
            Name = xElement.Element("Name")?.Value,
            CaloriesPer100Grams = xElement.Element("CaloriesPer100Grams").ToDouble(),
            SupplierId = xElement.Element("SupplierId").ToInt(),
        };

        return model;
    }

    public static Supplier ToSupplier(this XElement xElement)
    {
        if (xElement == null) return null;

        var model = new Supplier()
        {
            Id = xElement.Element("Id").ToInt(),
            Name = xElement.Element("Name")?.Value,
        };

        return model;
    }
}
