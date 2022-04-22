using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortraitsDrawing.Camera
{
    public interface IPortProvider
    {
        public List<int> GetAllConnectedCameras();
        public int GetActualPort();
    }
}
