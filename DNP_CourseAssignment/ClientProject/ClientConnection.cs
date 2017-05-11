using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ClientProject
{
    public class ClientConnection
    {
        public HandleServerConnection hc;
        private LoginRegistration lr;

        public ClientConnection (LoginRegistration lr)
        {
            Thread t = new Thread(new ThreadStart(TryConnect));
            this.lr = lr;
            t.Start(); 
        }

        public void SetListBoxes(ListBox chatBox, ListBox userList)
        {
            hc.SetListBoxes(chatBox, userList);
        }

        internal void Register(string username, string password)
        {
            hc.SendRegisterRequest(username, password);
        }

        public void Login(string username, string password)
        {
            hc.SendLoginRequest(username, password);
        }

        public void TryConnect()
        {
            TcpClient c = null;

            while (c == null)
            {
                try
                {
                    c = new TcpClient("localhost", 11000);
                } catch (SocketException)
                {
                    Console.WriteLine("Failed to connect, reconnecting in 10 seconds...");
                    Thread.Sleep(10000);
                }
            }
//....
            NetworkStream serverStream = c.GetStream();
            hc = new HandleServerConnection(serverStream, lr);

            //Receiving messages
            Thread connection = new Thread(new ThreadStart(hc.ReceiveMessages));
            connection.Start();
        }
    }
}
