using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FF1Router.Exceptions;
using KiiLibrary.WPF.Entities;
using MahApps.Metro.Controls.Dialogs;

namespace FF1Router.Models
{
    public class RouteModel : BaseModel
    {
        private BindingList<StepModel> _steps;
        private BindingList<EncounterModel> _encounters;
        private StepModel _focusedSteps;
        private StepCountPointerDirection _currentDirection;
        private string _currentIndex;
        private string _currentRng;
        private int _totalSteps;
        private int _stepsTillNextEnc;
        private string _name;
        private string _comments;
        private BindingList<ZoneModel> _possibleZones;
        private int _nextBattleGroup;


        public RouteModel(XElement xml, out bool isValid) : this()
        {
            isValid = true;
            Name = xml.Element(nameof(Name))?.Value;
            if (string.IsNullOrWhiteSpace(Name))
            {
                isValid = false;
                return;
            }

            Comments = xml.Element(nameof(Comments))?.Value;
            Steps.Clear();
            foreach (XElement stepXml in xml.Elements("Step"))
            {
                StepModel stepModel = new StepModel(stepXml, out bool valid);
                if (!valid)
                {
                    isValid = false;
                    return;
                }

                Steps.Add(stepModel);
            }
        }

        public RouteModel()
        {
            Steps = new BindingList<StepModel>();
            Steps.ListChanged += (s, e) =>
            {
                CalculateStepCount();
            };

            Encounters = new BindingList<EncounterModel>();
            Encounters.ListChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(Encounters));
            };

            AddStepsCommand = new Command(() => true, (s, e) =>
            {
                Steps.AddNew();
                FocusedSteps = Steps.Last();
            });
            RemoveStepsCommand = new Command(() => FocusedSteps != null && Steps.Count > 1, (s, e) =>
            {
                Steps.Remove(FocusedSteps);
                FocusedSteps = null;
            });

            ShowAboutCommand = new Command(() => true, ShowAbout);

            Steps.AddNew();
            FocusedSteps = Steps[0];
            PossibleZones =  new BindingList<ZoneModel>(Const.Zones.ToList());
            CalculateStepCount();
        }

        private void ShowAbout(object sender, EventArgs eventArgs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FF8 Router is a program designed to speed up the process of creating a step route, by calculating what specific steps will output");
            sb.AppendLine();
            sb.AppendLine("Credits:");
            sb.AppendLine("Kiitoksia: Author of the program");
            sb.AppendLine();
            sb.AppendLine("Avin_Chaos: For his detailed explanation of the programs requirements and logic");

            Window.ShowMessageAsync("About FF8 Router", sb.ToString());
        }

        public MainWindow Window { get; set; }

        public BindingList<ZoneModel> PossibleZones
        {
            get
            {
                return _possibleZones;
            }
            set
            {
                if (Equals(value, _possibleZones)) return;
                _possibleZones = value;
                OnPropertyChanged();
            }
        }

        public BindingList<StepModel> Steps
        {
            get
            {
                return _steps;
            }
            set
            {
                if (Equals(value, _steps)) return;
                _steps = value;
                OnPropertyChanged();
            }
        }

        public StepModel FocusedSteps
        {
            get
            {
                return _focusedSteps;
            }
            set
            {
                if (Equals(value, _focusedSteps)) return;
                _focusedSteps = value;
                OnPropertyChanged();
            }
        }

        public BindingList<EncounterModel> Encounters
        {
            get
            {
                return _encounters;
            }
            set
            {
                if (Equals(value, _encounters)) return;
                _encounters = value;
                OnPropertyChanged();
            }
        }

        public Command AddStepsCommand { get; }
        public Command RemoveStepsCommand { get; }
        public Command ShowAboutCommand { get; }

        public StepCountPointerDirection CurrentDirection
        {
            get
            {
                return _currentDirection;
            }
            set
            {
                if (value == _currentDirection) return;
                _currentDirection = value;
                OnPropertyChanged();
            }
        }

        public string CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                if (value == _currentIndex) return;
                _currentIndex = value;
                OnPropertyChanged();
            }
        }

        public string CurrentRng
        {
            get
            {
                return _currentRng;
            }
            set
            {
                if (value == _currentRng) return;
                _currentRng = value;
                OnPropertyChanged();
            }
        }

        public int TotalSteps
        {
            get
            {
                return _totalSteps;
            }
            set
            {
                if (value == _totalSteps) return;
                _totalSteps = value;
                OnPropertyChanged();
            }
        }

        public int StepsTillNextEnc
        {
            get
            {
                return _stepsTillNextEnc;
            }
            set
            {
                if (value == _stepsTillNextEnc) return;
                _stepsTillNextEnc = value;
                OnPropertyChanged();
            }
        }

        private void CalculateStepCount()
        {
            Encounters.Clear();

            int traverseIndex = 0; // An index to track where we are in the traversal.  i.e., down or up.  
            int encounterTableIndex = Const.EncounterStartPoint; // An index to track where we are in the encounter hex table.
            int battleGroupIndex = 0; // An index to track where we are in the battle group
            int stepsSinceLastEnc = 0;

            // Loop through the users supplied zones + steps
            foreach (StepModel step in Steps.Where(s => s.IsValid))
            {                
                if (step.PowerCycle)
                {
                    // User specified this was a power cycle, reset our indexes and continue our loop
                    traverseIndex = 0;
                    encounterTableIndex = Const.EncounterStartPoint;
                    battleGroupIndex = 0;
                    stepsSinceLastEnc = 0;
                    if (!string.IsNullOrWhiteSpace(step.Comments)) Encounters.Add(new EncounterModel(ZoneType.NotApplicable, "(Power Cycle)", null, null, null, step.Comments));
                    continue;
                }

                int encountersFound = Encounters.Count;
                // Go through every step in our zone
                for (int i = 1; i <= step.Steps; i++)
                {
                    encounterTableIndex = Const.StepCountDirectionPattern[traverseIndex].GetNextIndex(encounterTableIndex);
                    stepsSinceLastEnc++;

                    if (encounterTableIndex == 0)
                    {
                        traverseIndex++;
                        if (traverseIndex > Const.StepCountDirectionPattern.Length - 1)
                        {
                            // Repeat our direction again by resetting to 0
                            traverseIndex = 0;
                        }
                    }
                    
                    // Find the encounter theshhold at this index.  Compare it to the zones threshold
                    if (Const.EncounterTable[encounterTableIndex] < step.Zone.Threshhold)
                    {
                        // Battle!  Find the group
                        Encounters.Add(new EncounterModel(step.Zone.Type, step.Zone.Name, step.Zone.ZoneEncounters[Const.RandomBattleGroup[battleGroupIndex] - 1], Const.RandomBattleGroup[battleGroupIndex], stepsSinceLastEnc, step.Comments));
                        
                        // Move up to the next battle group
                        battleGroupIndex++;
                        if (battleGroupIndex > 255) battleGroupIndex = 0;
                        stepsSinceLastEnc = 0;
                    }
                }

                if (encountersFound == Encounters.Count && !string.IsNullOrWhiteSpace(step.Comments))
                {
                    // There are no encounters within this StepModel, but the user provided a comment.  Add this into the result screen (dummy encounter)
                    Encounters.Add(new EncounterModel(step.Zone?.Type ?? ZoneType.NotApplicable, step.ZoneDisplay, null, null, null, step.Comments));
                }
            }

            CurrentDirection = Const.StepCountDirectionPattern[traverseIndex];
            CurrentIndex = encounterTableIndex.ToString("X");
            CurrentRng = Const.EncounterTable[encounterTableIndex].ToString("X");
            NextBattleGroup = Const.RandomBattleGroup[battleGroupIndex];

            // Calculate when the next encounter will occur
            if (FocusedSteps?.Zone != null)
            {
                StepCountPointerDirection direction = CurrentDirection;
                for (int i = 1; i < int.MaxValue; i++)
                {
                    encounterTableIndex = direction.GetNextIndex(encounterTableIndex);

                    if (encounterTableIndex == 0)
                    {
                        traverseIndex++;
                        if (traverseIndex > Const.StepCountDirectionPattern.Length - 1)
                        {
                            // Repeat our direction again by resetting to 0
                            traverseIndex = 0;
                        }
                    }

                    if (Const.EncounterTable[encounterTableIndex] < FocusedSteps.Zone.Threshhold)
                    {
                        // We found the battle
                        StepsTillNextEnc = i;
                        break;
                    }
                }
            }            
        }


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
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

        public int NextBattleGroup
        {
            get
            {
                return _nextBattleGroup;
            }
            set
            {
                if (value == _nextBattleGroup) return;
                _nextBattleGroup = value;
                OnPropertyChanged();
            }
        }

        public void CopyTo(ref XElement xml)
        {
            xml.Add(new XElement(nameof(Name), Name));
            xml.Add(new XElement(nameof(Comments), Comments));

            foreach (StepModel stepModel in Steps.Where(s => s.Zone != null || s.PowerCycle))
            {
                XElement stepXml = new XElement("Step");
                stepModel.CopyTo(ref stepXml);
                xml.Add(stepXml);
            }
        }
    }
}
