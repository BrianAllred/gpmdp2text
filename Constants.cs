using System;
using System.IO;

namespace gpmdp2text
{
    public static class Constants
    {
        public static readonly string PlaybackFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Google Play Music Desktop Player", "json_store", "playback.json");
        public static readonly string ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "gpmdp2text", "config.json");
        public static string PlaybackTextFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Google Play Music Desktop Player", "json_store", "playback.txt");
    }
}