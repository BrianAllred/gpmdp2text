namespace gpmdp2text.Models
{
    public class Config
    {
        public string formatString { get; set; }
        public int? updateInterval { get; set; }
        public string outputFilePath { get; set; }
        public bool? updateOnTimeChange { get; set; }
    }
}