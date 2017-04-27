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
        ClientConnection client;
        public Client()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (client == null) return;
            //Get text
            String input = chatText.Text;
            client.hc.SendMessage(chatText.Text);
            chatText.Text = "";
            chatBox.Items.Add(input);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (client != null) return;
           client = new ClientConnection();
        }
    }
}
