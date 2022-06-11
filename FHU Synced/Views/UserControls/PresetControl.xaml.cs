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

        public static readonly DependencyProperty RemoteProperty = DependencyProperty.Register(
                    "Remote",
                    typeof(bool),
                    typeof(PresetControl),
                    new UIPropertyMetadata(true));

        public Preset PresetData
        {
            get { return (Preset)GetValue(PresetProperty); }
            set { SetValue(PresetProperty, value); }
        }

        public bool Remote
        {
            get { return (bool)GetValue(RemoteProperty); }
            set { SetValue(RemoteProperty, value); }
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

        public static readonly RoutedEvent PresetDeletedEvent = EventManager.RegisterRoutedEvent(
            "PresetDeleted",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PresetControl));

        public event RoutedEventHandler PresetDeleted
        {
            add { AddHandler(PresetDeletedEvent, value); }
            remove { RemoveHandler(PresetDeletedEvent, value); }
        }

        private async void SynchronizeClicked(object sender, RoutedEventArgs _)
        {
            if (this.ParentContainer.DataContext is AssetDownloaderVM vm)
            {
                try
                {
                    string destination = Settings.Preset.Default.PresetsFolderLocation + "/" + this.PresetData.Name;
                    await vm.DownloadAssetToFile(this.PresetData.DownloadLink, destination);

                    RaiseEvent(new RoutedEventArgs(SyncDoneEvent, this));

                    (Application.Current.MainWindow as MainWindow)?.RootSuccessSnackbar.Show("Downloaded Preset", $"Preset {this.PresetData.Name} has been added to your game!");
                } catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Couldn't download preset");
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        private void DeleteClicked(object sender, RoutedEventArgs _)
        {
            string fileAbsolutePath = Settings.Preset.Default.PresetsFolderLocation + "/" + this.PresetData.Name;

            try
            {
                System.IO.File.Delete(fileAbsolutePath);
                RaiseEvent(new RoutedEventArgs(PresetDeletedEvent, this));

                (Application.Current.MainWindow as MainWindow)?.RootInfoSnackbar.Show("Deleted Preset", $"Preset {this.PresetData.Name} has been removed from your game");

            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't delete file");
                System.Diagnostics.Debug.WriteLine(e);
            } 
        }
    }
}
