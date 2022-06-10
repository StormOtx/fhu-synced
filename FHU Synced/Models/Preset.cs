using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FHU_Synced.Models
{
    public struct Preset
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("download_link")]
        public string DownloadLink { get; set; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonPropertyName("size")]
        public Int64 Size { get; set; }
    }
}
