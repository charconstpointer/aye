using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aye.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

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

            Console.ReadKey();
        }
    }
}