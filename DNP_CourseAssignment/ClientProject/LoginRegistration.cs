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
    public partial class LoginRegistration : Form
    {
        ClientConnection c;
        public LoginRegistration()
        {
            InitializeComponent();
            ConnectToServer();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            c.Register(usernameText.Text, passwordText.Text);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            c.Login(usernameText.Text, passwordText.Text);

        }

        public void LoginResponse(bool success)
        {
            if (success)
            {
                Client client = new Client(c);

                Hide();
                client.Show();

                c.hc.SendUserListRequest();
            }
            else
            {
                MessageBox.Show("Wrong password or username");
            }
        }
        public void RegisterResponse(bool success)
        {
            if (success)
            {
                MessageBox.Show("You registered your account successfully");
            }
            else
            {
                MessageBox.Show("You failed to register your account");
            }
        }

        private void ConnectToServer ()
        {
            if (c != null) return;
            c = new ClientConnection(this);
        }

    }
}
