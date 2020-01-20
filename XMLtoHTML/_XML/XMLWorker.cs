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

        private int _TotalCaseCount;
        private int _Passes;
        private int _Failures;
        private int _Skips;

        private bool IsFileValid;
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
                var settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Ignore,
                    XmlResolver = null
                };

                using var reader = XmlReader.Create(new StreamReader(_Path), settings);

                var document = new XmlDocument();
                document.Load(reader);

                IsFileValid = true;
            }
            catch
            {
                IsFileValid = false;
                return IsFileValid;
            }
            return IsFileValid;
        }

        /// <summary>
        /// Read header results from XML if its valid 
        /// </summary>
        public void LoadTestResultsFromHeader()
        {
            if (IsFileValid)
                LoadHeaderResults();
        }

        private void LoadHeaderResults()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this._Path);

            XmlNode node = doc.DocumentElement.SelectSingleNode("test-run");
            _TotalCaseCount = Convert.ToInt32(node.Attributes["testcasecount"]?.Value);
            _Passes = Convert.ToInt32(node.Attributes["passed"]?.Value);
            _Failures = Convert.ToInt32(node.Attributes["failed"]?.Value);
            _Skips = Convert.ToInt32(node.Attributes["skipped"]?.Value);

        }

    }
}