using Aye.Core.Tracks;
using System.Collections.Generic;

namespace Aye.Core.Repositories
{
    public interface ITracksRepository<out T> where T : class, ITrack
    {
        IAsyncEnumerable<T> GetTracksAsync();
    }
}