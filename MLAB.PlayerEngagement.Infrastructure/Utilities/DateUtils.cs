using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Infrastructure.Utilities
{
    public static class DateUtils
    {
        public static string ConvertToUniversalTime(string date, string format)
        {
            if (string.IsNullOrEmpty(date))
                return null;

            DateTime parsedDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
            DateTime universalTime = parsedDate.ToUniversalTime().AddHours(8);

            return universalTime.ToString(format);
        }
    }
}
