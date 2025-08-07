using Newtonsoft.Json.Linq;

public static class PreLaunch
{
    //Settings parsers, could be converted into non-static variables to cut down on file reads.
    public static string GetRPCLocation()
    {
        JObject SettingsJson = JObject.Parse(File.ReadAllText("settings.json"));
        var RPCS3Path = SettingsJson.SelectToken("RPCS3").SelectToken("Location").ToString().Replace("/", "\\");
        //Check for Rock Band 3, checks for discordrp.json aswell:
        if (Path.Exists($@"{RPCS3Path}\dev_hdd0\game\BLUS30463\USRDIR\discordrp.json"))
        {
            return RPCS3Path;
        }
        return string.Empty;
    }
    public static bool IsRPCS3WindowShown()
    {
        JObject SettingsJson = JObject.Parse(File.ReadAllText("settings.json"));
        var GUIMode = (bool)SettingsJson.SelectToken("RPCS3").SelectToken("HideMainWindow");
        return GUIMode;
    }
}
