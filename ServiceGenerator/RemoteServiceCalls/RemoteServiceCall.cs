using ServiceGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceGenerator.ServiceCalls
{
    public class RemoteServiceCall : IRemoteServiceCall
    {
        public List<ServiceCall> GetServiceCalls()
        {
            List<ServiceCall> serviceCalls = new List<ServiceCall>() { new ServiceCall { Id = 1, ServiceId = 1, Frequency = 1000, GraceTime = 5000}, 
                new ServiceCall { Id = 2, ServiceId = 2, Frequency = 2000, GraceTime = 10000 }, new ServiceCall { Id = 3, ServiceId = 2, Frequency = 1000, GraceTime = 3000 } };
            return serviceCalls;
        }
    }
}
