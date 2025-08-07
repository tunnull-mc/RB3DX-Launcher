using MetaBrainz.MusicBrainz;

public static class MusicBrainz
{
    public static Guid GetMBIDForSong(string Artist, string TrackName)
    {
        //What is a coding convention? Ha! That's for nerds!
        var query = new Query();
        var qreturn = query.FindRecordings($"artist:{Artist} AND recording:{TrackName}", simple: true);
        query.Dispose();
        return qreturn.Results.First().Item.Id;
    }
}