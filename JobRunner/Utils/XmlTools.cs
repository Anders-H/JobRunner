using System.Xml;

namespace JobRunner.Utils
{
    public static class XmlTools
    {
        public static bool GetBoolFromAttribute(this XmlNode e, string attributeName)
        {
            if (e == null)
                return false;
            var att = e.Attributes?.GetNamedItem(attributeName);
            var v = att?.Value?.ToLower() ?? "";
            return v == "1" || v == "true";
        }

        public static string GetChildString(this XmlElement e, string elementName) =>
            e.SelectSingleNode(elementName)?.InnerText ?? "";
    }
}