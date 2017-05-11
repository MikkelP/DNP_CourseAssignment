using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using DNP_CourseAssignment;
using System.Threading;

namespace ClientProject
{
    public class HandleServerConnection
    {
        private ListBox chatBox, userList;

        private NetworkStream connectionStream;
        private LoginRegistration lr;
        private bool connected; 

        public HandleServerConnection(NetworkStream clientStream, LoginRegistration lr)
        {
            this.lr = lr;
            connectionStream = clientStream;
            if (clientStream != null)
            {
                connected = true; 
            }
        }

        internal void SetListBoxes(ListBox chatBox, ListBox userList)
        {
            this.chatBox = chatBox;
            this.userList = userList;
        }

        public void SendMessage(string message)
        {
            Packet msg = new Packet(0, message);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(connectionStream, msg);
            Console.WriteLine("HandleServerConnection: SendMessage: " + message);
        }

        internal void JoinChannel(string channelName)
        {
            Packet packet = new Packet(4, channelName);
            SoapFormatter f = new SoapFormatter();
            f.Serialize(connectionStream, packet);
        }

        public void SendUserListRequest()
        {
            Thread.Sleep(1000);//TODO: try to remove
            Packet msg = new Packet(1);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(connectionStream, msg);
        }

        public void SendLoginRequest(string username, string password)
        {
            Packet msg = new Packet(2, username, password);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(connectionStream, msg);
        }

        public void SendRegisterRequest(string username, string password)
        {
            Packet msg = new Packet(3, username, password);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(connectionStream, msg);
        }

        public void ReceiveMessages()
        {
        //    byte[] buffer = new byte[128];
            while (connected)
            {
                if (connectionStream == null)
                {
                    Console.WriteLine("Disconnected, please reconnect to server.");
                    connected = false;
                    break; 
                }
             //   buffer = new byte[128];
                try
                {
                    if (connectionStream.DataAvailable)
                    {
                        SoapFormatter formatter = new SoapFormatter();
                        Packet msg = (Packet) formatter.Deserialize(connectionStream);
                        int type = msg.type;


                        //      connectionStream.Read(buffer, 0, buffer.Length);
                        //    messageFromClient = Encoding.ASCII.GetString(buffer);
                        Console.WriteLine("HandleServerConnection receive messages type " + type);
                        switch(type)
                        {
                            case 0:
                                Console.WriteLine("client case 0");
                                chatBox.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    string username = (string) msg.GetObjects(0);
                                    string content = (string) msg.GetObjects(1);
                                    chatBox.Items.Add("[" + msg.GetTimestamp() + "] " + username + ": " + content);
                                    Console.WriteLine("client case 0, addingn item");
                                });
                                break;

                            case 1:
                                userList.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    string u = (string) msg.GetObjects(0);
                                    string[] users = u.Split('|');
                                    userList.Items.Clear();
                                    foreach (string user in users)
                                    {
                                        userList.Items.Add(user);
                                    }

                                });
                                break;

                            case 2://Login response from server
                                bool success = (bool) msg.GetObjects(0);
                                lr.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    lr.LoginResponse(success);
                                });
                                break;
                            case 3: //Registration response from server
                                bool success2 = (bool)msg.GetObjects(0);
                                lr.RegisterResponse(success2);
                                break;
                            case 4://Received confirmation from server that channel join was successful
                                string channelName = (string)msg.GetObjects(0);
                                SendUserListRequest();
                                chatBox.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    chatBox.Items.Add("You joined channel " + channelName);
                                });
                              
                                    break;
                          
                            default:
                                Console.WriteLine("HandleServerConnection: Unhandled type: " + type);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("Disconnected"); 
                    break;
                }
            }
        }
    }
}