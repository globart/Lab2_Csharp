using System.Xml.Linq;
using lab2.FilePaths;

namespace lab2.Contexts;

public class Context
{
    public XDocument DishesXml { get; private set; }
    public XDocument IngredientsXml { get; private set; }
    public XDocument MenusXml { get; private set; }
    public XDocument MenuItemsXml { get; private set; }
    public XDocument ProductsXml { get; private set; }
    public XDocument SuppliersXml { get; private set; }

    private readonly Paths _paths;

    public Context()
    {
        _paths = new Paths();
        Upload();
    }

    private void Upload()
    {
        DishesXml = XDocument.Load(_paths.DishesXml);
        IngredientsXml = XDocument.Load(_paths.IngredientsXml);
        MenusXml = XDocument.Load(_paths.MenusXml);
        MenuItemsXml = XDocument.Load(_paths.MenuItemsXml);
        ProductsXml = XDocument.Load(_paths.ProductsXml);
        SuppliersXml = XDocument.Load(_paths.SuppliersXml);
    }
}
