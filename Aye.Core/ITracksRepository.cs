using System.Collections.Generic;

namespace Aye.Core
{
    public interface ITracksRepository<out T> where T : class, ITrack
    {
        IAsyncEnumerable<T> GetTracksAsync();
    }
}