using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FF1Router.Exceptions;
using FF1Router.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace FF1Router
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SettingsModel Settings { get; set; }

        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                InvalidSettingsException ex = e.ExceptionObject as InvalidSettingsException;
                if (ex == null) ex = (e.ExceptionObject as Exception)?.InnerException as InvalidSettingsException;
                if (ex == null) return;

                
                MessageBox.Show("There was an error loading settings.", "Error");
            };
        }

        #endregion
    }
}
