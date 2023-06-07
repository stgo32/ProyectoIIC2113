namespace RawDeal.Initialize;


using RawDeal.Superstars;


public static class SuperstarFactory
{
    public static Superstar GetSuperstar(Superstar superstarInfo)
    {
        string superstarName = superstarInfo.Name;
        Superstar superstar;
        if (superstarName == "STONE COLD STEVE AUSTIN")
        {
            superstar = new StoneCold();
        }
        else if (superstarName == "THE UNDERTAKER")
        {
            superstar = new Undertaker();
        }
        else if (superstarName == "MANKIND")
        {
            superstar = new ManKind();
        }
        else if (superstarName == "HHH")
        {
            superstar = new HHH();
        }
        else if (superstarName == "THE ROCK")
        {
            superstar = new TheRock();
        }
        else if (superstarName == "KANE")
        {
            superstar = new Kane();
        }
        else if (superstarName == "CHRIS JERICHO")
        {
            superstar = new Jericho();
        }
        else
        {
            throw new Exception("Superstar not found");
        }
        return superstar;
    }
}