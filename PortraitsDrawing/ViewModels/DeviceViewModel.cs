using PortraitsDrawing.Camera;
using PortraitsDrawing.GlobalsData.Abstraction;
using System.Collections.Generic;
using System.Windows.Media;

namespace PortraitsDrawing.ViewModels
{
    public class DeviceViewModel : ViewModelBase
    {
        private readonly ICamera _camera;
        private readonly IPortProvider _portProvider;
        private readonly IDeviceDataProvider _deviceDataProvider;

        private List<int> _listOfCameraID;

        public List<int> ListOfCameraID
        {
            get { return _listOfCameraID; }
            set { _listOfCameraID = value; }
        }

        private int _actualCamera;
        private string _robotIP;
        private int _robotPort;

        public int ActualCamera
        {
            get { return _actualCamera; }
            set
            {
                _actualCamera = value;
                _camera.StopRecording();
                _camera.CameraID = ActualCamera;
                _camera.StartRecording();
            }
        }

        public string RobotIP
        {
            get { return _robotIP; }

            set
            {
                _robotIP = value;
                _deviceDataProvider.RobotIPAdress = RobotIP;
            }
        }

        public int RobotPort
        {
            get { return _robotPort; }
            set 
            { 
                _robotPort = value;
                _deviceDataProvider.RobotPort = RobotPort;
            }
        }

        public ImageSource ImgStream { get; set; }
        
        public DeviceViewModel(ICamera camera, IPortProvider portProvider, IDeviceDataProvider deviceDataProvider)
        {
            _camera = camera;
            _portProvider = portProvider;
            _deviceDataProvider = deviceDataProvider;
            
            _portProvider.GetAllConnectedCameras();
            _portProvider.GetActualPort();
            ListOfCameraID = _portProvider.GetAllConnectedCameras();
            _camera.onFrame += Camera_onFrame;

            RobotIP = _deviceDataProvider.RobotIPAdress;
            RobotPort = _deviceDataProvider.RobotPort;

            //ListOfCameraID = GetAllConnectedCameras();
            //ListOfCameraID = new List<int>();
            //ListOfCameraID.Add(0);
            //ListOfCameraID.Add(2);
        }

        private void Camera_onFrame(ImageSource stream)
        {
            stream.Freeze();
            ImgStream = stream;
            OnPropertyChanged(nameof(ImgStream));
        }
    }
}
