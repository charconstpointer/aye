using System.Collections.Generic;

namespace Aye.Core
{
    public class MojepolskieResponse
    {
        public int Id { get; set; }
        public IEnumerable<MojepolskieTrackResponse> Songs { get; set; }
    }
}