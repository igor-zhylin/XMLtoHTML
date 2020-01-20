namespace XMLtoHTML
{
    using System;
    using System.IO;

    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                string template = @"Template.xslt";

                string InputXMLFile = "report.xml";
                
                var converter = new XSLConverter(InputXMLFile, template);
                converter.RunTransformation();
                converter.SaveToFile(@".\", "report.html");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
                return -1;
            }
            return 0;
        }

    }
}
