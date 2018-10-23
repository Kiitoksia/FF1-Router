using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FF1Router.Models
{
    public class SettingsModel : BaseModel
    {
        private bool _advancedMode;
        private bool _coordinatesMode;
        private bool _isLoaded = false;

        public SettingsModel()
        {
            PropertyChanged += (s, e) => Save();
            _isLoaded = true;
        }

        public SettingsModel(XElement xml, out bool isValid) : this()
        {
            _isLoaded = false;
            isValid = true;

           if (!bool.TryParse(xml.Element(nameof(AdvancedMode))?.Value, out bool advancedMode))
           {
               isValid = false;
               return;
           }

            AdvancedMode = advancedMode;

            if (!bool.TryParse(xml.Element(nameof(CoordinatesMode))?.Value, out bool coordinatesMode))
            {
                isValid = false;
                return;
            }

            CoordinatesMode = coordinatesMode;
            _isLoaded = true;
        }

        private void Save()
        {
            if (!_isLoaded) return;
            XElement xml = new XElement("Settings");
            xml.Add(new XElement(nameof(AdvancedMode), AdvancedMode));
            xml.Add(new XElement(nameof(CoordinatesMode), CoordinatesMode));

            xml.Save(Const.SettingsPath);
        }

        public bool AdvancedMode
        {
            get
            {
                return _advancedMode;
            }
            set
            {
                if (value == _advancedMode) return;
                _advancedMode = value;
                OnPropertyChanged();
            }
        }

        public bool CoordinatesMode
        {
            get
            {
                return _coordinatesMode;
            }
            set
            {
                if (value == _coordinatesMode) return;
                _coordinatesMode = value;
                OnPropertyChanged();
            }
        }
    }
}
