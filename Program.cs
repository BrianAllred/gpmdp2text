using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using gpmdp2text.Models;
using Newtonsoft.Json;

namespace gpmdp2text
{
    public class Program
    {
        private static readonly object configLock = new object();

        private static Config config;
        private static PlaybackWatcher playbackWatcher;

        public static void Main(string[] args)
        {
            lock (configLock)
            {
                if (File.Exists(Constants.ConfigFilePath))
                {
                    try
                    {
                        config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Constants.ConfigFilePath));

                        // Set up file watcher for config file
                        var configWatcher = new PollingFileSystemWatcher(Directory.GetParent(Constants.ConfigFilePath).FullName, "config.json");
                        configWatcher.PollingInterval = 1000;
                        configWatcher.Changed += UpdateConfig;
                        configWatcher.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading config file: {ex.Message}");
                        Console.WriteLine(ex);
                    }
                }
            }

            SetupPlaybackWatcher();

            Task.Delay(Timeout.Infinite).Wait();
        }

        private static void UpdateConfig(object sender, EventArgs e)
        {
            lock (configLock)
            {
                try
                {
                    config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Constants.ConfigFilePath));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading config file: {ex.Message}");
                    Console.WriteLine(ex);
                }

                SetupPlaybackWatcher();
            }
        }

        private static void SetupPlaybackWatcher()
        {
            // Set up file watcher for gpmdp playback
            if (playbackWatcher != null)
            {
                playbackWatcher.Dispose();
            }

            playbackWatcher = new PlaybackWatcher(config);
            playbackWatcher.Start();
        }
    }
}
