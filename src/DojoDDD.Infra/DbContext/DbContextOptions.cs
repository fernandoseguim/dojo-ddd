namespace DojoDDD.Infra.DbContext
{
    public class DbContextOptions
    {
        public string[] Databases { get; set; }

        public string EventStore { get; set; }
    }
}