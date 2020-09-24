using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Aye.Core
{
    public class Playlist<T> where T : class, ITrack
    {
        private readonly LinkedList<T> _tracks = new LinkedList<T>();
        private readonly Thread _watcher;
        public IEnumerable<T> Tracks => _tracks.ToImmutableList();
        public T Current => _tracks.First?.Value;

        public Playlist()
        {
            _watcher = new Thread(Skip) {IsBackground = true};
            _watcher.Start();
        }

        private void Skip()
        {
            while (true)
            {
                if (DateTime.Now > Current?.Stop)
                {
                    _tracks.RemoveFirst();
                    Console.WriteLine($"Current track has change {Current}");
                }
                
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            // ReSharper disable once FunctionNeverReturns
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