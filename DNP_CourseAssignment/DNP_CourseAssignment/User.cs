using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace DNP_CourseAssignment
{
    class User
    {
        public TcpClient client { get; }
        public HandleClientConnection handleClient { get; }
        public string userName { get; }

        public User(TcpClient c,HandleClientConnection hc,string userN)
        {
            client = c;
            handleClient = hc;
            userName = userN;           
        }





    }
}
