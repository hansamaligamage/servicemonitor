using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceGenerator
{
    public class ServiceCall
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int Frequency { get; set; }
        public int GraceTime { get; set; }
    }
}
