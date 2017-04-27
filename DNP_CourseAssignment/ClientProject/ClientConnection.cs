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
            Thread t = new Thread(new ThreadStart(TryConnect));
            t.Start(); 
        }

        public void TryConnect()
        {
            TcpClient c = null;

            while (c == null)
            {
                try
                {
                    c = new TcpClient("10.52.224.122", 11000);
                } catch (SocketException e)
                {
                    Console.WriteLine("Failed to connect, reconnecting in 10 seconds..."); 
                }
                Thread.Sleep(10000); 
            }

            NetworkStream serverStream = c.GetStream();
            hc = new HandleServerConnection(serverStream);

            //Receiving messages
            Thread connection = new Thread(new ThreadStart(hc.ReceiveMessages));
            connection.Start();
        }
    }
}
