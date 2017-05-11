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
        public ClientConnection c;
        public Client(ClientConnection c)
        {
            InitializeComponent();
            this.c = c;
            if (chatBox == null) Console.WriteLine("chatBox null");
            this.c.SetListBoxes(chatBox, userList);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (c == null) return;
            //Get text
            String input = chatText.Text;
            c.hc.SendMessage(chatText.Text);
            chatText.Text = "";
          //  chatBox.Items.Add(input);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public ListBox GetMsgList()
        {
            return chatBox; 
        }

        public ListBox GetUserList()
        {
            return userList; 
        }

        private void joinChannelButton_Click(object sender, EventArgs e)
        {
            string channelName = channelText.Text;

            c.hc.JoinChannel(channelName);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
