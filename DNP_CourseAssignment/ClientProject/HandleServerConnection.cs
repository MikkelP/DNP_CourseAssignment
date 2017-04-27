using System;
using System.Collections.Generic;
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

        public HandleServerConnection(NetworkStream clientStream, ListBox _chatBox)
        {
            this.connectionStream = clientStream;
            chatBox = _chatBox;
        }

        public void SendMessage(string message)
        {
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            connectionStream.Write(dataToSend, 0, dataToSend.Length);

        }

        public void ReceiveMessages()
        {
            byte[] buffer = new byte[128];
            while (true)
            {
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
                    break;
                }
            }
        }
    }
}