using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace Aye.Core
{
    public class RkRepository : ITracksRepository<RkTrack>
    {
        private readonly ILogger<RkRepository> _logger;
        private readonly HttpClient _httpClient;

        public RkRepository(ILogger<RkRepository> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async IAsyncEnumerable<RkTrack> GetTracksAsync()
        {
            _logger.LogInformation("🔮 fetching tracks");
            var mojePolskieResponse = await _httpClient.GetFromJsonAsync<MojepolskieResponse>(
                $"https://moje.polskieradio.pl/api/?mobilestationid={121}&key=d590cafd-31c0-4eef-b102-d88ee2341b1a");
            mojePolskieResponse.Id = 121;
            for (var i = 0; i < mojePolskieResponse.Songs.Count() - 1; i++)
            {
                var current = mojePolskieResponse.Songs.ElementAt(i);
                var next = mojePolskieResponse.Songs.ElementAt(i + 1);
                current.Stop = next.ScheduleTime;
            }

            foreach (var track in mojePolskieResponse.Songs)
            {
                yield return new RkTrack(track.Id, track.Title, track.Artist, track.ScheduleTime, track.Stop);
            }
        }
    }
}