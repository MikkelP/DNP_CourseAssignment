using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using System.Web.Http.SelfHost;
using System.Web.Http;

namespace DNP_CourseAssignment
{
    public class ServerConnection
    {
        private bool isStarted = false;
        private TcpListener listener;
        private List<User> users;
        private ListBox listBox;
        private ListBox listLog;
        private static ServerConnection instance;
        public static ServerConnection Instance { get { return instance; } } 

         ServerConnection (ListBox box , ListBox log)
        {
   
            listBox = box;
            listLog = log;
            users = new List<User>();
            listener = new TcpListener(IPAddress.Any, 11000);

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:8080");
            config.MapHttpAttributeRoutes();
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

        }

        public int GetUserCount()
        {
            return users.Count();
        }

        public static ServerConnection getInstance(ListBox b, ListBox log)
        {
            if (instance == null)
            {
                instance = new ServerConnection(b, log);
            } 
            return instance;
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

        public void Broadcast(string info, int type, string username, string channelName)
        {
            Console.WriteLine("ServerConnection: Broadcast(" + info + ", " + type + ") Users length: " + users.Count);
            switch(type)
            {
                case 0:
                    if (channelName == "")
                    {
                        foreach (User user in users)
                        {
                            user.handleClient.SendMessageToClient(info, username);
                        }
                    }
                    else
                    {
                        foreach (User user in users)
                        {
                            if (user.channelName == channelName)
                            {
                                user.handleClient.SendMessageToClient(info, username);
                            }
                        }
                    }
                    break;
                case 1:
                    string names = "";
                    foreach(User user in users)
                    {
                        names += user.userName + " (" + user.channelName + ")|";
                    }
                    Console.WriteLine("Names length: " + names.Length);
                    foreach (User user in users)
                    {
                        user.handleClient.SendUserListToClient(names);
                    }
                    break;
                default:
                    Console.WriteLine("ServerConnection: Unhandled type: " + type);
                    break;
            }
        }


        public void ListenForConnections ()
        {
            Console.WriteLine("Server started. Listening for connections...");
            while (isStarted)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream ns = client.GetStream();
                
                this.listBox.BeginInvoke((MethodInvoker)delegate() 
                {
                    listBox.Items.Add(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                });
                
                Console.WriteLine(users.Count);
                string userName = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                Console.WriteLine("Connected to client: " + userName);

                HandleClientConnection hc = new HandleClientConnection(ns,listLog, userName,this);
              //  users.Add(new User(client,hc, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()));
                
                Thread handleConnection = new Thread(new ThreadStart(hc.ReceiveMessagesFromClient));
                handleConnection.Start();


            }
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void SetUserChannel(string username, string channelName)
        {
            foreach (User user in users)
            {
                if (user.userName == username)
                {
                    Console.WriteLine("User " + username + " joined channel " + channelName);
                    user.channelName = channelName;
                    user.handleClient.SendChannelJoinConfirmation(channelName);
                    return;
                }
            }
        }
    }
    

}
