using PortraitsDrawing.GlobalsData.Abstraction;
using PortraitsDrawing.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortraitsDrawing.Communication
{
    public class SocketCommunication : ICommunication
    {
        private readonly IDeviceDataProvider _deviceDataProvider;
        public SocketCommunication(IDeviceDataProvider deviceDataProvider)
        {
            _deviceDataProvider = deviceDataProvider; 
        }
        public void SendData(string message)
        {
            try
            {
                Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(_deviceDataProvider.GetRobotIPAdress(), _deviceDataProvider.GetRobotPort());
                if (socket.Connected)
                    if (message != null)
                    {
                        socket.Send(Encoding.UTF8.GetBytes(message), message.Length, SocketFlags.None);
                    }
                //bacha tady poznamka z minula

                socket.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
