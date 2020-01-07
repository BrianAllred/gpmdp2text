# gpmdp2text

Utility that will read from [Google Play Music Desktop Player](https://github.com/MarshallOfSound/Google-Play-Music-Desktop-Player-UNOFFICIAL-)'s [JSON API](https://github.com/MarshallOfSound/Google-Play-Music-Desktop-Player-UNOFFICIAL-/blob/master/docs/PlaybackAPI.md) and output the currently playing song title, artist, and album in configurable plain text.

## Configuration

By default, there is no configuration file and the program uses sane default values. To configure gpmdp2text, copy the included `sample-config.json` file to

* Windows: `%APPDATA%\gpmdp2text\config.json`
* OS X: `~/Library/Application Support/gpmdp2text/config.json`
* Linux: `~/.config/gpmdp2text/config.json`

### Values

* `formatString`: Template for plain text output. Available template values are `%TITLE%`, `%ARTIST%`, `%ALBUM%`, and `%BR%` for line breaks. Default is `%TITLE% - %ARTIST%`.
* `updateInterval`: How often to poll the JSON API for changes. Default is 5 seconds.
* `outputFilePath`: File path for plain text output. Default is 
  
  * Windows: `%APPDATA%\Google Play Music Desktop Player\json_store\playback.txt`
  * OS X: `~/Library/Application Support/Google Play Music Desktop Player\json_store\playback.txt`
  * Linux: `~/.config/Google Play Music Desktop Player\json_store\playback.txt`
  
* `updateOnTimeChange`: Set to `true` to update the plain text file as the song progresses. Default is `false`, so updates will only happen on song changes.
