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
                string template = File.ReadAllText(@"Template.xslt");

                string InputXMLFile = " ";
                string HTMLpath = " ";
                string OutputHTML = XSLConverter.TransformXMLToHTML(InputXMLFile, template);

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
