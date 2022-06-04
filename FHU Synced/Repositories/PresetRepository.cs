using FHU_Synced.Interfaces;
using FHU_Synced.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHU_Synced.Repositories
{
    public class PresetRepository : IPresetRepository
    {
        readonly RestClient _client;

        public PresetRepository()
        {
            var options = new RestClientOptions(Settings.URL.Default.PresetServerURL);
            this._client = new RestClient(options);
        }

        public async Task<Preset[]> GetPresets(DateTime? lastUpdated)
        {
            try
            {
                var sinceQuery = lastUpdated != null ? "?since=" + new DateTimeOffset(lastUpdated.Value).ToUnixTimeSeconds().ToString() : "";
                var response = await _client.GetJsonAsync<Preset[]>($"{Settings.URL.Default.GetAllPresetsTrailURL}{sinceQuery}");

                return response;
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't get presets");
                System.Diagnostics.Debug.WriteLine(e);
            }

            return null;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
