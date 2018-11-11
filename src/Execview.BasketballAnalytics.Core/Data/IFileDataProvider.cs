using System.Collections.Generic;
using System.Threading.Tasks;
using Execview.BasketballAnalytics.Core.Domain;

namespace Execview.BasketballAnalytics.Core.Data
{
    public interface IFileDataProvider
    {
        Task<IReadOnlyList<Player>> GetPlayersAsync(string datafile);
    }
}