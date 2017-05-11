using System;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace DNP_CourseAssignment
{//Server side thread handling one client
    public class HandleClientConnection
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mikke\Documents\GitHub\DNP_CourseAssignment\DNP_CourseAssignment\DNP_CourseAssignment\ChatSystemDatabase.mdf;Integrated Security=True";
        private string messageFromClient;
        private NetworkStream clientStream;
        private ListBox logList;
        private string userId;
        private ServerConnection srvCon;
        private string channelName;

        public HandleClientConnection(NetworkStream clientStream, ListBox log, string user,ServerConnection srv)
        {
            this.clientStream = clientStream;
            logList = log;
            userId = user;
            srvCon = srv;
        }

        public void SendMessageToClient(string m, string username)
        {
            IFormatter formatter = new SoapFormatter();
            Packet msg = new Packet(0, username, m);
            formatter.Serialize(clientStream, msg); 
        }

        public void SendUserListToClient(string users)
        {
            IFormatter formatter = new SoapFormatter();
            Packet msg = new Packet(1, users);
            formatter.Serialize(clientStream, msg);
        }

        private void SendLoginResponseToClient(bool success)
        {
            Packet packet = new Packet(2, success);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(clientStream, packet);
        }

        private void SendRegisterResponseToClient(bool success)
        {
            Packet packet = new Packet(3, success);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(clientStream, packet);
        }

        public void ReceiveMessagesFromClient()
        {
            Thread.Sleep(2000);
            byte[] buffer = new byte[128];
            while (true)
            {
                buffer = new byte[128];
                try
                {
                    if (clientStream.DataAvailable)
                    {
                        SoapFormatter formatter = new SoapFormatter();
                        Packet msg = (Packet)formatter.Deserialize(clientStream);
                        int type = msg.type;          

                        switch(type)
                        {
                            case 0://Server received chat message
                                messageFromClient = (string) msg.GetObjects(0);
                                string timestamp = GetTimestamp(DateTime.Now);
                                srvCon.Broadcast(messageFromClient, type, userId, this.channelName);
                                logList.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    logList.Items.Add("[" + timestamp + "] " + userId + ": " + messageFromClient);
                                });
                                break;

                            case 1: //Server received user list request
                                srvCon.Broadcast("", 1, "", "");
                                break;
                            case 2://Server received username|password

                                string username = (string) msg.GetObjects(0);
                                string password = (string)msg.GetObjects(1);
                                Login(username, password);
                                break;
                            case 3://Server received username|password

                                string username2 = (string)msg.GetObjects(0);
                                string password2 = (string)msg.GetObjects(1);
                                Register(username2, password2);
                                break;
                            case 4://Server received channel join request
                                string channelName = (string)msg.GetObjects(0);
                                srvCon.SetUserChannel(userId, channelName);
                                this.channelName = channelName;
                                break;
                            default:
                                Console.WriteLine("HandleClientConnection: Unhandled type: " + type);
                                break;
                        }
                        
                        //  byte[] messageToClient = Encoding.ASCII.GetBytes("Thank you for your message!");
                        // clientStream.Write(messageToClient, 0, messageToClient.Length);

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    break;
                }
            }
        }

        private string GetTimestamp(DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }

        internal void SendChannelJoinConfirmation(string channelName)
        {
            Packet packet = new Packet(4, channelName);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(clientStream, packet);
        }

        private void Login(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string realPassword = "";
                SqlCommand cmd = new SqlCommand("SELECT password FROM dbo.Users WHERE username = '" + username +"'", con);
                object response = cmd.ExecuteScalar();
                if (response != null)
                {
                    realPassword = response.ToString();
                    Console.WriteLine("Username: " + username + " Password: " + password + " Real password: " + realPassword);
                }
                else
                {
                    SendLoginResponseToClient(false);
                    return;
                }

                bool success = password == realPassword;
                SendLoginResponseToClient(password == realPassword);
                if(success)
                { 
                    srvCon.AddUser(new User(this, username));
                    userId = username;
                }

            }
        }

        private void Register(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string queryMessage = "INSERT INTO dbo.Users (username, password) VALUES('" + username + "', '" + password + "')";
                Console.WriteLine(queryMessage);
                SqlCommand cmd = new SqlCommand(queryMessage, con);
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Registered User. Username: " + username + " Password: " + password);
                    SendRegisterResponseToClient(true);
                }
                catch (SqlException)
                {
                    SendRegisterResponseToClient(false);
                }
                
                
            }
        }
    }
}