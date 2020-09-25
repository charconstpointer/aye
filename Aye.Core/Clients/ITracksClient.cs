using Aye.Core.Tracks;
using System.Collections.Generic;

namespace Aye.Core.Clients
{
    public interface ITracksClient<out T> where T : class, ITrack
    {
        IAsyncEnumerable<T> GetTracksAsync();
    }
}