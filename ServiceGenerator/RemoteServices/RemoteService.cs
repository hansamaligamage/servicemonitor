using ServiceGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceGenerator.RemoteServices
{
    public class RemoteService : IRemoteService
    {
        public List<Service> GetServices()
        {
            List<Service> allServices = new List<Service> {  new Service { Id = 1, Host = "localhost", Port = 10, Outage = true, StartTime = new DateTime(2020, 07, 31, 10, 00,00), 
                EndTime = new DateTime(2020, 07, 31, 10, 00,00 ) }, new Service{Id = 2, Host = "localhost", Port = 11}};
            return allServices;
        }
    }
}
