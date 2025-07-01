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
        // Si no tiene elementos hijos ni atributos, devolver solo el valor
        if (!element.HasElements && element.Attributes().Count() == 0)
            return element.Value;

        var dict = new Dictionary<string, object>();

        // Agregar atributos si existen
        foreach (var attr in element.Attributes())
        {
            if (attr.Name.LocalName.Contains("Options"))
                dict[$"{attr.Name.LocalName}"] = attr.Value.Split(',');
            else dict[$"{attr.Name.LocalName}"] = attr.Value;
        }

        // Si tiene elementos hijos, procesarlos
        if (element.HasElements)
        {
            // Agrupar hijos por nombre para manejar listas
            var grouped = element.Elements().GroupBy(e => e.Name.LocalName);
            foreach (var group in grouped)
            {
                var elements = group.Select(ConvertElement).ToList();
                dict[group.Key] = elements.Count == 1 ? elements.First() : elements;
            }
        }
        else
        {
            // Si no tiene elementos hijos pero sí tiene valor de texto, agregarlo
            if (!string.IsNullOrEmpty(element.Value))
            {
                dict["value"] = element.Value;
            }
        }

        return dict;
    }
}