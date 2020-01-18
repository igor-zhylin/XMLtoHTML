
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
            /* Serie serie = new Serie.Builder("testSeries")
                            .Columns("value1", "value2")
                            .Values(DateTime.Now.Millisecond, 5)
                            .Build();
            InfluxDbApiResponse writeResponse =  _client.WriteAsync("MyDb", TimeUnit.Milliseconds, serie);*/
        }


    

    }
}
