using System;
using System.Collections.Generic;


namespace DNP_CourseAssignment
{//Serializable class used to send different kinds of messages between server and client
    [Serializable]
    public class Packet
    {
        public int type { get; }
        /* 0 - Chat message 
         * 1 - Send/Request user list
         * 2 - Login message
         * 3 - Registration 
         * 4 - Channel join
         */
        private object[] objects;
        private string timestamp;

        public Packet (int type, params object[] objects)
        {
            this.type = type;
            this.objects = objects;
            timestamp = GetTimestamp(DateTime.Now);
        }

        public object GetObjects (int index)
        {
            return objects[index];
        }

        private string GetTimestamp(DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }

        public string GetTimestamp ()
        {
            return timestamp;
        }
    }
}
