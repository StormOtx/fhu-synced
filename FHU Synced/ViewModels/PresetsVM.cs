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
            set => AssignAndNotifyPropertyChanged(ref _unsyncedPresets, value);
        }

        public PresetsVM(IPresetRepository presetRepository)
        {
            this._presetRepository = presetRepository;

            this.InitializePresets();
        }

        public async void InitializePresets()
        {
            this.UnsyncedPresets = (await this._presetRepository.GetPresets()).ToList();
        }
    }
}
