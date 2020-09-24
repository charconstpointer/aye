using System;

namespace Aye.Core
{
    public interface ITrack
    {
        public DateTime Start { get; }
        public DateTime Stop { get; }
    }
}