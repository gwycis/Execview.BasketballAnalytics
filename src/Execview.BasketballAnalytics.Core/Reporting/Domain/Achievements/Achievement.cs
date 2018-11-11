using Newtonsoft.Json;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements
{
    public abstract class Achievement
    {
        protected Achievement(decimal ppg)
        {
            Ppg = ppg;
        }

        [JsonProperty(PropertyName = "PPG")]
        public decimal Ppg { get; }
    }
}