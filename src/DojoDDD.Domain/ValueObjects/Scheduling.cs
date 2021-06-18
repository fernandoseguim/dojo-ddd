using System;

namespace DojoDDD.Domain.ValueObjects
{
    public class Scheduling
    {
        public Scheduling(Guid? token, DateTime date)
        {
            Token = token;
            Date = date;
        }

        public Guid? Token { get; }
        public DateTime Date { get; }
    }
}