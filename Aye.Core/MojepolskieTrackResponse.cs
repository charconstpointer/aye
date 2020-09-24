using System;

namespace Aye.Core
{
    public class MojepolskieTrackResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime ScheduleTime { get; set; }
        public DateTime Stop { get; set; }
    }
}