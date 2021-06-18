namespace DojoDDD.Api.Extensions.DbContext.RavenDb
{
    public class RavenDbClusterConnection
    {
        public string[] Endpoints { get;set; }
        public string Certificate { get;set; }
        public string Password { get;set; }
    }
}