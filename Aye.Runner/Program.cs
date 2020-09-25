using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aye.Core.Clients;
using Aye.Core.Playlist;
using Aye.Core.Repositories;
using Aye.Core.Tracks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aye.Runner
{
    class Program
    {
        private static async Task Main()
        {
            var logger = new NullLoggerFactory().CreateLogger<RkRepository>();
            var client = new RkTracksClient(new HttpClient());
            var repo = new RkRepository(logger, client);
            var playlist = new Playlist<RkTrack>(repo);
            await playlist.Start();
        }
    }
}