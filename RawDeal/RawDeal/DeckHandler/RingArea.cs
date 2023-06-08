namespace RawDeal.DeckHandler;


public class RingArea : CardSet
{
    public int CalculatePlayerFortitude()
    {
        int fortitude = 0;
        foreach (Card card in Cards)
        {
            fortitude += card.GetDamage();
        }
        return fortitude;
    }
}