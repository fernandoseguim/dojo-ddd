using System;
using System.Collections.Generic;
using System.Linq;
using TimeZoneConverter;

namespace DojoDDD.Infra.Providers.BusinessPeriod
{
    public static class DatetimeExtensions
    {
        public static DateTime Concat(this DateTime date, string hour) => Convert.ToDateTime($"{date:d} { hour }");

        public static DateTime ToBrazilianTime(this DateTime date) => TimeZoneInfo.ConvertTime(date, TZConvert.GetTimeZoneInfo(Environment.GetEnvironmentVariable("TZ") ?? "America/Sao_Paulo"));
        public static DateTime ToBrazilianTime(this DateTime? date) => date != null ? ToBrazilianTime((DateTime)date) : DateTime.UtcNow.ToBrazilianTime();

        public static bool IsWeekDay(this DateTime date)
            => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;

        public static bool IsWeekendDay(this DateTime date)
            => !date.IsWeekDay();

        public static bool IsHoliday(this DateTime date)
            => GetNationalHolidaysDates(date.Year).Any(pair => pair.Value.Date.Equals(date.Date));

        private static DateTime GetEasterDate(int year)
        {
            var a = year % 19;
            var b = year % 4;
            var c = year % 7;
            var d = ((19 * a) + 24) % 30;
            var e = ((6 * d) + (4 * c) + (2 * b) + 5) % 7;
            var easter = new DateTime(year, 3, 22).AddDays(d + e);

            switch (easter.Day)
            {
                case 26:
                    easter = new DateTime(year, 4, 19);
                    break;
                case 25:
                    if (a > 10)
                        easter = new DateTime(year, 4, 18);
                    break;
            }

            return easter;
        }

        private static Dictionary<string, DateTime> GetNationalHolidaysDates(int year)
        {
            var easter = GetEasterDate(year);
            var holidays = new Dictionary<string, DateTime>
            {
                {"UniversalFellowship", new DateTime(year, 1, 1)},
                {"ChristPassion", easter.AddDays(-2)},
                {"CorpusChristi", easter.AddDays(60)},
                {"Tiradentes", new DateTime(year, 4, 21)},
                {"WorkDay", new DateTime(year, 5, 1)},
                {"IndependenceDay", new DateTime(year, 9, 7)},
                {"OurLadyDay", new DateTime(year, 10, 12)},
                {"DayOfTheDead", new DateTime(year, 11, 2)},
                {"ProclamationOfRepublic", new DateTime(year, 11, 15)},
                {"Christmas", new DateTime(year, 12, 25)}
            };

            return holidays;
        }
    }
}