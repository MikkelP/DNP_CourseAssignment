using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DNP_CourseAssignment
{
    class HandleClientConnection
    {
        private String messageFromClient;
        private NetworkStream clientStream;

        public HandleClientConnection(NetworkStream clientStream)
        {
            this.clientStream = clientStream;
        }

        public void ReceiveMessagesFromClient()
        {
            byte[] buffer = new byte[128];
            while (true)
            {
                buffer = new byte[128];
                try
                {
                    if (clientStream.DataAvailable)
                    {

                        clientStream.Read(buffer, 0, 128);
                        messageFromClient = Encoding.ASCII.GetString(buffer);
                        Console.WriteLine(messageFromClient);
                        byte[] messageToClient = Encoding.ASCII.GetBytes("Thank you for your message!");
                        clientStream.Write(messageToClient, 0, messageToClient.Length);

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