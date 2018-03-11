using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryData.Models;

namespace LibraryServices
{
    public class DateHelpers
    {
        public static IEnumerable<string> HumanizeBusinessHours(IEnumerable<BranchHours> branchHours)
        {
            return (
                from time in branchHours
                let day = HumanizeDayOfWeek(time.DayOfWeek)
                let openTime = HumanizeTime(time.OpenTime)
                let closeTime = HumanizeTime(time.CloseTime)
                select $"{day} {openTime} to {closeTime}"
            ).ToList();
        }

        private static string HumanizeDayOfWeek(int number)
        {
            // 1-> Sunday
            return Enum.GetName(typeof(DayOfWeek), number-1);
        }

        private static string HumanizeTime(int time)
        {
            var result = TimeSpan.FromHours(time);
            return result.ToString("hh':'mm");
        }
    }
}
