using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kesin_Step_Server1._1
{
    public partial class Form1 : Form
    {

        static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static List<venPlayer> playerList = new List<venPlayer>();




        static int idcount = 0;


        bool active; // Server is active

        class venPlayer
        {
            public Socket client;
            public string name;
            public int id;
            public bool playing = false;
            public venPlayer opponent;

            public int secret;
            public int guessno;
            public bool guessed = false;

            public int gPoint;
            public int roundW;
           
            


            public venPlayer(Socket _client)
            {
                client = _client;
                name = "";
                id = idcount;
                playing = false;
                opponent = this; // Place holder
                guessed = false;
                gPoint = 0;
                roundW = 0;
            }
        }



        public Form1()
        {
            InitializeComponent();
            msgList.Enabled = false;
        }


        private void listen_Click(object sender, EventArgs e)
        {
            int portId = Convert.ToInt32(portBox.Text);
            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, portId));
                server.Listen(3);
                active = true;
                msgList.Items.Add("Server started running...");

                Thread thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.Start();

                //design stuff
                msgList.Enabled = true;
                portBox.Enabled = false;
                listen.BackColor = Color.Green;
                listen.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Error: Listen failed!");
            }
        }

        private void Accept()
        {
            while (active)
            {
                try
                {
                    playerList.Add(new venPlayer(server.Accept()));
                    Thread thReceive = new Thread(new ThreadStart(Receive));
                    thReceive.Start();
                }
                catch
                {
                    active = false;
                }
            }

        }

        private void Receive()
        {
            if (active)
            {
                bool connected = false;
                idcount++;

                venPlayer mainP = playerList[playerList.Count - 1];
                Socket n = playerList[playerList.Count - 1].client; //Last connection
                // Check if the connection should proceed

                try
                {
                    byte[] buffer1 = new byte[64];
                    int rec = n.Receive(buffer1); // Name received
                    if (rec <= 0)
                        throw new SocketException();
                    string newmessage = Encoding.Default.GetString(buffer1);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));

                    //check if the received buffer is a name, 
                    //indicator is "//" at the beginning of string
                    if (newmessage.Substring(0, 2).Equals("//")) // Name sent correctly
                    {
                        bool exists = false;
                        newmessage = newmessage.Substring(2, newmessage.Length - 2);
                        /*foreach (string s in names) 
                        {
                            if (s.Equals(newmessage))
                            {
                                exists = true;
                                break;
                            }
                        }*/
                        foreach (venPlayer p in playerList)
                        {
                            if (p.name.Equals(newmessage))
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (exists) // Connection failed
                        {
                            byte[] buffer2 = Encoding.Default.GetBytes("The name " + newmessage + " has been taken! Connection rejected...");
                            n.Send(buffer2);
                            buffer2 = Encoding.Default.GetBytes("#0"); //Command to disconnect
                            n.Send(buffer2);
                            //reject the connection request with the name, remove the player from list
                            n.Close();

                            playerList.Remove(mainP);

                            //int index = playerList.FindIndex(n => x.Equals(pid));
                            //socketList.RemoveAt(index); // Socket has been removed from the list
                            //ids.RemoveAt(index);

                        }
                        else // Connection established
                        {
                            connected = true;

                            mainP.name = newmessage;
                            newmessage = newmessage + " has joined the game";



                            byte[] buffer2 = Encoding.Default.GetBytes(newmessage);
                            foreach (venPlayer p in playerList)
                            {
                                p.client.Send(buffer2);
                            }
                            buffer2 = Encoding.Default.GetBytes("#1"); //Command to confirm connection

                            n.Send(buffer2);



                            msgList.Invoke(new MethodInvoker(delegate
                            {
                                msgList.Items.Add(newmessage);
                            }));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: Name wasn't received from the client!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Error: Connection process failed!");
                    connected = false;
                }
                // MessageBox.Show("Good! Server"); // For debugging
                ///////////////////////// Connection Established ///////////////////////


                while (connected)
                {
                    try
                    {
                        byte[] buffer = new byte[64];
                        int rec = n.Receive(buffer);
                        if (rec <= 0)
                            throw new SocketException();
                        string newmessage = Encoding.Default.GetString(buffer);
                        newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));

                        //Check for the input from client


                        if (newmessage.Equals("#list")) //we need to display the names of all players
                        {
                            try
                            {
                                buffer = Encoding.Default.GetBytes("Player List:");
                                n.Send(buffer);
                                foreach (venPlayer p in playerList)
                                {
                                    buffer = Encoding.Default.GetBytes(p.name + " " + p.gPoint + "    ");
                                    n.Send(buffer);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                MessageBox.Show("Error sending the player list.");
                            }
                        }
                        else if (newmessage.Equals("#dc"))
                        {
                            buffer = Encoding.Default.GetBytes("#dc");
                            mainP.client.Send(buffer);

                            mainP.client.Close();

                            string leftP = mainP.name; //Name of the player that is removed
                            playerList.Remove(mainP);



                            connected = false;
                            byte[] buffer2 = Encoding.Default.GetBytes(leftP + " has left the game.");
                            foreach (venPlayer p in playerList)
                            {
                                p.client.Send(buffer2);
                            }

                            msgList.Invoke(new MethodInvoker(delegate
                            {
                                msgList.Items.Add(leftP + " has left the game.");
                            }));

                        }
                        else if (newmessage.Length > 4 && newmessage.Substring(0, 4).Equals("#inv")) // A player wants to invate someone
                        {
                            if (mainP.playing)
                                MessageBox.Show("Error: A player who is already playing can not invate.");



                            //Check if the player exits
                            newmessage = newmessage.Substring(5); // Name of the invited player
                            if (newmessage.Equals(mainP.name))
                            {
                                buffer = Encoding.Default.GetBytes("#rejected"); //Invintation request rejected
                                mainP.client.Send(buffer);

                                byte[] buffer2 = Encoding.Default.GetBytes("You can't invite yourself to a game!");
                                mainP.client.Send(buffer2);
                            }
                            else
                            {

                                bool exists = false;


                                int invIndex = -1; // Index of the invited player

                                for (int i = 0; i < playerList.Count; i++)
                                {
                                    if (playerList[i].name.Equals(newmessage))
                                    {
                                        exists = true;
                                        invIndex = i; //Find the index of the player
                                        break;
                                    }
                                }

                                if (!exists) //Player doesn't exist
                                {
                                    mainP.playing = false;
                                    buffer = Encoding.Default.GetBytes("#rejected"); //Invintation request rejected
                                    mainP.client.Send(buffer);

                                    byte[] buffer2 = Encoding.Default.GetBytes("Player " + newmessage + " doesn't exist!");
                                    mainP.client.Send(buffer2);
                                }
                                else // Player exists, check if player is playing
                                {
                                    bool plays = playerList[invIndex].playing;
                                    if (plays) // Player is already playing, reject
                                    {
                                        mainP.playing = false;
                                        buffer = Encoding.Default.GetBytes("#rejected"); //Invintation request rejected
                                        mainP.client.Send(buffer);

                                        byte[] buffer2 = Encoding.Default.GetBytes(newmessage + " is already in a game!");
                                        mainP.client.Send(buffer2);
                                    }
                                    else // Player is free to play
                                    {

                                        mainP.opponent = playerList[invIndex];
                                        mainP.opponent.playing = true;
                                        mainP.opponent.opponent = mainP;
                                        mainP.opponent.roundW = 0;
                                        mainP.roundW = 0;
                                        mainP.playing = true;


                                        buffer = Encoding.Default.GetBytes("Waiting for " + mainP.opponent.name + " to respond...");
                                        mainP.client.Send(buffer);

                                        byte[] buffer2 = Encoding.Default.GetBytes("#req " + mainP.name); // Send the player an invintation
                                        mainP.opponent.client.Send(buffer2);


                                    }

                                }
                            }
                        }
                        else if (newmessage.Length > 7 && newmessage.Substring(0, 7).Equals("#accept"))
                        {
                            try
                            {


                                newmessage = newmessage.Substring(8); // Name of the challenger

                                if (newmessage != mainP.opponent.name) // For debug
                                    MessageBox.Show("Error: Wrong player accepted invate.");



                                buffer = Encoding.Default.GetBytes("#accepted " + mainP.name);
                                mainP.opponent.client.Send(buffer);

                                newmessage = newmessage + " started a game with " + mainP.name + "!";

                                byte[] buffer3 = Encoding.Default.GetBytes(newmessage);
                                foreach (venPlayer p in playerList) //Declare the game between two players
                                {
                                    p.client.Send(buffer3);
                                }

                                msgList.Invoke(new MethodInvoker(delegate
                                {
                                    msgList.Items.Add(newmessage);
                                }));

                                ////////////// Step 3 /////////////////////////////////////////////////////////////////////7
                                Random rnd = new Random();
                                int secretno = rnd.Next(1, 100);
                                mainP.secret = secretno;
                                mainP.opponent.secret = secretno;

                                msgList.Invoke(new MethodInvoker(delegate
                                {
                                    msgList.Items.Add("Secret number: " + secretno);
                                }));

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                MessageBox.Show("Error: Server didn't handle #accept properly");
                            }

                        }
                        else if (newmessage.Length > 8 && newmessage.Substring(0, 8).Equals("#decline"))
                        {
                            try
                            {
                                newmessage = newmessage.Substring(9);
                                if (newmessage != mainP.opponent.name) // For debug
                                    MessageBox.Show("Error: Wrong player declined invate.");

                                byte[] buffer2 = Encoding.Default.GetBytes(mainP.name + " has declined your invitation!");
                                mainP.opponent.client.Send(buffer2);

                                buffer = Encoding.Default.GetBytes("#declined");
                                mainP.client.Send(buffer);
                                mainP.opponent.client.Send(buffer);

                                mainP.opponent.playing = false;
                                mainP.playing = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                MessageBox.Show("Error: Server didn't handle #decline properly");
                            }

                        }
                        else if (newmessage.Length > 7 && newmessage.Substring(0, 4).Equals("#sur"))
                        {
                            newmessage = newmessage.Substring(5);
                            if (!newmessage.Equals(mainP.opponent.name))
                                MessageBox.Show("Error: Names don't match after surrender");


                            buffer = Encoding.Default.GetBytes("#win");
                            mainP.opponent.client.Send(buffer);

                            newmessage = mainP.opponent.name + " has won againts " + mainP.name + "!";
                            mainP.opponent.gPoint++;


                            byte[] buffer2 = Encoding.Default.GetBytes(newmessage);
                            foreach (venPlayer p in playerList)
                            {
                                p.client.Send(buffer2);
                            }


                            msgList.Invoke(new MethodInvoker(delegate
                            {
                                msgList.Items.Add(newmessage);
                            }));

                            mainP.playing = false;
                            mainP.opponent.playing = false;
                            mainP.roundW = 0;
                            mainP.opponent.roundW = 0;

                        }
                        else if (newmessage.Length > 3 && newmessage.Substring(0, 3).Equals("#gs")) //Number guess
                        {
                            mainP.guessed = true;
                            mainP.guessno = Int32.Parse(newmessage.Substring(4)); // Convert str to int
                            if (mainP.opponent.guessed)
                            {
                                mainP.guessed = false;
                                mainP.opponent.guessed = false;

                                int mScore = Math.Abs(mainP.guessno - mainP.secret);
                                int oScore = Math.Abs(mainP.opponent.guessno - mainP.secret);

                                if (mScore > oScore) // Opponent wins
                                {
                                    mainP.opponent.roundW++;


                                    byte[] buffer5 = new byte[64];
                                    buffer5 = Encoding.Default.GetBytes("#lr"); // Lost round
                                    mainP.client.Send(buffer5);
                                    buffer5 = Encoding.Default.GetBytes("Score: You " + mainP.roundW + " - " + mainP.opponent.roundW + " " + mainP.opponent.name);
                                    mainP.client.Send(buffer5);

                                    buffer5 = Encoding.Default.GetBytes("#wr"); // Won round
                                    mainP.opponent.client.Send(buffer5);
                                    buffer5 = Encoding.Default.GetBytes("Score: You " + mainP.opponent.roundW + " - " + mainP.roundW + " " + mainP.name );
                                    mainP.opponent.client.Send(buffer5);
                           
                                }
                                else if (oScore > mScore) // mainP wins
                                {
                                    mainP.roundW++;

                                    byte[] buffer5 = new byte[64];
                                    buffer5 = Encoding.Default.GetBytes("#lr"); // Lost round
                                    mainP.opponent.client.Send(buffer5);
                                    buffer5 = Encoding.Default.GetBytes("Score: You " + mainP.opponent.roundW + " - " + mainP.roundW + " " + mainP.name);
                                    mainP.opponent.client.Send(buffer5);

                                    buffer5 = Encoding.Default.GetBytes("#wr"); // Won round
                                    mainP.client.Send(buffer5);
                                    buffer5 = Encoding.Default.GetBytes("Score: You " + mainP.roundW + " - " + mainP.opponent.roundW + " " + mainP.opponent.name);
                                    mainP.client.Send(buffer5);                                    

                                }
                                else // Draw
                                {
                                    byte[] buffer5 = new byte[64];
                                    buffer5 = Encoding.Default.GetBytes("#wr"); // Won round
                                    mainP.client.Send(buffer5);
                                    mainP.opponent.client.Send(buffer5);
                                    string dmsg = "Draw: Both guesses are equally close! Play again...";
                                    buffer5 = Encoding.Default.GetBytes(dmsg);
                                    mainP.client.Send(buffer5);
                                    mainP.opponent.client.Send(buffer5);
                                }

                                if (mainP.roundW == 2 || mainP.opponent.roundW == 2) // Check if game is over
                                {

                                }
                                else // Pick a new number
                                {
                                    Random rnd = new Random();
                                    int secretno = rnd.Next(1, 100);
                                    mainP.secret = secretno;
                                    mainP.opponent.secret = secretno;

                                    msgList.Invoke(new MethodInvoker(delegate
                                    {
                                        msgList.Items.Add("Secret number: " + secretno);
                                    }));
                                }

                            }
                            else
                            {
                                byte[] buffer5 = new byte[64];
                                buffer5 = Encoding.Default.GetBytes("Waiting for other player to make his/her guess...");
                                mainP.client.Send(buffer5);
                            }

                        }
                        else // Message received
                        {

                            newmessage = mainP.name + ": " + newmessage;
                            buffer = Encoding.Default.GetBytes(newmessage);

                            byte[] buffer2 = Encoding.Default.GetBytes(newmessage);
                            foreach (venPlayer p in playerList)
                            {
                                p.client.Send(buffer2);
                            }
                            msgList.Invoke(new MethodInvoker(delegate
                            {
                                msgList.Items.Add(newmessage);
                            }));
                        }

                    }
                    catch //(Exception ex)
                    {
                        //MessageBox.Show(ex.ToString()); //Only when debugging
                        connected = false;
                    }
                }
                //msgList.Invoke(new MethodInvoker(delegate
                //{
                //msgList.Items.Add("Connection ended");
                //}));
            }
        }






        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (active == true)
                {
                    active = false;
                    if (!playerList.Count.Equals(0))
                    {
                        // Disconnect every player
                        /*int k = playerList.Count;
                        for (int i = 0; i < k; i++)
                        {
                            byte[] buffer = Encoding.Default.GetBytes("#dc");

                            Socket n = socketList[socketList.Count - 1];
                            n.Send(buffer);

                            n.Close();
                            socketList.RemoveAt(socketList.Count - 1); // Last Socket has been removed from the list

                            names.RemoveAt(names.Count - 1);
                        }*/
                        for (int j = 0; j < playerList.Count; j++)
                        {
                            byte[] buffer = Encoding.Default.GetBytes("#dc");
                            Socket n = playerList[playerList.Count - 1].client;
                            n.Send(buffer);
                            n.Close();
                            playerList.RemoveAt(playerList.Count - 1);
                        }
                    }
                }
                server.Close();
                MessageBox.Show("Server has been shutdown.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Error: Closing form function failed.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
