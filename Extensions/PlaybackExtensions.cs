using gpmdp2text.Models;

namespace gpmdp2text.Extensions
{
    public static class PlaybackExtensions
    {
        public static bool CompareSongs(this Playback playback, Playback otherPlayback)
        {
            return playback.song.title == otherPlayback.song.title
                && playback.song.artist == otherPlayback.song.artist
                && playback.song.album == otherPlayback.song.album;
        }
    }
}