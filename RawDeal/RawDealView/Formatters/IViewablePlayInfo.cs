namespace RawDealView.Formatters;

public interface IViewablePlayInfo
{
    IViewableCardInfo CardInfo { get; }
    String PlayedAs { get; }
}