using MonitorWorkerService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceGenerator;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace MonitorWorkerService.Tests
{
    [TestClass()]
    public class ServiceMonitorAvailable
    {

        [TestMethod()]
        public void MonitorServicesTest_IsAvailable()
        {
            var servicesList = new List<Service>();
            var serviceCall = new ServiceCall();
            var worker = new Mock<Worker>(It.IsAny<ILogger>());
            worker.Setup(x => x.MoniterServices(It.IsAny<List<Service>>(), It.IsAny<ServiceCall>())).Verifiable();
            Worker service = worker.Object;
            service.MoniterServices(servicesList, serviceCall);
            worker.VerifyAll();
        }

        [TestMethod]
        public void ServiceMonitorAvailable_IsOutage() 
        {
            var servicesList = new List<Service>();
            var service1 = new Service();
            service1.Id = 1;
            service1.Host = "abc";
            service1.Port = 5000;
            service1.Outage = true;
            service1.StartTime = new DateTime(2000, 1, 1, 1,1,1);
            service1.EndTime = new DateTime(2000, 1, 1, 3, 3, 3);
            servicesList.Add(service1);

            var serviceCall = new ServiceCall();
            serviceCall.Id = 1;

            var worker = new Mock<Worker>(It.IsAny<ILogger>());
            Worker service = (Worker)worker.Object;
            service.MoniterServices(servicesList, serviceCall);
            worker.VerifyAll();
        }
    }
}


