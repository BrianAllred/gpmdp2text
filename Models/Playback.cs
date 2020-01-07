namespace gpmdp2text.Models
{
    public class Playback
    {
        public bool? playing { get; set; }
        public Song song { get; set; }
        public Rating rating { get; set; }
        public Time time { get; set; }
        public string songLyrics { get; set; }
        public string shuffle { get; set; }
        public string repeat { get; set; }
        public int? volume { get; set; }
    }
}