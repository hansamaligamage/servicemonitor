using System;

namespace ServiceGenerator
{
    public class Service
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Outage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
