using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FHU_Synced.Abstracts;
using FHU_Synced.Models;

namespace FHU_Synced.ViewModels
{
    public class PresetsVM : ANotifyPropertyChanged
    {
        private List<Preset> _loadedPresets;
        public List<Preset> LoadedPresets
        {
            get => _loadedPresets;
            set => AssignAndNotifyPropertyChanged(ref _loadedPresets, value);
        }

        public PresetsVM()
        {
            Preset defaultPreset = new Preset();
            defaultPreset.Name = "sm0lcat_3.fhp";

            this.LoadedPresets = new List<Preset> { defaultPreset, defaultPreset, defaultPreset };

            System.Diagnostics.Debug.WriteLine("Initialized default preset");
            System.Diagnostics.Debug.WriteLine(this.LoadedPresets.ToString());
        }
    }
}
