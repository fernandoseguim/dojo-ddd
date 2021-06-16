using System;

namespace DojoDDD.Infra.Providers.BusinessPeriod.Models
{
    public class BusinessPeriodModel
    {
        public BusinessPeriodModel(bool isOpen, Window currentWindow, Window nextWindow)
        {
            IsOpen = isOpen;
            CurrentWindow = currentWindow;
            NextWindow = nextWindow;
        }

        public bool IsOpen { get; }

        public Window CurrentWindow { get; }

        public Window NextWindow { get; }
    }

    public class Window
    {
        public Window(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
    }
}