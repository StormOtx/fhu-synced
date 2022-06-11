using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FHU_Synced.Abstracts;
using FHU_Synced.Interfaces;
using FHU_Synced.Models;

namespace FHU_Synced.ViewModels
{
    public class PresetsVM : ANotifyPropertyChanged
    {
        readonly IPresetRepository _presetRepository;

        private ObservableCollection<Preset> _unsyncedPresets;
        public ObservableCollection<Preset> UnsyncedPresets
        {
            get => _unsyncedPresets;
            set 
            {
                AssignAndNotifyPropertyChanged(ref _unsyncedPresets, value);
                NotifyPropertyChanged("UnsyncedPresetsNotEmpty");
            }
        }

        private ObservableCollection<Preset> _syncedPresets;
        public ObservableCollection<Preset> SyncedPresets
        {
            get => _syncedPresets;
            set
            {
                AssignAndNotifyPropertyChanged(ref _syncedPresets, value);
                NotifyPropertyChanged("SyncedPresetsNotEmpty");
            }
        }

        public bool UnsyncedPresetsNotEmpty
        {
            get => this._unsyncedPresets != null ? this._unsyncedPresets.Count > 0 : false;
        }

        public bool SyncedPresetsNotEmpty
        {
            get => this._syncedPresets != null ? this._syncedPresets.Count > 0 : false;
        }

        private bool _isLoadingRemote;
        public bool IsLoadingRemote
        {
            get => _isLoadingRemote;
            set => AssignAndNotifyPropertyChanged(ref _isLoadingRemote, value);
        }

        private bool _isLoadingLocal;
        public bool IsLoadingLocal
        {
            get => _isLoadingLocal;
            set => AssignAndNotifyPropertyChanged(ref _isLoadingLocal, value);
        }

        private string _statusTextSyncedPresets;
        public string StatusTextSyncedPresets
        {
            get => _statusTextSyncedPresets;
            set => AssignAndNotifyPropertyChanged(ref _statusTextSyncedPresets, value);
        }

        private string _statusTextUnsyncedPresets;
        public string StatusTextUnsyncedPresets
        {
            get => _statusTextUnsyncedPresets;
            set => AssignAndNotifyPropertyChanged(ref _statusTextUnsyncedPresets, value);
        }

        public PresetsVM(IPresetRepository presetRepository)
        {
            this._unsyncedPresets = new ObservableCollection<Preset>();
            this._syncedPresets = new ObservableCollection<Preset>();
            this._isLoadingRemote = true;
            this._isLoadingLocal = false;
            this._statusTextUnsyncedPresets = "👻 There are currently no new preset available";
            this._statusTextSyncedPresets = "👻 You do not have any installed preset yet";
            this._presetRepository = presetRepository;

            this.InitializeLocalPresets();
            this.InitializeRemotePresets();
        }

        public void InitializeLocalPresets()
        {
            try
            {
                var filePaths = Directory.EnumerateFiles(Settings.Preset.Default.PresetsFolderLocation, "*.fhp");

                foreach (var filePath in filePaths)
                {
                    Preset foundPreset = new Preset();

                    foundPreset.Name = Path.GetFileName(filePath);

                    this.SyncedPresets.Add(foundPreset);
                }
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                this.StatusTextSyncedPresets = "❌ Couldn't retrieve local preset list";
            }
        }

        public async void InitializeRemotePresets()
        {
            try
            {
                List<Preset> retrievedList = new List<Preset>(await this._presetRepository.GetPresets());
                this.UnsyncedPresets = new ObservableCollection<Preset>(retrievedList.ExceptBy(this.SyncedPresets.Select(local => local.Name), remote => remote.Name));
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                this.StatusTextUnsyncedPresets = "❌ Couldn't retrieve remote preset list";
            }

            this.IsLoadingRemote = false;
        }

        public void DeletedPreset(string name)
        {
            var deleted = this.SyncedPresets.SingleOrDefault(x => x.Name == name);


            if (!String.IsNullOrEmpty(deleted.Name))
                this.SyncedPresets.Remove(deleted);

            if (this.SyncedPresets.Count == 0)
                NotifyPropertyChanged("SyncedPresetsNotEmpty");
        }

        public void PresetDownloaded(string name)
        {
            var downloaded = this.UnsyncedPresets.SingleOrDefault(x => x.Name == name);

            if (!String.IsNullOrEmpty(downloaded.Name))
            {
                this.UnsyncedPresets.Remove(downloaded);
                this.SyncedPresets.Add(downloaded);
            }

            if (this.UnsyncedPresets.Count == 0)
                NotifyPropertyChanged("UnsyncedPresetsNotEmpty");

            if (this.SyncedPresets.Count == 1)
                NotifyPropertyChanged("SyncedPresetsNotEmpty");
        }
    }
}
