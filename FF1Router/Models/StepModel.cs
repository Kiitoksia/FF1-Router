using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FF1Router.Annotations;

namespace FF1Router.Models
{
    public class StepModel : BaseModel
    {
        private int _steps;
        private ZoneModel _zone;
        private bool _powerCycle;
        private string _comments;

        public StepModel()
        {

        }
        
        public StepModel(XElement xml, out bool isValid) : this()
        {            
            if (bool.TryParse(xml.Attribute(nameof(PowerCycle))?.Value, out bool powerCycle)) PowerCycle = powerCycle;
            else
            {
                isValid = false;
                return;
            }

            // New way of grabbing indexes (1.0.0.3)
            if (int.TryParse(xml.Attribute(nameof(Zone))?.Value, out int index))
            {
                Zone = Const.Zones[index];
            }
            else
            {
                // Old method for backwards compatibility (switcheed to first or default to avoid errors)
                string zoneName = xml.Attribute(nameof(Zone))?.Value;
                Zone = Const.Zones.FirstOrDefault(s => s.Name == zoneName);
            }


            if (Zone == null && !PowerCycle)
            {
                isValid = false;
                return;
            }

            if (int.TryParse(xml.Attribute(nameof(Steps))?.Value, out int steps)) Steps = steps;
            else
            {
                isValid = false;
                return;
            }

            Comments = xml.Attribute(nameof(Comments))?.Value;

            isValid = true;
        }

        public void CopyTo(ref XElement xml)
        {
            xml.Add(new XAttribute(nameof(PowerCycle), PowerCycle));
            xml.Add(new XAttribute(nameof(Zone), Zone?.Index.ToString() ?? ""));
            xml.Add(new XAttribute(nameof(Steps), Steps));
            xml.Add(new XAttribute(nameof(Comments), Comments ?? ""));
        }


        public int Steps
        {
            get
            {
                return _steps;
            }
            set
            {
                if (value == _steps) return;
                _steps = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StepsDisplay));
            }
        }

        public ZoneModel Zone
        {
            get
            {
                return _zone;
            }
            set
            {
                if (Equals(value, _zone)) return;
                _zone = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ZoneDisplay));
                if (value != null) PowerCycle = false;
            }
        }

        public bool PowerCycle
        {
            get
            {
                return _powerCycle;
            }
            set
            {
                if (value == _powerCycle) return;
                _powerCycle = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ZoneDisplay));
                OnPropertyChanged(nameof(StepsDisplay));
                if (value) Zone = null;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                if (value == _comments) return;
                _comments = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid => Zone != null || PowerCycle;

        public string ZoneDisplay => PowerCycle ? "(Power Cycle)" : (App.Settings.CoordinatesMode ? Zone?.Coordinates ?? Zone?.Name : Zone?.Name);

        public string StepsDisplay => PowerCycle ? "N/A" : Steps.ToString();
    }
}
