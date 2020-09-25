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

        private bool Equals(RkTrack other)
        {
            return Title == other.Title && Artist == other.Artist && Start.Equals(other.Start) && Stop.Equals(other.Stop);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((RkTrack)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Artist, Start, Stop);
        }
    }
}