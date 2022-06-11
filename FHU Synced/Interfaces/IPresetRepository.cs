using FHU_Synced.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHU_Synced.Interfaces
{
    public interface IPresetRepository
    {
        public Task<Preset[]?> GetPresets(DateTime? lastUpdated = null);
    }
}
