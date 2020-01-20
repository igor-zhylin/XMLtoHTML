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
                string template = " ";

                string InputXMLFile = " ";
                string HTMLpath = @".\";

                var converter = new XSLConverter(InputXMLFile, template);
                converter.RunTransformation();
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
