using System;

namespace HomeProjectAPI.Tools.Correlations
{
    public class CorrelationsHelper
    {
        public static string BuildCorrelationId()
        {
            return $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssFFF")}";
        }
    }
}