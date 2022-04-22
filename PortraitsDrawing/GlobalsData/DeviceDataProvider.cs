using PortraitsDrawing.GlobalsData.Abstraction;

namespace PortraitsDrawing.GlobalsData
{
    public class DeviceDataProvider : IDeviceDataProvider
    {
        public int CameraID { get; set; }
        public string RobotIPAdress { get; set; }
        public int RobotPort { get; set; }

        public DeviceDataProvider()
        {
            RobotIPAdress = "192.168.1.101";
            RobotPort = 30001;
        }
        public string GetRobotIPAdress()
        {
            return RobotIPAdress;
        }
        public int GetRobotPort()
        {
            return RobotPort;
        }
    }
}
