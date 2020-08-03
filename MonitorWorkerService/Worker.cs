using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceGenerator;
using ServiceGenerator.Interfaces;
using ServiceGenerator.RemoteServices;
using ServiceGenerator.ServiceCalls;

namespace MonitorWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRemoteService _remoteService;
        private readonly IRemoteServiceCall _remoteServiceCall;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _remoteService = new RemoteService();
            _remoteServiceCall = new RemoteServiceCall();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var services = _remoteService.GetServices();
                foreach (var call in _remoteServiceCall.GetServiceCalls())
                {
                    MoniterServices(services, call);
                    await Task.Delay(call.Frequency, stoppingToken);
                }
            }
        }

        public virtual void MoniterServices(List<Service> services, ServiceCall call)
        {
            var service = services.FirstOrDefault(s => s.Id == call.ServiceId);
            if (service != null)
            {
                if (service.Outage && service.StartTime < DateTime.Now && service.EndTime > DateTime.Now)
                {
                    _logger.LogInformation("Service outage for service {service} ", service.Id);
                }
                else
                {
                    try
                    {
                        var client = new TcpClient(service.Host, service.Port);

                        NetworkStream stream = client.GetStream();

                        byte[] bytes = new byte[1024];
                        int bytesRead = stream.Read(bytes, 0, bytes.Length);

                        var response = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        _logger.LogInformation("Response from service {service} to caller {caller} - {response}", service.Id, call.Id, response);
                        client.Close();
                        //TODO:  send notification to the caller using INotification methods
                    }
                    catch(Exception ex)
                    {
                        _logger.LogInformation("Service error {error}", ex.Message);
                    }
                }
            }
        }
    }
}
