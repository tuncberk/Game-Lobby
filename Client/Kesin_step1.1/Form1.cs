using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kesin_step1._1
{
    public partial class Form1 : Form
    {
        static Socket player = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string name;
        bool dc = true; //Disconnect
        bool open = true;
        bool gaming = false;
        string opponent; //name of the opponent
        int round = 0;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            connect.Enabled = true;
            disconnect.Enabled = false;
            msgList.Enabled = false;
            display.Enabled = false;
            acceptB.Enabled = false;
            declineB.Enabled = false;
            surrender.Enabled = false;
        }

        private void connect_Click(object sender, EventArgs e)
        {
            string IpAddress = ipBox.Text;
            int portNumber = Convert.ToInt32(portBox.Text);
            name = nameBox.Text;

            if (IpAddress.Equals("") || portNumber.Equals(null) || name.Equals(""))
            {
                connect.BackColor = Color.Red; //error
                msgList.Items.Add("Input(s) missing...Try Again");
            }
            else
            {
                try
                {
                    msgList.ClearSelected();
                    player.Connect(IpAddress, portNumber);

                    // "//" is our indicator that we are sending the name
                    byte[] buffer = Encoding.Default.GetBytes("//" + name);
                    player.Send(buffer);


                    // Server sends a message whether if the connection is established or not then sends 1 for connected, 0 for not connected.
                    buffer = new byte[64];
                    int rec = player.Receive(buffer);
                    if (rec <= 0)
                        throw new SocketException();

                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));

                    msgList.Items.Add(newmessage); // Connection feedback from server

                    buffer = new byte[64]; // Does this cause memory leak??
                    rec = player.Receive(buffer);
                    if (rec <= 0)
                        throw new SocketException();
                    newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));  // Server sends 1 or 0 to connected

                    if (newmessage.Equals("#1")) // Connection established
                    {
                        //design stuff
                        connect.BackColor = Color.Green;
                        connect.Enabled = false;
                        ipBox.Enabled = false;
                        portBox.Enabled = false;
                        nameBox.Enabled = false;
                        disconnect.Enabled = true;
                        msgList.Enabled = true;
                        display.Enabled = true;
                        msg.Enabled = true;
                        inviteBox.Enabled = true;
                        invite.Enabled = true;
                        
                        Thread thrReceive = new Thread(new ThreadStart(Receive));
                        thrReceive.Start();

                    }
                    else if (newmessage.Equals("#0")) // Connection failed
                    {
                        msgList.Items.Add("Try again..");
                        player.Close();
                        player = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    else //Error
                    {
                        msgList.Items.Add("Error: Server did not respond correctly");
                        msgList.Items.Add(newmessage);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Can not connect to server!");
                }
            }

        }

        private void Receive()
        {
            dc = false; // True if client disconnects

            while (!dc)
            {
                try
                {
                    byte[] buffer = new byte[128];
                    int rec = player.Receive(buffer);
                    if (rec <= 0 && !dc)
                        throw new SocketException();

                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));
                    // Byte received from server
                    if (newmessage.Equals("#dc")) // Disconnect from server
                    {

                        player.Close();
                        player = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        if (open)
                        {
                            connect.Invoke(new MethodInvoker(delegate
                                {
                                    connect.Enabled = true;
                                }));

                            disconnect.Invoke(new MethodInvoker(delegate
                                {
                                    disconnect.Enabled = false;
                                }));


                            dc = true;
                            msgList.Invoke(new MethodInvoker(delegate
                            {
                                msgList.Items.Add("Server connection terminated...");
                            }));
                        }
                    }
                    else if (newmessage.Length > 4 && newmessage.Substring(0, 4).Equals("#req")) // Invintation received
                    {
                        try
                        {
                            round = 0;
                            string opponent = newmessage.Substring(5); //Name of the invitor
                            //label5.Visible = false;
                            //invite.Visible = false;

                            //label7.Visible = true;
                            inviteBox.Text = opponent;
                            inviteBox.Enabled = false;
                            //acceptB.Visible = true;
                            //declineB.Visible = true;
                            acceptB.Enabled = true;
                            declineB.Enabled = true;

                            gaming = true;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else if (newmessage.Equals("#rejected")) // Server rejected the invintation offer
                    {
                        inviteBox.Clear();
                        invite.Enabled = true;
                        inviteBox.Enabled = true;
                        gaming = false;
                    }
                    else if (newmessage.Length > 9 && newmessage.Substring(0, 9).Equals("#accepted"))
                    {
                        surrender.Enabled = true;
                        inviteBox.Enabled = false;
                        invite.Enabled = false;

                        surrender.Enabled = true;
                        guessBox.Enabled = true;
                        guessButton.Enabled = true;
                        round = 0;
                    }
                    else if (newmessage.Equals("#declined"))
                    {
                        gaming = false;
                        //label5.Visible = true;
                        //label7.Visible = false;
                        //invite.Visible = true;
                        invite.Enabled = true;
                        inviteBox.Clear();
                        //inviteBox.Visible = true;
                        inviteBox.Enabled = true;
                        //acceptB.Visible = false;
                        //declineB.Visible = false;
                        surrender.Enabled = false;
                        round = 0;
                    }
                    else if (newmessage.Equals("#win"))
                    {
                        gaming = false;
                        //label5.Visible = true;
                        //label7.Visible = false;
                        //invite.Visible = true;
                        invite.Enabled = true;
                        inviteBox.Clear();
                        //inviteBox.Visible = true;
                        inviteBox.Enabled = true;
                        //acceptB.Visible = false;
                        //declineB.Visible = false;
                        surrender.Enabled = false;
                    }
                    else if (newmessage.Equals("#lr"))
                    {
                        guessBox.Enabled = true;
                        guessButton.Enabled = true;
                        guessBox.Clear();
                        round++;
                        if (round == 2)
                        {
                            if (!gaming)
                                MessageBox.Show("Error: Surrendering while not in a game!");

                            byte[] buffer2 = Encoding.Default.GetBytes("#sur " + inviteBox.Text);
                            player.Send(buffer2);
                            gaming = false;

                            invite.Enabled = true;
                            inviteBox.Clear();
                            inviteBox.Enabled = true;
                            surrender.Enabled = false;
                            guessButton.Enabled = false;
                            guessBox.Enabled = false;
                            round = 0;
                        }
                    }
                    else if (newmessage.Equals("#wr"))
                    {
                        guessBox.Enabled = true;
                        guessButton.Enabled = true;
                        guessBox.Clear();
                    }
                    else // Server is sending a message
                    {
                        msgList.Items.Add(newmessage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Receive function crashed!");
                    dc = true;
                }

            }
        }

        private void Disconnect()
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes("#dc");
                player.Send(buffer);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Disconnect function crashed!");
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            if (gaming)
            {
                byte[] buffer = Encoding.Default.GetBytes("#sur " + inviteBox.Text);
                player.Send(buffer);
                gaming = false;

                invite.Enabled = true;
                inviteBox.Clear();
                inviteBox.Enabled = true;
                surrender.Enabled = false;
            }

            dc = true;
            Disconnect();
            disconnect.Enabled = false;

        }

        private void display_Click(object sender, EventArgs e)
        {
            if (dc)
            {
                display.Enabled = false;
                return;
            }
            else
                display.Enabled = true;

            try
            {
                byte[] buffer = Encoding.Default.GetBytes("#list");
                player.Send(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Error: Display Players Failed!");
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (dc)
            {
                send.Enabled = false;
                return;
            }
            else
                send.Enabled = true;

            if (msg.Text.Equals(""))
                return;
            string messsage = msg.Text;

            try
            {
                byte[] buffer = Encoding.Default.GetBytes(messsage);
                player.Send(buffer);

                msg.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Your message could not be sent.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!dc)
            {
                if (gaming)
                {
                    byte[] buffer = Encoding.Default.GetBytes("#sur " + inviteBox.Text);
                    player.Send(buffer);
                    gaming = false;

                    invite.Enabled = true;
                    inviteBox.Clear();
                    inviteBox.Enabled = true;
                    surrender.Enabled = false;
                }

                open = false;
                dc = true;
                Disconnect();
            }

        }

        ////////// STEP 2 ///////////////////

        private void invite_Click(object sender, EventArgs e)
        {
            if (dc)
            {
                invite.Enabled = false;
                inviteBox.Enabled = false;
                return;
            }
            else
            {
                if (inviteBox.Text.Equals(""))
                    return;
                string messsage = "#inv " + inviteBox.Text;
                gaming = true;

                try
                {
                    byte[] buffer = Encoding.Default.GetBytes(messsage);
                    player.Send(buffer);


                    inviteBox.Enabled = false;
                    invite.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Error: Failed to send invite");
                }
            }

        }

        private void acceptB_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes("#accept " + inviteBox.Text);
                player.Send(buffer);

                //acceptB.Visible = false;
                //declineB.Visible = false;
                //label7.Visible = false;
                //label5.Visible = true;
                //invite.Visible = true;
                declineB.Enabled = false;
                acceptB.Enabled = false;
                surrender.Enabled = true;

                guessBox.Enabled = true;
                guessButton.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Error: Accept button failed");
            }


        }

        private void declineB_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes("#decline " + inviteBox.Text);
                player.Send(buffer);

                //acceptB.Visible = false;
                //declineB.Visible = false;
                //label7.Visible = false;
                //label5.Visible = true;

                declineB.Enabled = false;
                acceptB.Enabled = false;
                inviteBox.Clear();
                inviteBox.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Error: Decline button failed");
            }

        }

        private void surrender_Click(object sender, EventArgs e)
        {
            if (!gaming)
                MessageBox.Show("Error: Surrendering while not in a game!");

            byte[] buffer = Encoding.Default.GetBytes("#sur " + inviteBox.Text);
            player.Send(buffer);
            gaming = false;

            invite.Enabled = true;
            inviteBox.Clear();
            inviteBox.Enabled = true;
            surrender.Enabled = false;
            guessButton.Enabled = false;
            guessBox.Enabled = false;
            round = 0;
        }

        //////// Step 3 //////////

        private void guessButton_Click(object sender, EventArgs e)
        {
            if (dc || !gaming )
            {
                send.Enabled = false;
                guessButton.Enabled = false;
                guessBox.Enabled = false;
                return;
            }
            else
                send.Enabled = true;

            if (guessBox.Text.Equals(""))
                return;

            //int i = 0;
            //string s = "108";
            //bool result = int.TryParse(s, out i); //i now = 108 
            int n;
            string s = guessBox.Text;
            if (int.TryParse(s, out n))
            {
                string messsage = "#gs " + s;

                guessButton.Enabled = false;
                guessBox.Enabled = false;

                try
                {
                    byte[] buffer = Encoding.Default.GetBytes(messsage);
                    player.Send(buffer);

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Your guess could not be sent.");
                }
            }
            else // Input is not a number
            {
                msgList.Items.Add(guessBox.Text + " is not a number! Try again...");
                guessBox.Clear();
            }
        }





        /////////////////////////Functions I can't Delete//////////////////////////////////

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void msg_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }



    }
}
