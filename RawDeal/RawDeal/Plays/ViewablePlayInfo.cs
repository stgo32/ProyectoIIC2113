namespace RawDeal;

using RawDealView.Formatters;

public class ViewablePlayInfo : IViewablePlayInfo
{
    public IViewableCardInfo CardInfo { get; set; }
    public string PlayedAs { get; set; }
    public ViewablePlayInfo(IViewableCardInfo cardInfo, string playedAs)
    {
        CardInfo = cardInfo;
        PlayedAs = playedAs;
    }
}
