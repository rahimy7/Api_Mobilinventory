using System.Xml.Linq;

public static class XmlToJsonConverter
{
    public static object ConvertXmlToDynamicJson(string xml)
    {
        try
        {
            var doc = XDocument.Parse(xml);
            return ConvertElement(doc.Root!);
        }
        catch (System.Exception)
        {
            return xml;
        }
    }

    private static object ConvertElement(XElement element)
    {
        if (!element.HasElements && element.Attributes().Count() == 0)
            return element.Value;

        var dict = new Dictionary<string, object>();

        // Agregar atributos si existen
        foreach (var attr in element.Attributes())
        {
            dict[$"@{attr.Name.LocalName}"] = attr.Value;
        }

        // Agrupar hijos por nombre para manejar listas
        var grouped = element.Elements().GroupBy(e => e.Name.LocalName);
        foreach (var group in grouped)
        {
            var elements = group.Select(ConvertElement).ToList();
            dict[group.Key] = elements.Count == 1 ? elements.First() : elements;
        }

        return dict;
    }
}
