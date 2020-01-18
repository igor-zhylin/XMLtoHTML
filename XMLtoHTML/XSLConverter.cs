namespace XMLtoHTML
{
    using System.IO;
    using System.Xml;
    using System.Xml.Xsl;

    public class XSLConverter
    {
        private string _XMLPath;
        private string _XML;
        private string _Template;

        public XSLConverter(string xmlPath, string template)
        {
            this._XMLPath = xmlPath;
            this._Template = template;

            _XML = !string.IsNullOrEmpty(_XMLPath) ? ReadAllXML() : throw new System.Exception("EMPTY XML FILEPATH!");
        }

        /// <summary>
        /// Read Input file from path 
        /// </summary>
        /// <returns>Returns HTML page</returns>
        private string ReadAllXML()
            => File.ReadAllText(_XMLPath);

        /// <summary>
        /// Transform file from params from class constructor
        /// </summary>
        /// <returns></returns>
        public string TransformXMLToHTML()
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(_Template)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(_XML)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }


        /// <summary>
        /// Static Transormer for xml 
        /// </summary>
        /// <param name="inputXml">Input string XML</param>
        /// <param name="xsltString">Template</param>
        /// <returns>Returns HTML page</returns>
        public static string TransformXMLToHTML(string inputXml, string xsltString)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }
    }
}
