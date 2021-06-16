using System;

namespace DojoDDD.Infra.Providers.BusinessPeriod.Models
{
    public class CustomPeriodModel
    {
        public DateTime Date { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
    }
}