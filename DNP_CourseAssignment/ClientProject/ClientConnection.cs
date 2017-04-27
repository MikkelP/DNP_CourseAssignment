using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace ClientProject
{
    class ClientConnection
    {
        public HandleServerConnection hc;

        public ClientConnection ()
        {
            Console.WriteLine("Trying to connect");
            TcpClient c = new TcpClient("10.52.224.122", 11000);

            Console.WriteLine("Trying to connect");
            NetworkStream serverStream = c.GetStream();
            hc = new HandleServerConnection(serverStream);

            //Receiving messages
            Thread connection = new Thread(new ThreadStart(hc.ReceiveMessages));
            connection.Start();
        }
    }
}
