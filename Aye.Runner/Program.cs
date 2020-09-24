using Aye.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aye.Runner
{
    class Program
    {
        private static async Task Main()
        {
            var logger = new NullLoggerFactory().CreateLogger<RkRepository>();
            var repo = new RkRepository(logger, new HttpClient());
            var playlist = new Playlist<RkTrack>();
            await foreach (var track in repo.GetTracksAsync())
            {
                playlist.AddTrack(track);
            }
            await playlist.StartTracking();
        }
    }
}