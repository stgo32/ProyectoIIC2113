namespace RawDeal;


using RawDeal.Superstars;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;


public class SuperstarConverter : JsonConverter<Superstar>
{
    private string _property = "Name";
    public override Superstar Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var jsonObject = jsonDocument.RootElement;
        var name = jsonObject.GetProperty(_property).GetString();
        Superstar superstar = DeserializeSuperstarByName(name, jsonObject, options);
        return superstar;
    }

    private Superstar DeserializeSuperstarByName(string name, JsonElement jsonObject, JsonSerializerOptions options)
    {
        Superstar superstar;
        switch (name)
        {
            case "STONE COLD STEVE AUSTIN":
                superstar = JsonSerializer.Deserialize<StoneCold>(jsonObject.GetRawText(), options);
                break;
            case "THE UNDERTAKER":
                superstar = JsonSerializer.Deserialize<Undertaker>(jsonObject.GetRawText(), options);
                break;
            case "MANKIND":
                superstar = JsonSerializer.Deserialize<ManKind>(jsonObject.GetRawText(), options);
                break;
            case "HHH":
                superstar = JsonSerializer.Deserialize<HHH>(jsonObject.GetRawText(), options);
                break;
            case "THE ROCK":
                superstar = JsonSerializer.Deserialize<TheRock>(jsonObject.GetRawText(), options);
                break;
            case "KANE":
                superstar = JsonSerializer.Deserialize<Kane>(jsonObject.GetRawText(), options);
                break;
            case "CHRIS JERICHO":
                superstar = JsonSerializer.Deserialize<Jericho>(jsonObject.GetRawText(), options);
                break;
            default:
                throw new JsonException($"Unknown Superstar: {name}");
        }
        return superstar;
    }

    public override void Write(Utf8JsonWriter writer, Superstar value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}