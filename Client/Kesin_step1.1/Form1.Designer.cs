namespace Kesin_step1._1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.disconnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.msgList = new System.Windows.Forms.ListBox();
            this.display = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.label4 = new System.Windows.Forms.Label();
            this.msg = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.inviteBox = new System.Windows.Forms.TextBox();
            this.invite = new System.Windows.Forms.Button();
            this.surrender = new System.Windows.Forms.Button();
            this.declineB = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.acceptB = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.guessBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.guessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(97, 42);
            this.ipBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(127, 22);
            this.ipBox.TabIndex = 2;
            this.ipBox.Text = "127.0.0.1";
            this.ipBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(97, 78);
            this.portBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(127, 22);
            this.portBox.TabIndex = 3;
            this.portBox.Text = "81";
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(277, 39);
            this.connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(127, 25);
            this.connect.TabIndex = 4;
            this.connect.Text = "CONNECT";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(277, 78);
            this.disconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(127, 25);
            this.disconnect.TabIndex = 5;
            this.disconnect.Text = "DISCONNECT";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(97, 119);
            this.nameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(127, 22);
            this.nameBox.TabIndex = 7;
            this.nameBox.Text = "Ven";
            // 
            // msgList
            // 
            this.msgList.FormattingEnabled = true;
            this.msgList.ItemHeight = 16;
            this.msgList.Location = new System.Drawing.Point(15, 177);
            this.msgList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.msgList.Name = "msgList";
            this.msgList.Size = new System.Drawing.Size(391, 180);
            this.msgList.TabIndex = 8;
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(15, 385);
            this.display.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(389, 32);
            this.display.TabIndex = 9;
            this.display.Text = "Display Players";
            this.display.UseVisualStyleBackColor = true;
            this.display.Click += new System.EventHandler(this.display_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 434);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Your Message:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // msg
            // 
            this.msg.Enabled = false;
            this.msg.Location = new System.Drawing.Point(15, 465);
            this.msg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(252, 22);
            this.msg.TabIndex = 11;
            this.msg.TextChanged += new System.EventHandler(this.msg_TextChanged);
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(297, 454);
            this.send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(97, 34);
            this.send.TabIndex = 12;
            this.send.Text = "SEND";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(611, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Invite Player:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(529, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Name:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // inviteBox
            // 
            this.inviteBox.Enabled = false;
            this.inviteBox.Location = new System.Drawing.Point(585, 103);
            this.inviteBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inviteBox.Name = "inviteBox";
            this.inviteBox.Size = new System.Drawing.Size(143, 22);
            this.inviteBox.TabIndex = 15;
            // 
            // invite
            // 
            this.invite.Enabled = false;
            this.invite.Location = new System.Drawing.Point(616, 145);
            this.invite.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.invite.Name = "invite";
            this.invite.Size = new System.Drawing.Size(91, 27);
            this.invite.TabIndex = 16;
            this.invite.Text = "INVITE";
            this.invite.UseVisualStyleBackColor = true;
            this.invite.Click += new System.EventHandler(this.invite_Click);
            // 
            // surrender
            // 
            this.surrender.Enabled = false;
            this.surrender.Location = new System.Drawing.Point(593, 419);
            this.surrender.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.surrender.Name = "surrender";
            this.surrender.Size = new System.Drawing.Size(121, 69);
            this.surrender.TabIndex = 17;
            this.surrender.Text = "SURRENDER";
            this.surrender.UseVisualStyleBackColor = true;
            this.surrender.Click += new System.EventHandler(this.surrender_Click);
            // 
            // declineB
            // 
            this.declineB.Location = new System.Drawing.Point(661, 177);
            this.declineB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.declineB.Name = "declineB";
            this.declineB.Size = new System.Drawing.Size(85, 43);
            this.declineB.TabIndex = 19;
            this.declineB.Text = "DECLINE";
            this.declineB.UseVisualStyleBackColor = true;
            this.declineB.Click += new System.EventHandler(this.declineB_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(590, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Invintation Received!";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // acceptB
            // 
            this.acceptB.Location = new System.Drawing.Point(571, 177);
            this.acceptB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(85, 43);
            this.acceptB.TabIndex = 21;
            this.acceptB.Text = "ACCEPT";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(613, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "The Game:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // guessBox
            // 
            this.guessBox.Enabled = false;
            this.guessBox.Location = new System.Drawing.Point(593, 368);
            this.guessBox.Name = "guessBox";
            this.guessBox.Size = new System.Drawing.Size(121, 22);
            this.guessBox.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(525, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 17);
            this.label9.TabIndex = 24;
            this.label9.Text = "Guess:";
            // 
            // guessButton
            // 
            this.guessButton.Enabled = false;
            this.guessButton.Location = new System.Drawing.Point(730, 359);
            this.guessButton.Name = "guessButton";
            this.guessButton.Size = new System.Drawing.Size(87, 40);
            this.guessButton.TabIndex = 25;
            this.guessButton.Text = "GUESS";
            this.guessButton.UseVisualStyleBackColor = true;
            this.guessButton.Click += new System.EventHandler(this.guessButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 502);
            this.Controls.Add(this.guessButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.guessBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.acceptB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.declineB);
            this.Controls.Add(this.surrender);
            this.Controls.Add(this.invite);
            this.Controls.Add(this.inviteBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.send);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.display);
            this.Controls.Add(this.msgList);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ListBox msgList;
        private System.Windows.Forms.Button display;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox msg;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox inviteBox;
        private System.Windows.Forms.Button invite;
        private System.Windows.Forms.Button surrender;
        private System.Windows.Forms.Button declineB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button acceptB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox guessBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button guessButton;
    }
}

