using Newtonsoft.Json;

public static class DiscordRP
{
    //I don't know who decided \q was a perfectly fine "DELIMITER" for strings, but I would like a word with them.
    //Whether it be a DTA problem, Milohax problem, or Sony problem, I would like a word, and a fight.
    //Parsing this shit took an entire previously productive day, until I had to deal with the quote-ening.
    public static PlayingData Parse(string Input)
    {
        var Output = "";
        Output = Input.Replace(@"\q", "\"");
        Output = Output.Remove(0, 1);
        Output = Output.Remove(Output.Trim().Length - 1);
        var PlayData = JsonConvert.DeserializeObject<PlayingData>(Output);
        return PlayData;
    }
    public class PlayingData
    {
        //PLEASE use JsonPropertys in order to clean up the mutilated JSON from the discordrp.json file.

        [JsonProperty("Game mode")]
        public string GameMode { get; set; }
        [JsonProperty("Loaded Song")]
        public string LoadedSong { get; set; }
        [JsonProperty("Songname")]
        public string SongName { get; set; }
        [JsonProperty("Artist")]
        public string Artist { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("Album")]
        public string Album { get; set; }
        [JsonProperty("Genre")]
        public string Genre { get; set; }
        [JsonProperty("Subgenre")]
        public string SubGenre { get; set; }
        [JsonProperty("Source")]
        public string Source { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        [JsonProperty("Online")]
        public bool IsOnlineMatch { get; set; }
        public Selectedinstrument[] SelectedInstruments { get; set; }
        public class Selectedinstrument
        {
            [JsonProperty("active")]
            public bool IsActive { get; set; }
            [JsonProperty("instrument")]
            private string _Instrument;
            public string Instrument
            {
                get
                {
                    return _Instrument;
                }
                set
                {
                    _Instrument = char.ToUpper(Instrument[0]) + Instrument.Substring(1).ToLower();
                }
            }


            [JsonProperty("difficulty")]
            private Difficulty _InstrumentDifficulty;
            public string InstrumentDifficulty
            {
                get
                {
                    return _InstrumentDifficulty.ToString();
                }
                set
                {
                    _InstrumentDifficulty = (Difficulty)Int32.Parse(InstrumentDifficulty);
                }
            }
        }
        [JsonProperty("Screen category")]
        public string ScreenCategory { get; set; }
        [JsonProperty("Current screen")]
        public string ScreenName { get; set; }
    }
}




public enum Difficulty
{
    Warmup = 0,
    Apprentice = 1,
    Solid = 2,
    Moderate = 3,
    Challenging = 4,
    Nightmare = 5,
    Impossible = 6
}
//Waiter, waiter, more useless faux-Enums please, and leave in the unused features!
public class RBInstrument
{
    public string Guitar { get { return "GUITAR"; } }
    public string ProGuitar { get { return "REAL_GUITAR"; } }
    public string Drums { get { return "DRUMS"; } }
    public string Vocals { get { return "VOCALS"; } }
    public string Bass { get { return "BASS"; } }
    public string ProBass { get { return "REAL_BASS"; } }
    public string Keys { get { return "KEYS"; } }
    public string ProKeys { get { return "REAL_KEYS"; } }

}