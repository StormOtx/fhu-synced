using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FHU_Synced.Views.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void LaunchGame(object sender, RoutedEventArgs _)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = FHU_Synced.Settings.Launcher.Default.GamePath,
                    FileName = System.IO.Path.Combine(FHU_Synced.Settings.Launcher.Default.GamePath, FHU_Synced.Settings.Launcher.Default.GameExecutableName)
                };

                Process.Start(startInfo);
                Application.Current.Shutdown();
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't start game or launcher");
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
