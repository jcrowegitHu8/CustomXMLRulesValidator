using System;
using System.Xml;
using Wmhelp.XPath2;

namespace CustomXMLRulesValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];
            Console.WriteLine($"Processing file '{args[0]}'");

            var doc = new XmlDocument();
            doc.Load(file);

            AllPublishedFunctionsHaveVersionHistoryComments(doc);
            Console.WriteLine($"Done");

        }

        static void AllPublishedFunctionsHaveVersionHistoryComments(XmlDocument xDoc)
        {
            var allPublishedfunctions = @"//publishedFunction";
            var allFunctionNodes = xDoc.SelectNodes(allPublishedfunctions);
            Console.WriteLine($"There are '{allFunctionNodes?.Count}' published functions");
            foreach (XmlNode functionNode in allFunctionNodes)
            {
                if(functionNode.Attributes["script"].Value.Contains("TFS 137422"))
                {
                    Console.WriteLine($"There are '{functionNode.Attributes["name"].Value}' has a code comment.");
                }
            }
        }
    }
}
