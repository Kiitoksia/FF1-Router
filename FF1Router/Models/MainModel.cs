using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using KiiLibrary.WPF.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace FF1Router.Models
{
    public class MainModel : BaseModel
    {
        private RouteModel _route;
        private SettingsModel _settings;

        public MainModel()
        {
            bool isValid = true;
            
            try
            {
                if (File.Exists(Const.SettingsPath))
                {
                    XElement xml = XElement.Load(Const.SettingsPath);
                    Settings = new SettingsModel(xml, out isValid);
                }
                else
                {
                    Settings = new SettingsModel();
                }
            }
            catch(Exception ex)
            {
                isValid = false;
            }

            if (!isValid)
            {                
                MessageBox.Show("There was an error loading settings.  Settings have been reverted to default", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            App.Settings = Settings;
            LoadRouteCommand = new Command(() => true, LoadRoute);
            SaveRouteCommand = new Command(() => !string.IsNullOrWhiteSpace(Route?.Name), SaveRoute);
            ShowAboutCommand = new Command(() => true, ShowAbout);

            Route = new RouteModel();            
        }

        private void ShowAbout(object sender, EventArgs eventArgs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FF1 Step Count Calculator is a program designed to speed up the process of creating a step route, by calculating what specific steps will output");
            sb.AppendLine();
            sb.AppendLine("Credits:");
            sb.AppendLine("Kiitoksia: Author of the program");
            sb.AppendLine();
            sb.AppendLine("Avin_Chaos: For his detailed explanation of the programs requirements and logic and feedback");

            Window.ShowMessageAsync("About FF1 Step Count Calculator", sb.ToString());
        }

        private void SaveRoute(object sender, EventArgs eventArgs)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            ConfigureDialog(dialog);

            if (!Directory.Exists(Const.RoutesPath)) Directory.CreateDirectory(Const.RoutesPath);
            dialog.InitialDirectory = Const.RoutesPath;
           

            if (dialog.ShowDialog(Window) ?? false)
            {
                string filePath = dialog.FileName;
                XElement xml = new XElement("Route");
                Route.CopyTo(ref xml);
                xml.Save(filePath);
            }
        }

        private void ConfigureDialog(FileDialog dialog)
        {
            dialog.AddExtension = true;
            dialog.DefaultExt = "xml";
            dialog.Filter = "Route Files (.xml)|*.xml";
            dialog.DefaultExt = ".xml";
        }

        private void LoadRoute(object sender, EventArgs eventArgs)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            ConfigureDialog(dialog);

            if (dialog.ShowDialog(Window) ?? false)
            {
                string filePath = dialog.FileName;
                RouteModel route = new RouteModel(XElement.Load(filePath), out bool isValid);

                if (!isValid)
                {
                    Window.ShowMessageAsync("Error loading route", "There was an error loading the route.  This route may be an older version of the program that is no longer supported");
                    return;
                }

                Route = route;
            }
        }

        public MainWindow Window { get; set; }

        public RouteModel Route
        {
            get
            {
                return _route;
            }
            set
            {
                if (Equals(value, _route)) return;
                _route = value;
                OnPropertyChanged();
            }
        }

        public SettingsModel Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                if (Equals(value, _settings)) return;
                _settings = value;
                OnPropertyChanged();
            }
        }

        public Command LoadRouteCommand { get; }
        public Command SaveRouteCommand { get; }
        public Command ShowAboutCommand { get; }

        public string Version => $"Version {Assembly.GetEntryAssembly().GetName().Version}";
    }
}
