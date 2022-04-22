using Emgu.CV;
using System.Windows.Media;

namespace PortraitsDrawing.Camera
{
    public interface ICamera
    {
        public int CameraID { get; set; }
        public ImageSource ImageSource { get; set; }
        public delegate void dCamStream(ImageSource stream);
        public event dCamStream onFrame;
        public Mat ActualImage { get; set; }
        public int X { get; }
        public int Y { get; }
        public void StartRecording();
        public void StopRecording();
        public ImageSource GrabImage();
    }
}
