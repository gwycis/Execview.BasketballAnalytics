using System;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements
{
    public sealed class GoldMedalAchievement : Achievement
    {
        public GoldMedalAchievement(string name, decimal ppg) : base(ppg)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Gold = name;
        }

        public string Gold { get; }
    }
}