using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ClientProject
{
    class ClientConnection
    {
        private ListBox chatBox;
        public HandleServerConnection hc;

        public ClientConnection (ListBox _chatBox)
        {
            chatBox = _chatBox;
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
                    c = new TcpClient("10.52.227.70", 11000);
                } catch (SocketException e)
                {
                    Console.WriteLine("Failed to connect, reconnecting in 10 seconds..."); 
                    Thread.Sleep(10000); 
                }
            }
//....
            NetworkStream serverStream = c.GetStream();
            hc = new HandleServerConnection(serverStream, chatBox);

            //Receiving messages
            Thread connection = new Thread(new ThreadStart(hc.ReceiveMessages));
            connection.Start();
        }
    }
}
