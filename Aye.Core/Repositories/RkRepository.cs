using Aye.Core.Clients;
using Aye.Core.Tracks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Aye.Core.Repositories
{
    public class RkRepository : ITracksRepository<RkTrack>
    {
        private readonly ILogger<RkRepository> _logger;
        private readonly ITracksClient<RkTrack> _tracksClient;

        public RkRepository(ILogger<RkRepository> logger, ITracksClient<RkTrack> tracksClient)
        {
            _logger = logger;
            _tracksClient = tracksClient;
        }

        public async IAsyncEnumerable<RkTrack> GetTracksAsync()
        {
            _logger.LogInformation("🔮 fetching tracks");
            await foreach (var track in _tracksClient.GetTracksAsync())
            {
                yield return track;
            }
        }
    }
}