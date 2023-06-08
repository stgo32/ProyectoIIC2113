namespace RawDeal;


public class HibridFortitudeHandler
{
    Dictionary<string, Dictionary<string, int>> _hibridFortitude = new Dictionary<string, Dictionary<string, int>>();


    private void SetFortitudeCollection()
    {
        _hibridFortitude.Add(
            "Undertaker's Tombstone Piledriver", 
            new Dictionary<string, int> { { "Maneuver", 30 }, { "Action", 0 } }
        );
    }

    public Dictionary<string, int> GetFortitudeCollection(string cardTitle)
    {
        try 
        {
            return _hibridFortitude[cardTitle];
        }
        catch (KeyNotFoundException)
        {
            return new Dictionary<string, int> { { "Maneuver", 0 }, { "Action", 0 } };
        }
    }
}
