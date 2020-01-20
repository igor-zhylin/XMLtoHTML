namespace XMLtoHTML._XML
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Gets report summary 
    /// </summary>
    public class XMLWorker
    {
        private string _Path;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="path">Path to XML file</param>
        public XMLWorker(string path)
        {
            this._Path = path;
        }

        /// <summary>
        /// Try to validate XML
        /// </summary>
        /// <returns>true if validated else if not</returns>
        public bool ValidateFile()
            => Validate();

        private bool Validate()
        {
            try
            {
                var settings = new XmlReaderSettings {
                DtdProcessing = DtdProcessing.Ignore,
                XmlResolver = null
            };

                using var reader = XmlReader.Create(new StreamReader(_Path), settings);

                var document = new XmlDocument();
                document.Load(reader);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}