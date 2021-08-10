using System;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using Wmhelp.XPath2;

namespace CustomXMLRulesValidator
{
    class Program
    {
        static string validFunctionPrefixes = "process_,generate_,fn_,rest_,da_";
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
            var functionPrefixes = validFunctionPrefixes.Split(',').ToList();


            Console.WriteLine($"There are '{allFunctionNodes?.Count}' published functions");
            foreach (XPathNavigator functionNode in allFunctionNodes)
            {
                var functionName = functionNode.GetName();
                var scriptText = functionNode.GetAttribute("script", "");
                if (!scriptText.Contains("TFS 137422"))
                {
                    Console.WriteLine($"The published function '{functionName}' at line {functionNode.GetLineNumberOrDefault()} is missing a version history comment.");
                }

                if (!functionPrefixes.Any(p => p.StartsWith(functionName)))
                {
                    Console.WriteLine($"The published function '{functionName}' at line {functionNode.GetLineNumberOrDefault()} does not match any approved prefixes.");
                }

            }
        }
    }
}
