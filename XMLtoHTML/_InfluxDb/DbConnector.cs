
namespace XMLtoHTML._InfluxDb
{
    using System;
    using InfluxDB.Net;
    using InfluxDB.Net.Enums;
    using InfluxDB.Net.Infrastructure.Influx;
    using InfluxDB.Net.Models;

    public class DbConnector
    {
        public DbConnector()
        {
            var _client = new InfluxDb("http://...:8086", "root", "root");
            
        }
    }
}