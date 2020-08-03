using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace USServics
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            int port = Convert.ToInt32(config["port"]);

            bool done = false;

            var listener = new TcpListener(IPAddress.Any, port);

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
