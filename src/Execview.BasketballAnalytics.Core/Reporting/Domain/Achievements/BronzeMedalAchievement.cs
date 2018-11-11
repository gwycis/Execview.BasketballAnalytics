using System;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements
{
    public sealed class BronzeMedalAchievement : Achievement
    {
        public BronzeMedalAchievement(string name, decimal ppg) : base(ppg)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Bronze = name;
        }

        public string Bronze { get; }
    }
}