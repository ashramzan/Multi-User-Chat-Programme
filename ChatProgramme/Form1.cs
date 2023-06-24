using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        const int portNo = 20000;
        TcpClient client;
        byte[] data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void BtnSignIn_Click (object sender, EventArgs e)
        {
            if (BtnSignIn.Text == "Sign In")
            {
                try
                {
                    // connect to server
                    client = new TcpClient();
                    client.Connect("127.0.0.1", portNo);
                    data = new byte[client.ReceiveBufferSize];

                    // read from server
                    SendMessage(txtNick.Text);
                    client.GetStream().BeginRead(data, 0,
                        System.Convert.ToInt32(client.ReceiveBufferSize), 
                        ReceiveMessage, null);
                    BtnSignIn.Text = "Sign Out";
                    BtnSend.Enabled = true;
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.ToString());
                    client = null;
                }
            }
            else
            {
                // disconnect from server
                Disconnect();
                BtnSignIn.Text = "Sign In";
                BtnSend.Enabled = false;
            }
        }
        private void BtnSend_Click (object sender, EventArgs e)
        {
            SendMessage(txtMessage.Text);
            txtMessage.Clear();
        }
        public void SendMessage (string message)
        {
            try
            {
                // sends a message to the server
                NetworkStream ns = client.GetStream();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // sends the text
                ns.Write(data, 0, data.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                int bytesRead;

                // read data from the server
                bytesRead = client.GetStream().EndRead(ar);
                if (bytesRead < 1)
                {
                    return;
                }
                else
                {
                    // invoke delegate to display the received data
                    object[] para = {System.Text.Encoding.ASCII.
                            GetString(data, 0, bytesRead)};
                    this.Invoke(new delUpdateHistory(UpdateHistory), para);
                }
                // continues reading
                client.GetStream().BeginRead(data, 0,
                    System.Convert.ToInt32(client.ReceiveBufferSize),
                    ReceiveMessage, null);
            }
            catch (Exception ex)
            {
                // ignore the error
            }
        }

        // delegate and subroutine to update the TextBox control
        public delegate void delUpdateHistory(string str);
        public void UpdateHistory (string str)
        {
            txtMessageHistory.AppendText(str);
        }
        public void Disconnect()
        {
            try
            {
                //disconnect from server
                client.GetStream().Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // disconnect client from server upon closing of the form
            Disconnect();
        }
    }
}
