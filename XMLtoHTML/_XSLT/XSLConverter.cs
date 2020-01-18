namespace XMLtoHTML
{
    using System.IO;
    using System.Xml;
    using System.Xml.Xsl;

    public class XSLConverter
    {
        private string _XMLPath;
        private string _XML;

        private string _TemplatePath;
        private string _Template;

        private string _HTML;

        public XSLConverter(string xmlPath, string templatePath)
        {
            this._XMLPath = xmlPath;
            this._TemplatePath = templatePath;

            _XML = (!string.IsNullOrEmpty(_XMLPath))
                    ? ReadAll(_XMLPath)
                    : throw new System.Exception("EMPTY XML FILEPATH!");

            _Template = (!string.IsNullOrEmpty(_TemplatePath))
                    ? ReadAll(_TemplatePath)
                    : throw new System.Exception("EMPTY TEMPLATE FILEPATH!");
        }

        /// <summary>
        /// Read Input file from path 
        /// </summary>
        /// <returns>Returns HTML page</returns>
        private string ReadAll(string path)
            => File.ReadAllText(path);

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
            _HTML = results.ToString();
            return _HTML;
        }

        public void SaveToFile(string path)
        {
            if (!path.EndsWith(@"\"))
                path += @"\";
            path += "report.html";
            File.WriteAllText(path, _HTML);
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