using System.Collections.Generic;

namespace Aye.Core.Clients.Responses
{
    public class MojepolskieResponse
    {
        public int Id { get; set; }
        public IEnumerable<MojepolskieTrackResponse> Songs { get; set; }
    }
}