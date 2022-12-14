using FHU_Synced.ViewModels;
using FHU_Synced.Views.UserControls;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Presets.xaml
    /// </summary>
    public partial class Presets : Page
    {
        public Presets()
        {
            InitializeComponent();
        }

        private void PresetControl_SyncDone(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is PresetsVM vm && e.Source is PresetControl routedInst)
                vm.PresetDownloaded(routedInst.PresetData.Name);
        }

        private void PresetControl_PresetDeleted(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is PresetsVM vm && e.Source is PresetControl routedInst)
                vm.DeletedPreset(routedInst.PresetData.Name);
        }
    }
}
