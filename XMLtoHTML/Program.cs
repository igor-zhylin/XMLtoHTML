﻿namespace XMLtoHTML
{
    using System;
    using XMLtoHTML._XML;

    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                string template = @"Template.xslt";
#if DEBUG
                string InputXMLFile = "report.xml";
                string SaveTo = "report.html";
#else
                string InputXMLFile = args[0];
                string SaveTo = args[1];
#endif

                XMLWorker worker = new XMLWorker(InputXMLFile);
                if (!worker.ValidateFile())
                {
                    Console.WriteLine($"Input xml file has an invalid format!!! \n {InputXMLFile}");
                }
                else
                {
                    worker.LoadTestResultsFromHeader();
                    var converter = new XSLConverter(InputXMLFile, template);
                    converter.RunTransformation();
                    converter.SaveToFile(SaveTo);
                }

                return worker.GetFailedTestsCount();
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR REPORT GENERATION! (XMLtoHTML.exe)");
                Console.WriteLine(ex.StackTrace.ToString());
                return -1;
            }
        }

    }
}
