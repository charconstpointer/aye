using System;

namespace Aye.Core.Tracks
{
    public interface ITrack
    {
        public DateTime Start { get; }
        public DateTime Stop { get; }
    }
}