
namespace PortraitsDrawing.GlobalsData.Abstraction
{
    public interface IDeviceDataProvider
    {
        public int CameraID { get; set; }
        public string RobotIPAdress { get; set; }
        public int RobotPort { get; set; }
        public string GetRobotIPAdress();
        public int GetRobotPort();
    }
}
