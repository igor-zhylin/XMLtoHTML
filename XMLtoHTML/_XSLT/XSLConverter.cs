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

        /// <summary>
        /// Read input xml and template(do enable to convert to html)
        /// </summary>
        /// <param name="xmlPath">Path to xml file(without file validation)</param>
        /// <param name="templatePath">Path to XSLT template file</param>
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
        /// Runs Tranformation from XML to HTML 
        /// </summary>
        public void RunTransformation()
        {
            _HTML = TransformXMLToHTML();
        }

        /// <summary>
        /// Transform file from params from class constructor(for saving to file use save method)
        /// </summary>
        private string TransformXMLToHTML()
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
        /// Write all converted text to file 
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="filename">File name with extension</param>
        public void SaveToFile(string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(@$"{path}\{filename}", _HTML);
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