using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProject
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientConnection client = new ClientConnection();
        }
    }
}
