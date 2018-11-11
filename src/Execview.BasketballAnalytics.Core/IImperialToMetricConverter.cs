namespace Execview.BasketballAnalytics.Core
{
    public interface IImperialToMetricConverter
    {
        decimal ConvertToCm(int feet, int inches);
    }
}