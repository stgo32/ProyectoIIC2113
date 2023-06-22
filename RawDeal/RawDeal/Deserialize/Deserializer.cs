namespace RawDeal.Deserialize;


using RawDeal.DeckHandler;
using System.Text.Json;

public static class Deserializer
{
    private static string _cardsPath = "./data/cards.json";
    
    private static string _superstarsPath = "./data/superstar2.json";

    public static SuperstarSet DeserializeInfoSuperstars()
    {
        string json = File.ReadAllText(_superstarsPath);
        var options = new JsonSerializerOptions
        {
            Converters = { new SuperstarConverter() }
        };
        SuperstarSet superstars = new SuperstarSet();
        superstars.Set = JsonSerializer.Deserialize<List<Superstar>>(json, options);
        return superstars;
    }

    public static CardSet DeserializeInfoCards()
    {
        string json = File.ReadAllText(_cardsPath);
        CardSet cards = new CardSet();
        cards.Cards = JsonSerializer.Deserialize<List<Card>>(json);
        return cards;
    }
}