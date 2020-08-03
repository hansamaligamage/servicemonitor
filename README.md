# Service Monitor application written in .NET Core
This is a Service monitor application to check the availability of the external services as per the parameters from the caller

## Technology stack 
* .NET Core 3.1 on Visual Studio 2019
* MSTest to write the unit testing for the solution
* Moq framework used to mock the data in test methods

## Structure of the solution
Please find the structure of the project solution as below.
### Services
Two service projects names USService and Asia service, simply returninng the current datetime values
### Service Monitor
Service Monitor is implemented as a worker service to initiate multiple threads as per the method calls from caller
### Caller
Caller data is providede in the RemoteServiceCall class using the IRemoteServiceCall interface. Can extend to get the data from a database, from a file or any other media later
### Service Generator project
This is used to mock the data to services and service calls, for the time being service and call details are hard coded. But can extend to retrive them from other sources later
### Test project
Tests are written in MSTest and Moq framework is used to mock the data. Two test methods has been written to check the service availability and to check the service outage

## How to run the solution
* Start two service projects - USService and Asia Service
* Then start MonitorWorkerService project to start monitoring the services

## In Future
* Service monitor is created as a background worker service and can be deployed as a windows service or daemon service in linux
* Built in Logger is used, can extended to SeriLog to save log details into multiple medias like database, json file, csv file
* A notification (email or sms) can generated and sent to caller after confirming the service is down. check the Notifications folder to identify a suitable way to deliver that
* Caller data / service infroamtion can be retrieved from a database or a json file, at the moment its retrived from the project itself.

## Code Snippets 
### Worker service
This is the heart of the application, worker service collects data from the caller and check the service health
```
    public class Worker : BackgroundService
    {
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
```
       
### Monitor service method
This is a helper method written to check the service aavailability by sending a tcp request. It's going to log the response to the console window
```
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
                        _logger.LogInformation("Response from service {service} to caller {caller} - {response}",
                            service.Id, call.Id, response);
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
```
