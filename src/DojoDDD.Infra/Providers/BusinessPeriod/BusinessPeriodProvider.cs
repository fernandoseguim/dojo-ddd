using System;
using System.Threading.Tasks;
using DojoDDD.Infra.Providers.BusinessPeriod.Models;
using Microsoft.Extensions.Options;

namespace DojoDDD.Infra.Providers.BusinessPeriod
{
    public class BusinessPeriodProvider : IBusinessPeriodProvider
    {
        public BusinessPeriodProvider(IOptionsMonitor<BusinessPeriodOptions> options)
        {
            if (options is null) { throw new ArgumentNullException(nameof(options)); }
            Options = options.CurrentValue;
        }

        public BusinessPeriodOptions Options { get; }

        public Task<BusinessPeriodModel> GetBusinessPeriodAsync()
        {
            var date = DateTime.UtcNow;

            var window = GetCurrentWindow(date);
            var nextWindow = GetNextOpenWindow(date);

            return Task.FromResult(new BusinessPeriodModel(WindowIsOpen(date, window), window, nextWindow));
        }

        private Window GetCurrentWindow(DateTime date)
        {
            var starTime = date.Concat(Options.DefaultStartHour);
            var endTime = date.Concat(Options.DefaultEndHour);
            var window = new Window(starTime, endTime);
            return window;
        }

        private Window GetNextOpenWindow(DateTime date)
        {
            var nextDate = date.AddDays(1);
            var starTime = nextDate.Concat(Options.DefaultStartHour);
            var endTime = nextDate.Concat(Options.DefaultEndHour);

            Window nextWindow;

            do nextWindow = new Window(starTime, endTime);
            while(IsBlockedDate(nextDate));

            return nextWindow;
        }

        private static bool WindowIsOpen(DateTime date, Window window)
        {
            var inWindow = date >= window.StartTime && date <= window.EndTime;

            return inWindow && !IsBlockedDate(date);
        }

        private static bool IsBlockedDate(DateTime date) => date.IsHoliday() || date.IsWeekendDay();
    }
}