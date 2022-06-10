using System;
using System.Collections.Generic;
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

        private List<Preset> _unsyncedPresets;
        public List<Preset> UnsyncedPresets
        {
            get => _unsyncedPresets;
            set 
            {
                AssignAndNotifyPropertyChanged(ref _unsyncedPresets, value);
                NotifyPropertyChanged("UnsyncedPresetsNotEmpty");
            }
        }
        public bool UnsyncedPresetsNotEmpty
        {
            get => this._unsyncedPresets != null ? this._unsyncedPresets.Count > 0 : false;
        }

        private bool _isLoadingRemote;
        public bool IsLoadingRemote
        {
            get => _isLoadingRemote;
            set => AssignAndNotifyPropertyChanged(ref _isLoadingRemote, value);
        }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set => AssignAndNotifyPropertyChanged(ref _statusText, value);
        }

        public PresetsVM(IPresetRepository presetRepository)
        {
            this._unsyncedPresets = new List<Preset>();
            this._isLoadingRemote = true;
            this._statusText = "👻 There are currently no preset available";
            this._presetRepository = presetRepository;

            this.InitializeRemotePresets();
        }

        public async void InitializeRemotePresets()
        {
            try
            {
                this.UnsyncedPresets = (await this._presetRepository.GetPresets()).ToList();
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                this.StatusText = "❌ Couldn't retrieve remote preset list";
            }

            this.IsLoadingRemote = false;
        }
    }
}
