using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsiaService
{
    class Program
    {
        private static int defaultPort = 0;

        static int Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();


            //Console.WriteLine($" Hello { config["name"] } !");
            if (args.Count() > 0)
                defaultPort = Convert.ToInt32(args[0]);
            else
                defaultPort = Convert.ToInt32(config["port"]);
            bool done = false;

            var listener = new TcpListener(IPAddress.Any, defaultPort);

            listener.Start();

            while (!done)
            {
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                NetworkStream stream = client.GetStream();

                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                try
                {
                    stream.Write(byteTime, 0, byteTime.Length);
                    stream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            listener.Stop();

            return 0;
        }
    }
}
