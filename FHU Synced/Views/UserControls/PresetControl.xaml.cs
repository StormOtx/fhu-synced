using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
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
using FHU_Synced.Models;
using FHU_Synced.ViewModels;

namespace FHU_Synced.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PresetControl.xaml
    /// </summary>
    public partial class PresetControl : UserControl
    {
        public PresetControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PresetProperty = DependencyProperty.Register(
                    "PresetData",
                    typeof(Preset),
                    typeof(PresetControl),
                    new FrameworkPropertyMetadata(OnPresetDataChangedCallBack));

        public Preset PresetData
        {
            get { return (Preset)GetValue(PresetProperty); }
            set { SetValue(PresetProperty, value); }
        }

        private static void OnPresetDataChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PresetControl inst = (PresetControl)sender;

            if (!String.IsNullOrEmpty(inst.PresetData.Thumbnail))
                inst.PresetThumbnail.ImageSource = new BitmapImage(new Uri(inst.PresetData.Thumbnail), new RequestCachePolicy(RequestCacheLevel.Revalidate));
        }

        public static readonly RoutedEvent SyncClickedEvent = EventManager.RegisterRoutedEvent(
            "SyncClicked",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PresetControl));

        public event RoutedEventHandler SyncClicked
        {
            add { AddHandler(SyncClickedEvent, value); }
            remove { RemoveHandler(SyncClickedEvent, value); }
        }

        public static readonly RoutedEvent SyncDoneEvent = EventManager.RegisterRoutedEvent(
            "SyncDone",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PresetControl));

        public event RoutedEventHandler SyncDone
        {
            add { AddHandler(SyncDoneEvent, value); }
            remove { RemoveHandler(SyncDoneEvent, value); }
        }

        private async void SynchronizeClicked(object sender, RoutedEventArgs e)
        {
            if (this.ParentContainer.DataContext is AssetDownloaderVM vm)
                await vm.DownloadAssetToFile(this.PresetData.DownloadLink, "test.fhp");
        }
    }
}
