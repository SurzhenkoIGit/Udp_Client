using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Client
{


    internal class Program
    {
        static Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        static void Main(string[] args)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse("192.168.2.115");
                IPEndPoint iPEnd = new IPEndPoint(ipAddress, 36072);
                udpClient.Connect(iPEnd);
                if(udpClient.Connected)
                {
                    Console.WriteLine("Вы подключились!");
                }
                else
                {
                    Console.WriteLine(new Exception("Проверьте правильность введенных данных!"));
                }
                udpClient.Send(new byte[] { 1, 1 });
                while (udpClient.Connected)
                {
                    Console.Write("Введите сообщение:");
                    string message = Console.ReadLine();
                    byte[] buf = Encoding.UTF8.GetBytes(message);

                    int bytes = udpClient.Send(buf);

                    Console.WriteLine("Отправлено " + bytes + "байт");
                    
                }
                udpClient.Shutdown(SocketShutdown.Both);
                udpClient.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



        }
    }
}
