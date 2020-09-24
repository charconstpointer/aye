using System;

namespace Aye.Core.Tracks
{
    public class RkTrack : ITrack
    {
        public RkTrack(int id, string title, string artist, DateTime start, DateTime stop)
        {
            var image = "https://i.imgur.com/aj6sB86.png";
            Id = id;
            Title = title;
            Artist = artist;
            Image = image;
            Start = start;
            Stop = stop;
        }

        private RkTrack()
        {
        }

        public int Id { get; }
        public string Title { get; }
        public string Artist { get; }
        public string Image { get; }
        public DateTime Start { get; }
        public DateTime Stop { get; }
        public override string ToString()
        {
            return $"{Title}";
        }
    }
}