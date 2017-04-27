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
        public ClientConnection ()
        {
            TcpClient c = new TcpClient("10.52.224.122", 11000);

            NetworkStream serverStream = c.GetStream();
            HandleServerConnection hc = new HandleServerConnection(serverStream);

            //Receiving messages
            Thread connection = new Thread(new ThreadStart(hc.ReceiveMessages));
            connection.Start();
        }
    }
}
