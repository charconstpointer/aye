using System.Collections.Generic;
using Aye.Core.Tracks;

namespace Aye.Core.Clients
{
    public interface ITracksClient<out T> where T : class, ITrack
    {
        IAsyncEnumerable<T> GetTracksAsync();
    }
}