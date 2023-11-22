using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Input IP address: ");
                IPAddress ipAddress = IPAddress.Parse(Console.ReadLine());

                Console.Write("Input port: ");
                int port = Int32.Parse(Console.ReadLine());

                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                using Socket server = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(ipEndPoint);
                server.Listen();

                Console.WriteLine("Waiting for connection on port {0}", ipEndPoint);

                Socket handler = server.Accept();

                //while (true)
                //{
                    byte[] buffer = new byte[1024];
                    int received = handler.Receive(buffer, SocketFlags.None);
                    string response = Encoding.Unicode.GetString(buffer, 0, received);
                    Console.WriteLine(response);

                    //if (response.IndexOf("<Bye>") > -1)
                    //{
                    //    break;
                    //}

                    Console.Write("Input message: ");
                    string str = "Server: " + Console.ReadLine() + " " + DateTime.Now.ToString();
                    byte[] msg = Encoding.Unicode.GetBytes(str);

                    handler.Send(msg);

                //}
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}