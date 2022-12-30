using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Remote.Data
{
    public class RemoteCommandService
    {
        public string? targetip { get; set; }

        public string? targetport { get; set; }

        public string? command { get; set; }


        public string ExecuteCommand()
        {
            var host = this.targetip;
            var port = Int16.Parse(this.targetport);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse(host), port));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            //var bytes = new byte[1024];
            //var count = client.Receive(bytes);
            //Console.WriteLine("New message from server: {0}", Encoding.UTF32.GetString(bytes, 0, count));

            var input = this.command;
            client.Send(Encoding.UTF8.GetBytes(input));
            

            var bytes = new byte[1024];
            var count = client.Receive(bytes);
            var recv = Encoding.UTF8.GetString(bytes, 0, count);
            Console.WriteLine("New message from server: {0}", recv);

            client.Close();
            return recv;
        }
    }
}