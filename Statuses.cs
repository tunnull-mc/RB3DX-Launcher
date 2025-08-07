using DiscordRPC;

public static class Statuses
{
    public static void SendDefaultPresence(DiscordRpcClient Client)
    {
        //Where are we? Who knows! Send a generic message because we have no clue if RPCS3 was started from our program or not.
        Client.SetPresence(new RichPresence()
        {
            Details = "Starting RB3DX",
            State = "",
            Assets = new Assets()
            {
                LargeImageKey = "banner",
                LargeImageText = "RB3DX Launcher by tunnull"
            },
            Type = ActivityType.Playing,
            StatusDisplay = StatusDisplayType.Details,
            DetailsUrl = "https://rb3dx.milohax.org"
        });
        Client.Invoke();
    }
    //var,var,var,var,var. Please clean this eventually.
    public static DiscordRP.PlayingData UpdatePresence(DiscordRpcClient Client, DiscordRP.PlayingData RPData)
    {
        Dictionary<string, string> ActiveInstruments = new Dictionary<string, string>();
        var RPDetails = "";
        var RPState = "";
        var SmallImageAsset = "";
        var SmallImageText = "";
        if (RPData.LoadedSong != null)
        {
            foreach (var Instrument in RPData.SelectedInstruments)
            {
                if (Instrument.IsActive == true)
                {
                    ActiveInstruments.Add(Instrument.Instrument,
                        Instrument.InstrumentDifficulty);
                }
            }

            RPDetails = "[Local] Quickplay";
            if (RPData.IsOnlineMatch == true)
            {
                RPDetails = "[Online] Quickplay";
            }
            RPState = RPData.LoadedSong.ToString();
            SmallImageAsset = ActiveInstruments.First().Key.ToLower();
            var InstrumentName = ActiveInstruments.First().Key;
            //Jesus abandoned us long ago.
            var ProperInstrumentName = char.ToUpper(InstrumentName[0]) + InstrumentName.Substring(1).ToLower();
            SmallImageText = $"{ProperInstrumentName} : {ActiveInstruments.First().Value}";
            if (RPData.GameMode == "autoplay")
            {
                SmallImageText = $"Autoplaying : {ActiveInstruments.First().Value}";

            }
        }
        else
        {
            switch (RPData.ScreenName.ToString())
            {
                case "main_hub_screen":
                    if (SmallImageAsset != null)
                    {
                        SmallImageText = $"Last played instrument";
                    }
                    RPDetails = "In the Menus";
                    RPState = "";
                    break;
                case "song_select_screen":
                    if (SmallImageAsset != null)
                    {
                        SmallImageText = $"Last played instrument";
                    }
                    RPDetails = "Choosing a song...";
                    RPState = "";
                    break;
            }
        }
        //party has died of over-declaration.
        Party party = new Party();
        party.ID = "1238918923410289734";
        party.Size = ActiveInstruments.Count;
        party.Max = 4;
        Client.SetPresence(new RichPresence()
        {
            Details = RPDetails,
            State = RPState,
            Assets = new Assets()
            {
                LargeImageKey = "banner",
                LargeImageText = "Rock Band 3 Deluxe",
                SmallImageKey = SmallImageAsset,
                SmallImageText = SmallImageText
            },
            Type = ActivityType.Playing,
            StatusDisplay = StatusDisplayType.Name,
            Party = party,
            //Why isn't this button generated in another function or loop which checks the settings to see if it's required?
            //Who knows! It makes the presence look snappy regardless.. Fuck it! Ship it as an intended feature!
            Buttons =
            [
                new Button()
                {
                    Label = "View this song",
                    Url = $"https://listenbrainz.org/track/{MusicBrainz.GetMBIDForSong(RPData.Artist, RPData.SongName).ToString()}"
                }
            ]
        });
        Client.Invoke();
        return RPData;
    }
}