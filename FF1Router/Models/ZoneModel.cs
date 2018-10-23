using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace FF1Router.Models
{
    public class ZoneModel : BaseModel
    {
        private string _name;
        private BindingList<string> _encounters;
        private int _threshhold;
        private string _coordinates;
        private BindingList<ZoneEncounterModel> _zoneEncounters;
        private ZoneType _type;
        private int _index;

        public ZoneModel(int index, ZoneType type, string name, int threshhold, string[] encounters, string coordinates = null, params int[] unfleeableBattleGroups)
        {
            Index = index;
            Type = type;
            Name = name;
            Encounters = new BindingList<string>(encounters);
            Threshhold = threshhold;
            Coordinates = coordinates;

            if (App.Settings != null)
            {
                App.Settings.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(SettingsModel.CoordinatesMode)) OnPropertyChanged(nameof(Display));
                };
            }

            ZoneEncounters = new BindingList<ZoneEncounterModel>();
            foreach (string encounter in encounters)
            {
                List<FormationModel> formationList = new List<FormationModel>();
                foreach (string formation in encounter.Split(','))
                {
                    Match match = Regex.Match(formation.Trim(), "(.+) (\\d*)-?(\\d*)?");
                    Debug.Assert(match.Success);

                    if (match.Success)
                    {
                        int minAmount = int.Parse(match.Groups[2].Value);
                        int maxAmount = string.IsNullOrWhiteSpace(match.Groups[3].Value) ? minAmount : int.Parse(match.Groups[3].Value);

                        formationList.Add(new FormationModel(Const.Monsters.Single(m => string.Equals(m.Name, match.Groups[1].Value.Trim(), StringComparison.CurrentCultureIgnoreCase)), minAmount, maxAmount));
                    }
                }

                ZoneEncounters.Add(new ZoneEncounterModel(formationList.ToArray(), ZoneEncounters.Count + 1));
            }

            if (unfleeableBattleGroups != null)
            {
                foreach (int battleGroup in unfleeableBattleGroups) ZoneEncounters[battleGroup].CanFlee = false;
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

        public string Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                if (value == _coordinates) return;
                _coordinates = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> Encounters
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
                OnPropertyChanged(nameof(EncountersDisplay));
            }
        }

        public BindingList<ZoneEncounterModel> ZoneEncounters
        {
            get
            {
                return _zoneEncounters;
            }
            set
            {
                if (Equals(value, _zoneEncounters)) return;
                _zoneEncounters = value;
                OnPropertyChanged();
            }
        }

        public string EncountersDisplay => string.Join("\r\n", Encounters.Select(s => $"{Encounters.IndexOf(s) + 1}) {s}"));

        public string Display
        {
            get
            {
                return App.Settings.CoordinatesMode ? Coordinates ?? Name : Name;
            }
        }

        public int Threshhold
        {
            get
            {
                return _threshhold;
            }
            set
            {
                if (value == _threshhold) return;
                _threshhold = value;
                OnPropertyChanged();
            }
        }

        public ZoneType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public int Index
        {
            get => _index;
            set
            {
                if (value == _index) return;
                _index = value;
                OnPropertyChanged();
            }
        }
    }
}
