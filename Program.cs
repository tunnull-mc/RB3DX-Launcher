using DiscordRPC;
using System.Diagnostics;

DiscordRpcClient Client = new DiscordRpcClient("1125571051607298190");
var PreviousRPData = new DiscordRP.PlayingData();
var RPCS3Location = PreLaunch.GetRPCLocation();
var HideMainWindow = PreLaunch.IsRPCS3WindowShown();


Setup();
Statuses.SendDefaultPresence(Client);
FileSystemWatcher fsw = new FileSystemWatcher($@"{RPCS3Location}\dev_hdd0\game\BLUS30463\USRDIR", "discordrp.json");
fsw.Changed += RPFile_Changed;
fsw.EnableRaisingEvents = true;
Thread.Sleep(Timeout.Infinite);
void RPFile_Changed(object sender, FileSystemEventArgs e)
{
    DiscordRP.PlayingData CurrentRP = null;

    //Microsoft is evil and allows you to enable events on a changed file which is still locked,
    //do not attempt to replace the FileStream with a File.ReadAllText.
    try
    {
        using (FileStream RPStream = File.Open($@"{RPCS3Location}\dev_hdd0\game\BLUS30463\USRDIR\discordrp.json", FileMode.Open))
        {
            CurrentRP = DiscordRP.Parse(new StreamReader(RPStream).ReadToEnd());
        }
    }
    catch
    {
        return;
    }
    Statuses.UpdatePresence(Client, CurrentRP);
    PreviousRPData = CurrentRP;

    /*
    if (PreviousRPData.ScreenName == CurrentRP.ScreenName)
    {
        SamePresenceCount++;
        if (SamePresenceCount == IdleCutoff)
        {
            Statuses.SendIdlePresence(Client);
        }
    }
    else
    {
        Statuses.UpdatePresence(Client, CurrentRP, new CurrentSong.SongData());
        PreviousRPData = CurrentRP;
        SamePresenceCount = 0;
    }
    */
}
/*
while (true)
{
    DiscordRP.PlayingData CurrentRP = null;
    try
    {
        using(FileStream RPStream = File.Open($@"{RPCS3Location}\dev_hdd0\game\BLUS30463\USRDIR\discordrp.json", FileMode.Open))
        {
            CurrentRP = DiscordRP.Parse(new StreamReader(RPStream).ReadToEnd());
        }
    }
    catch
    {
        return;
    }
    //var CurrentPlaylist = CurrentSong.Parse($@"{RPCS3Location}\dev_hdd0\game\BLUS30463\USRDIR\currentsong.json");
    if (PreviousRPData.ScreenName == CurrentRP.ScreenName)
    {
        SamePresenceCount++;
        if (SamePresenceCount == IdleCutoff)
        {
            Statuses.SendIdlePresence(Client);
        }
    }
    else
    {
        Statuses.UpdatePresence(Client, CurrentRP, new CurrentSong.SongData());
        PreviousRPData = CurrentRP;
        SamePresenceCount = 0;
    }
    Thread.Sleep(PresenceUpdateWait * 1000);
}
*/



void Setup()
{
    Console.WriteLine($"Loading RPCS3 from {RPCS3Location}.");
    var RPCS3Arguments = "";
    if (HideMainWindow == false)
    {
        RPCS3Arguments = $@"{RPCS3Location}\dev_hdd0\game\BLUS30463\USRDIR\EBOOT.BIN";
    }
    else
    {
        RPCS3Arguments = $@"--no-gui {RPCS3Location}\dev_hdd0\game\BLUS30463\USRDIR\EBOOT.BIN";

    }
    var RPCS3Process = new Process()
    {
        StartInfo = new ProcessStartInfo()
        {
            FileName = $@"{RPCS3Location}\rpcs3.exe",
            WorkingDirectory = RPCS3Location,
            Arguments = RPCS3Arguments
        }
    };
    var SystemProcesses = Process.GetProcesses();
    if (Process.GetProcessesByName("RPCS3").Length == 0)
    {
        RPCS3Process.Start();
    }
    RPCS3Process.EnableRaisingEvents = true;
    RPCS3Process.Exited += RPCS3Process_Exited;
    Client.OnConnectionEstablished += DiscordClient_OnConnectionEstablished;
    Client.OnConnectionFailed += DiscordClient_OnConnectionFailed;
    Client.Initialize();
    ShellHacks.MinimizeConsole();
}

void DiscordClient_OnConnectionFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage args)
{
    Console.WriteLine("Failed to connect to Discord RPC, is Discord open?");
}

void DiscordClient_OnConnectionEstablished(object sender, DiscordRPC.Message.ConnectionEstablishedMessage args)
{
    Console.WriteLine("Discord RPC connection established.");
}

void RPCS3Process_Exited(object? sender, EventArgs e)
{
    Client.Dispose();
    Environment.Exit(0);
}