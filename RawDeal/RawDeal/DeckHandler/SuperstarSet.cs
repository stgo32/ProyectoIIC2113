namespace RawDeal.DeckHandler;


public class SuperstarSet
{
    public List<Superstar> Set { get; set; } = new List<Superstar>();

    public virtual void AddSuperstar(Superstar superstar)
    {
        Set.Add(superstar);
    }
    public int Count()
    {
        return Set.Count;
    }
}