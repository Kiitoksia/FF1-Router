using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FF1Router.Data
{
    public class AppSettings
    {
        public AppSettings()
        {

        }

        /// <summary>
        /// Attempts to load AppSettings from XML
        /// </summary>
        public AppSettings(XElement xml, out bool isValid)
        {
            
            isValid = true;
        }

        public bool AdvancedMode { get; set; } = true;


    }
}
