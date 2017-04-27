using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNP_CourseAssignment
{
    public partial class Server : Form
    {
        private ServerConnection connection;

        public Server()
        {
            InitializeComponent();
            connection = new ServerConnection();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            connection.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            connection.Stop();
        }
    }
}
