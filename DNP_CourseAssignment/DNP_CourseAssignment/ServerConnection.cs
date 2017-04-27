using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace DNP_CourseAssignment
{
    class ServerConnection
    {
        private bool isStarted = false;
        private TcpListener listener;
        public ServerConnection ()
        {
            //[] address = { 127, 0, 0, 1 };
            //IPAddress ip = new IPAddress(address);
            listener = new TcpListener(IPAddress.Any, 11000);   
        }

        public void Start ()
        {
            if (isStarted) return;

            listener.Start();
            isStarted = true;
            Console.WriteLine("Starting server..");
            Thread t = new Thread(new ThreadStart(ListenForConnections));
            t.Start();
        }

        public void Stop ()
        {
            isStarted = false;
        }

        public void ListenForConnections ()
        {
            Console.WriteLine("Server started. Listening for connections...");
            while (isStarted)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream ns = client.GetStream();

                Console.WriteLine("Connected to client: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());

                string welcome = "Welcome to the DNPI1 test server";
                byte[] data = Encoding.ASCII.GetBytes(welcome);
                ns.Write(data, 0, data.Length);

                HandleClientConnection hc = new HandleClientConnection(ns);
                Thread handleConnection = new Thread(new ThreadStart(hc.ReceiveMessagesFromClient));
                handleConnection.Start();

            }
        }
    }


}
