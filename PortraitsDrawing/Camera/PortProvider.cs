using System.Collections.Generic;
using System.Management;

namespace PortraitsDrawing.Camera
{
    public class PortProvider : IPortProvider
    {
        public int ActualCameraID { get; set; }
        public List<int> GetAllConnectedCameras()
        {
            int i = 0;
            List<int> cameraList = new List<int>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')"))
            {
                foreach (var device in searcher.Get())
                {
                    cameraList.Add(i++);
                }
            }
            return cameraList;
        }

        public int GetActualPort()
        {
            return ActualCameraID;
        }
    }
}
