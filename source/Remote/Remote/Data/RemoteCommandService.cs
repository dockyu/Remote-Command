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
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(this.targetip), int.Parse(this.targetport)));

            clientSocket.Send(Encoding.UTF8.GetBytes(this.command));

            byte[] date = new byte[1024];
            int count = clientSocket.Receive(date);
            string? msg = Encoding.UTF8.GetString(date, 0, count);

            Console.ReadKey();
            clientSocket.Close();
            return msg;
        }
    }
}