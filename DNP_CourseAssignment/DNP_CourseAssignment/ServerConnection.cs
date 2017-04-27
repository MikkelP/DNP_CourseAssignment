using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace DNP_CourseAssignment
{
    class ServerConnection
    {
        private bool isStarted = false;
        private TcpListener listener;
        private List<TcpClient> users;
        private ListBox listBox;
        private ListBox listLog;

        public ServerConnection (ListBox box , ListBox log)
        {
            //[] address = { 127, 0, 0, 1 };
            //IPAddress ip = new IPAddress(address);
            listBox = box;
            listLog = log;
            users = new List<TcpClient>();
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
                users.Add(client);
                this.listBox.BeginInvoke((MethodInvoker)delegate() 
                {
                    listBox.Items.Add(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                });

                Console.WriteLine(users.Count);
                Console.WriteLine("Connected to client: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());

                string welcome = "Welcome to the DNPI1 test server";
                byte[] data = Encoding.ASCII.GetBytes(welcome);
                ns.Write(data, 0, data.Length);

                HandleClientConnection hc = new HandleClientConnection(ns,listLog, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                Thread handleConnection = new Thread(new ThreadStart(hc.ReceiveMessagesFromClient));
                handleConnection.Start();

            }
        }
    }


}
