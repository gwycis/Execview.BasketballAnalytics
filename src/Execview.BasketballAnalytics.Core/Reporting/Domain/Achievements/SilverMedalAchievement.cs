using System;

namespace Execview.BasketballAnalytics.Core.Reporting.Domain.Achievements
{
    public sealed class SilverMedalAchievement : Achievement
    {
        public SilverMedalAchievement(string name, decimal ppg) : base(ppg)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Silver = name;
        }

        public string Silver { get; }
    }
}