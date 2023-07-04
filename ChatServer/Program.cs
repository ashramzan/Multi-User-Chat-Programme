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
        const int portNo = 40000;
        static void Main(string[] args)
        {
            System.Net.IPAddress localAdd =
            System.Net.IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAdd, portNo);
            listener.Start();
            while (true)
            {
            ChatClient user = new
            ChatClient(listener.AcceptTcpClient());
            }
        }
    }
}
