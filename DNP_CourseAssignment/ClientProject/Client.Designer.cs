namespace ClientProject
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatBox = new System.Windows.Forms.ListBox();
            this.userList = new System.Windows.Forms.ListBox();
            this.chatText = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.channelText = new System.Windows.Forms.TextBox();
            this.joinChannelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chatBox
            // 
            this.chatBox.FormattingEnabled = true;
            this.chatBox.ItemHeight = 16;
            this.chatBox.Location = new System.Drawing.Point(12, 40);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(609, 356);
            this.chatBox.TabIndex = 0;
            // 
            // userList
            // 
            this.userList.FormattingEnabled = true;
            this.userList.ItemHeight = 16;
            this.userList.Location = new System.Drawing.Point(627, 40);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(167, 356);
            this.userList.TabIndex = 1;
            // 
            // chatText
            // 
            this.chatText.Location = new System.Drawing.Point(12, 446);
            this.chatText.Name = "chatText";
            this.chatText.Size = new System.Drawing.Size(740, 22);
            this.chatText.TabIndex = 2;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(758, 446);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(59, 23);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Chat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(624, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Online Users";
            // 
            // channelText
            // 
            this.channelText.Location = new System.Drawing.Point(627, 402);
            this.channelText.Name = "channelText";
            this.channelText.Size = new System.Drawing.Size(125, 22);
            this.channelText.TabIndex = 7;
            // 
            // joinChannelButton
            // 
            this.joinChannelButton.Location = new System.Drawing.Point(758, 403);
            this.joinChannelButton.Name = "joinChannelButton";
            this.joinChannelButton.Size = new System.Drawing.Size(59, 23);
            this.joinChannelButton.TabIndex = 8;
            this.joinChannelButton.Text = "Join";
            this.joinChannelButton.UseVisualStyleBackColor = true;
            this.joinChannelButton.Click += new System.EventHandler(this.joinChannelButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 405);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Channel:";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 480);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.joinChannelButton);
            this.Controls.Add(this.channelText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.chatText);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.chatBox);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.TextBox chatText;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox channelText;
        private System.Windows.Forms.Button joinChannelButton;
        private System.Windows.Forms.Label label3;
    }
}