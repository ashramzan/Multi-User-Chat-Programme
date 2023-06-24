using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        const int portNo = 20000;
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();

            // connect to the server
            tcpClient.Connect("127.0.0.1", portNo);

            NetworkStream ns = tcpClient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("Hello");

            ns.Write(data, 0, data.Length);           
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
