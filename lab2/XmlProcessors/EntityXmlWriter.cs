using System.Xml.Linq;

namespace lab2.XmlProcessors;

public class EntityXmlWriter
{
    public void AddElement<T>(T item, string filepath) where T : class
    {
        if (item == null)
            throw new ArgumentNullException("The item value cannot be null.");

        var xElement = CreateXElement(item);

        var xDocument = LoadXDocument(filepath);

        if (xDocument != null)
        {
            if(xDocument.Root == null)
                throw new ArgumentNullException("There is not root element.");

            var type = item.GetType();
            var root = type.Name.Equals("Dish") ? $"{type.Name}es" : $"{type.Name}s";

            if (!xDocument.Root.Name.LocalName.Equals(root))
                throw new InvalidCastException("There is incorrect root element.");

            xDocument.Root.Add(xElement);
        }
        else
        {
            xDocument = new XDocument(
                new XElement($"{item.GetType().Name}s", xElement)
            );
        }

        xDocument.Save(filepath);
    }

    private XElement? CreateXElement<T>(T item)
    {
        if(item == null) return null;

        var type = item.GetType();
        var properties = type.GetProperties();
        var list = new List<XElement>();

        foreach (var property in properties)
            list.Add(new XElement(property.Name, property.GetValue(item)));

        return new XElement(type.Name, list.ToArray());
    }

    private XDocument? LoadXDocument(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"There is no existing file '{filename}'");

        var xDocument = XDocument.Load(filename);

        if (xDocument.Root == null) return null;

        return xDocument;
    }

}
