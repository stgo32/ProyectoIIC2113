namespace RawDeal;


using System.Text.Json;


public static class Deserializer
{
    private static string _cardsPath = "./data/cards.json";
    private static string _superstarsPath = "./data/superstar2.json";

    public static List<Superstar> DeserializeInfoSuperstars()
    {
        string json = File.ReadAllText(_superstarsPath);
        var options = new JsonSerializerOptions
        {
            Converters = { new SuperstarConverter() }
        };
        List<Superstar> superstars = JsonSerializer.Deserialize<List<Superstar>>(json, options);
        return superstars;
    }

    public static List<Card> DeserializeInfoCards()
    {
        string json = File.ReadAllText(_cardsPath);
        List<Card> cards = JsonSerializer.Deserialize<List<Card>>(json);
        return cards;
    }
}