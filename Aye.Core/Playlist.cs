using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Aye.Core
{
    public class Playlist<T> where T : class, ITrack
    {
        private readonly LinkedList<T> _tracks = new LinkedList<T>();
        public IEnumerable<T> Tracks => _tracks.ToImmutableList();
        public T Current => _tracks.First?.Value;
        public T Next => _tracks.First?.Next?.Value;
        public bool IsEmpty => !_tracks.Any();

        private async Task Skip()
        {
            while (Next != null)
            {
                if (DateTime.Now > Current?.Stop)
                {
                    _tracks.RemoveFirst();
                    Console.WriteLine($"Current track has changed {Current}");
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public async Task StartTracking()
        {
            await Task.Run(Skip);
        }

        public void AddTrack(T track)
        {
            if (track.Stop < DateTime.Now)
            {
                Console.WriteLine($"Skipping {track}");
                return;
            }

            if (!_tracks.Any())
            {
                _tracks.AddFirst(track);
                return;
            }

            var currentNode = _tracks.Last;
            while (currentNode != null)
            {
                if (currentNode.Value.Start < track.Start)
                {
                    _tracks.AddAfter(currentNode, track);
                    return;
                }

                currentNode = currentNode.Previous;
            }

            _tracks.AddFirst(track);
        }
    }
}