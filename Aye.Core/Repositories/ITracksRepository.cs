using System.Collections.Generic;
using Aye.Core.Tracks;

namespace Aye.Core.Repositories
{
    public interface ITracksRepository<out T> where T : class, ITrack
    {
        IAsyncEnumerable<T> GetTracksAsync();
    }
}