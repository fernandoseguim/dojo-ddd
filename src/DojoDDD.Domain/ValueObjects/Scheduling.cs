using System;

namespace DojoDDD.Domain.ValueObjects
{
    public class Scheduling
    {
        public Scheduling(DateTime date) => Date = date;

        public Scheduling(Guid token, DateTime date)
        {
            Token = token;
            Date = date;
        }

        public Guid? Token { get; }
        public DateTime Date { get; }
    }
}