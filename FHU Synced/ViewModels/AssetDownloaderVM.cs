using FHU_Synced.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FHU_Synced.ViewModels
{
    public class AssetDownloaderVM : ANotifyPropertyChanged
    {
        public bool _isDownloading;
        public bool IsDownloading
        {
            get => _isDownloading;
            set => AssignAndNotifyPropertyChanged(ref _isDownloading, value);
        }

        public int _percentCompletion;
        public int PercentCompletion
        {
            get => _percentCompletion;
            set => AssignAndNotifyPropertyChanged(ref _percentCompletion, value);
        }
        public async Task DownloadAssetToFile(string source, string destination)
        {
            this.PercentCompletion = 0;
            this.IsDownloading = true;

            using (HttpClient httpClient = new HttpClient()) {
                var (success, httpResponse) = await httpClient.DownloadFileAsync(
                  new(source),
                  destination,
                  null, // CancellationTokenSource 
                  (long bytesRecieved, int percent, float speedKbSec) => { this.PercentCompletion = percent; return true; }
                );

                if (!success)
                    System.Diagnostics.Debug.WriteLine($"Error downloading file {source}");
            }

            this.IsDownloading = false;
        }
    }
}
