using System;
using System.Xml;
using System.Xml.XPath;
using Wmhelp.XPath2;

namespace CustomXMLRulesValidator
{
    class Program
    {
        static string validFunctionPrefixes = "process_,generate_,fn_,rest_";
        static void Main(string[] args)
        {
            var file = args[0];
            Console.WriteLine($"Processing file '{args[0]}'");

            var doc = new XPathDocument(file);
            var xpathNav = doc.CreateNavigator();

            AllPublishedFunctionsHaveVersionHistoryComments(xpathNav);
            Console.WriteLine($"Done");

        }

        static void AllPublishedFunctionsHaveVersionHistoryComments(XPathNavigator xDoc)
        {
            var allPublishedfunctions = @"//publishedFunction";
            var allFunctionNodes = xDoc.Select(allPublishedfunctions);
            Console.WriteLine($"There are '{allFunctionNodes?.Count}' published functions");
            foreach (XPathNavigator functionNode in allFunctionNodes)
            {
                if(!functionNode.GetAttribute("script","").Contains("TFS 137422"))
                {
                    var lineNumber =(IXmlLineInfo)functionNode != null ? ((IXmlLineInfo)functionNode).LineNumber : 0;
                    Console.WriteLine($"The published function '{functionNode.GetAttribute("name","")}' at line {lineNumber} is missing a version history comment.");

                }
            }
        }
    }
}
