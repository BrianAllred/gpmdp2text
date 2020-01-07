using System;
using System.IO;
using gpmdp2text.Extensions;
using gpmdp2text.Models;
using Newtonsoft.Json;

namespace gpmdp2text
{
    public class PlaybackWatcher : IDisposable
    {
        private static readonly object playbackLock = new object();

        private PollingFileSystemWatcher playbackWatcher;
        private string playbackTextFilePath;
        private string formatString;
        private bool updateOnTimeChange;

        private Playback oldPlayback;

        public PlaybackWatcher(Config config)
        {
            // Get config values
            this.playbackTextFilePath = string.IsNullOrWhiteSpace(config?.outputFilePath) ? Constants.PlaybackTextFilePath : config.outputFilePath;
            this.formatString = config?.formatString;
            this.updateOnTimeChange = config?.updateOnTimeChange ?? false;
            int playbackPollingInterval = config?.updateInterval ?? 5;

            // Set up file watcher for gpmdp playback
            playbackWatcher = new PollingFileSystemWatcher(Directory.GetParent(Constants.PlaybackFilePath).FullName, "playback.json");
            playbackWatcher.PollingInterval = playbackPollingInterval * 1000;
            playbackWatcher.Changed += WriteTextFile;
        }

        public void Start()
        {
            playbackWatcher.Start();
            WriteTextFile(null, null);
        }

        private void WriteTextFile(object sender, EventArgs e)
        {
            lock (playbackLock)
            {
                try
                {
                    var fs = new FileStream(Constants.PlaybackFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        var playback = JsonConvert.DeserializeObject<Playback>(sr.ReadToEnd());

                        if (oldPlayback != null && !updateOnTimeChange && playback.CompareSongs(oldPlayback))
                        {
                            oldPlayback = playback;
                            return;
                        }

                        try
                        {
                            if (string.IsNullOrWhiteSpace(formatString))
                            {
                                File.WriteAllText(playbackTextFilePath, Formatter.ToString(playback));
                            }
                            else
                            {
                                File.WriteAllText(playbackTextFilePath, Formatter.ToString(playback, formatString));
                            }

                            oldPlayback = playback;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error writing playback text file: {ex.Message}");
                            Console.WriteLine(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading GPMDP playback file: {ex.Message}");
                    Console.WriteLine(ex);
                }
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    lock (playbackLock)
                    {
                        playbackWatcher.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}