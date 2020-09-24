using System;
using System.Linq;
using Aye.Core;
using Aye.Core.Playlist;
using Aye.Core.Tracks;
using FluentAssertions;
using Xunit;

namespace Aye.Tests
{
    public class Track : ITrack
    {
        public DateTime Start { get; }
        public DateTime Stop { get; }

        public Track(DateTime start, DateTime stop)
        {
            Start = start;
            Stop = stop;
        }
    }

    public class PlaylistTests
    {
        [Fact]
        public void Playlists_AddTrack_KeepsCorrectOrder()
        {
            var playlist = new Playlist<Track>();
            var timestamp = DateTime.Now;
            var track0 = new Track(timestamp.AddSeconds(44),timestamp.AddSeconds(92));
            var track1 = new Track(timestamp.AddSeconds(1), timestamp.AddSeconds(2));
            var track2 = new Track(timestamp.AddSeconds(4), timestamp.AddSeconds(5));
            var track3 = new Track(timestamp.AddSeconds(5), timestamp.AddSeconds(10));
            playlist.AddTrack(track0);
            playlist.AddTrack(track2);
            playlist.AddTrack(track3);
            playlist.AddTrack(track1);
            playlist.Tracks.ToList()[0].Should().Be(track1);
            playlist.Tracks.ToList()[1].Should().Be(track2);
            playlist.Tracks.ToList()[2].Should().Be(track3);
            playlist.Tracks.ToList()[3].Should().Be(track0);
        }

        [Fact]
        public void Playlists_Current_ReturnsInCorrectOrder()
        {
            var playlist = new Playlist<Track>();
            var timestamp = DateTime.Now;
            var track0 = new Track(timestamp.AddSeconds(44),timestamp.AddSeconds(92));
            var track1 = new Track(timestamp.AddSeconds(1), timestamp.AddSeconds(2));
            playlist.AddTrack(track0);
            playlist.AddTrack(track1);
            playlist.Current.Should().Be(track1);
        }
    }
}