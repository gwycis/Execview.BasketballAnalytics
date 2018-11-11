using System;

namespace Execview.BasketballAnalytics.Core
{
    public sealed class ImperialToMetricConverter : IImperialToMetricConverter
    {
        private const decimal CmPerFeet = 30.48m;
        private const decimal CmPerInch = 2.54m;

        public decimal ConvertToCm(int feet, int inches)
        {
            return Convert.ToDecimal(Math.Round(feet * CmPerFeet + 
                                              inches * CmPerInch, 0, MidpointRounding.AwayFromZero));
        }
    }
}