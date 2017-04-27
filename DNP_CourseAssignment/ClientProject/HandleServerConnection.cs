using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject
{
    class HandleServerConnection
    {
        private String messageFromClient;
        private NetworkStream connectionStream;

        public HandleServerConnection(NetworkStream clientStream)
        {
            this.connectionStream = clientStream;
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