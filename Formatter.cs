using System;
using gpmdp2text.Models;

namespace gpmdp2text
{
    public class Formatter
    {
        private Playback playback;

        public Formatter(Playback playback)
        {
            this.playback = playback;
        }

        public override string ToString()
        {
            return ToString(this.playback);
        }

        public string ToString(string format)
        {
            return ToString(this.playback, format);
        }

        public static string ToString(Playback playback)
        {
            return $"{playback?.song?.title ?? "?"} - {playback?.song?.artist ?? "?"}";
        }

        public static string ToString(Playback playback, string format)
        {
            string result = format.Replace("%TITLE%", playback?.song?.title ?? "?")
                                  .Replace("%ARTIST%", playback?.song?.artist ?? "?")
                                  .Replace("%ALBUM%", playback?.song?.album ?? "?")
                                  .Replace("%BR%", Environment.NewLine);

            return result;
        }
    }
}