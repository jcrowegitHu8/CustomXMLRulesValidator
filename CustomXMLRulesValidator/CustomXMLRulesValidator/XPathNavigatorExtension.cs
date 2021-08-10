using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace CustomXMLRulesValidator
{
    public static class XPathNavigatorExtension
    {
        public static int GetLineNumberOrDefault(this XPathNavigator element)
        {
            return (IXmlLineInfo)element != null ? ((IXmlLineInfo)element).LineNumber : 0;
        }

        public static string GetName(this XPathNavigator element)
        {
            return element.GetAttribute("name", string.Empty);
        }
    }
}
