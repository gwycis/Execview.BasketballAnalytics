using Execview.BasketballAnalytics.Core.Domain;

namespace Execview.BasketballAnalytics.Core.Data
{
    public interface IPlayerParser
    {
        Player Build(string data);
    }
}