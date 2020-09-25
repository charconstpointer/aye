using Aye.Core.Clients.Responses;
using Aye.Core.Tracks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace Aye.Core.Clients
{
    public class RkTracksClient : ITracksClient<RkTrack>
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiUrl;

        public RkTracksClient(HttpClient httpClient, string apiUrl = "https://moje.polskieradio.pl/api/?mobilestationid=121&key=d590cafd-31c0-4eef-b102-d88ee2341b1a")
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
        }

        public async IAsyncEnumerable<RkTrack> GetTracksAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<MojepolskieResponse>(_apiUrl);
            response.Id = 121;
            FillMissingDates();

            foreach (var track in response.Songs)
            {
                yield return new RkTrack(track.Id, track.Title, track.Artist, track.ScheduleTime, track.Stop);
            }

            void FillMissingDates()
            {
                for (var i = 0; i < response.Songs.Count() - 1; i++)
                {
                    var current = response.Songs.ElementAt(i);
                    var next = response.Songs.ElementAt(i + 1);
                    current.Stop = next.ScheduleTime;
                }
            }
        }
    }
}