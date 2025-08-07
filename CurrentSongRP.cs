using Newtonsoft.Json;

public static class CurrentSong
{
    //This parses CurrentSong.json, which contains some more metadata.
    //Not in use due to the timestamp generator in RB3DX being unimplemented.
    public static SongData Parse(string Input)
    {
        Input = File.ReadAllText(Input);
        if (!string.IsNullOrEmpty(Input))
        {
            var Output = "";
            var CurrentSong = Input;
            Output = CurrentSong.Replace(@":\q,\q", ":{\"");
            Output = Output.Replace("\\q", "\"");
            Output = Output.Remove(0, 1);
            Output = Output.Remove(Output.Trim().Length - 1);
            Output = Output + "}}}";
            var Json = JsonConvert.DeserializeObject<SongData>(Output);
            return Json;
        }
        else
        {
            return null;
        }
    }

    public class SongData
    {
        public Playlist Playlist { get; set; }
    }
    public class Playlist
    {
        public Subplaylist SubPlaylist { get; set; }
    }

    public class Subplaylist
    {
        public bool IsModChart { get; set; }
        public bool HasLyrics { get; set; }
        public int VideoStartOffset { get; set; }
        public string CacheRoot { get; set; }
        public int DrumType { get; set; }
        public string Name { get; set; }
        public string NameNoParenthesis { get; set; }
        public string Artist { get; set; }
        public string Charter { get; set; }
        public int IsMaster { get; set; }
        public string Album { get; set; }
        public int AlbumTrack { get; set; }
        public int PlaylistTrack { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public int SongLength { get; set; }
        public string SongLengthTimeSpan { get; set; }
        public int PreviewStart { get; set; }
        public string PreviewStartTimeSpan { get; set; }
        public int PreviewEnd { get; set; }
        public string PreviewEndTimeSpan { get; set; }
        public float Delay { get; set; }
        public Loadingphrase LoadingPhrase { get; set; }
    }

    public class Loadingphrase
    {
        public int HopoThreshold { get; set; }
        public bool EighthNoteHopo { get; set; }
        public int MultiplierNote { get; set; }
        public string Source { get; set; }
        //public Partdifficulties PartDifficulties { get; set; }
        public int BandDifficulty { get; set; }
        public long AvailableParts { get; set; }
        public int VocalParts { get; set; }
        public string Checksum { get; set; }
        public string NotesFile { get; set; }
        public string Location { get; set; }
    }

    //Class incomplete due to errors being thrown during JsonConvert parsing,
    //this sub-section is almost a direct copy of DiscordRP.PlayingData.SelectedInstrument
}