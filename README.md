# Service Monitor application written in .NET Core
This is a Service monitor application to check the availability of the external services as per the requirements from the caller

## Technology stack 
* .NET Core 3.1 on Visual Studio 2019
* MSTest to write the unit testing for the solution
* Moq framework used to mock the data in test methods

## Structure of the solution
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


