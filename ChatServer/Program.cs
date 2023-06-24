using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ChatServer
{
    class Program
    {
        const int portNo = 20000;
        static void Main(string[] args)
        {

            // creates a local ip address for server
            System.Net.IPAddress localAdd =
                System.Net.IPAddress.Parse("127.0.0.1");

            // creates a tcp listener and starts listening
            TcpListener listener = new TcpListener(localAdd, portNo);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                ChatClient user = new ChatClient(client);
            }
        }
    }
}
