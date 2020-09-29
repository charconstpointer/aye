using Aye.Core.Repositories;
using Aye.Core.Tracks;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aye.Core.Playlist
{
    public class Playlist<T> where T : class, ITrack
    {
        private readonly ITracksRepository<T> _tracksRepository;
        private readonly LinkedList<T> _tracks = new LinkedList<T>();
        public bool IsStable => _checks == RequiredChecks;
        private const uint RequiredChecks = 3;
        private uint _checks;
        private int _lastCheckSum;
        public IEnumerable<T> Tracks => _tracks.ToImmutableList();
        public bool IsEmpty => !_tracks.Any();
        private static Func<T, bool> FilterAlreadyPlayed => t => t.Start >= DateTime.Now;
        public T Current => _tracks.First?.Value;


        public Playlist(ITracksRepository<T> tracksRepository)
        {
            _tracksRepository = tracksRepository;
        }

        private async Task Stabilise()
        {
            while (!IsStable)
            {
                await FetchTracks();
                await Task.Delay(TimeSpan.FromMinutes(3));
            }
        }
        private async Task FetchTracks()
        {
            var tracks = await _tracksRepository.GetTracksAsync().ToListAsync();
            var notYetPlayed = tracks.Where(FilterAlreadyPlayed);
            foreach (var track in notYetPlayed)
            {
                AddTrack(track);
            }

            var currentCheckSum = _tracks.GetHashCode();
            if (currentCheckSum == _lastCheckSum)
            {
                _checks++;
                Console.WriteLine(_checks);
                return;
            }

            _lastCheckSum = currentCheckSum;
        }

        public async Task Start()
        {
            await Task.Run(async () => { await Skip(); });
        }

        private async Task Skip()
        {
            _ = Task.Run(async () => await Stabilise());
            while (true)
            {
                if (DateTime.Now > Current?.Stop)
                {
                    _tracks.RemoveFirst();
                    Console.WriteLine($"Current track has change {Current}");
                }

                var currentTrackEnding = Current?.Stop.Subtract(DateTime.Now);
                if (currentTrackEnding.HasValue)
                {
                    await Task.Delay(currentTrackEnding.Value);
                    continue;
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            // ReSharper disable once FunctionNeverReturns
        }

        public void AddTrack(T track)
        {
            if (_tracks.Contains(track))
            {
                return;
            }

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