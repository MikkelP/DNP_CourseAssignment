using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProject
{
    class HandleServerConnection
    {

        private ListBox chatBox;
        private String messageFromClient;

        private NetworkStream connectionStream;
        private bool connected; 

        public HandleServerConnection(NetworkStream clientStream, ListBox _chatBox)
        {

            chatBox = _chatBox;
            connectionStream = clientStream;
            if (clientStream != null)
            {
                connected = true; 
            }
        }

        public void SendMessage(string message)
        {
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            try
            {
                connectionStream.Write(dataToSend, 0, dataToSend.Length);
            } catch (IOException e)
            {
                Console.WriteLine("Please connect to the chat server."); 
            }
        }

        public void ReceiveMessages()
        {
            byte[] buffer = new byte[128];
            while (connected)
            {
                if (connectionStream == null)
                {
                    Console.WriteLine("Disconnected, please reconnect to server.");
                    connected = false;
                    break; 
                }
                buffer = new byte[128];
                try
                {
                    if (connectionStream.DataAvailable)
                    {

                        connectionStream.Read(buffer, 0, buffer.Length);
                        messageFromClient = Encoding.ASCII.GetString(buffer);
                        Console.WriteLine(messageFromClient);
                        this.chatBox.BeginInvoke((MethodInvoker)delegate ()
                        {
                            chatBox.Items.Add(messageFromClient);
                        });
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