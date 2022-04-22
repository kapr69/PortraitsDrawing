using PortraitsDrawing.Camera;
using PortraitsDrawing.Commands;
using PortraitsDrawing.ImagePreProcessing.Abstraction;
using PortraitsDrawing.Program;
using PortraitsDrawing.Robot;
using PortraitsDrawing.Views.IOC;
using System.Windows.Media;

namespace PortraitsDrawing.ViewModels
{
    public class HomeViewModel : ViewModelBase, IIsVisibleNotifiable
    {
        private readonly IRobot _robot;
        private readonly IImageProcessing _imageProcessing;
        private readonly ICamera _camera;
        private readonly IProgramFactory _programFactory;


        public ActionCommand TakeImageCommand { get; set; }
        public ActionCommand TakePreviewCommand { get; set; }
        public ActionCommand RunRobotCommand { get; set; }

        public ImageSource ImgStream { get; set; }
        public ImageSource ImgActual { get; set; }
        public ImageSource ImgPreview { get; set; }

        public string ProgramTime { get; set; }

        public HomeViewModel(IRobot robot, IImageProcessing imageProcessing, ICamera camera, IProgramFactory programFactory)
        {

            TakeImageCommand = new ActionCommand(TakeImage);
            TakePreviewCommand = new ActionCommand(TakePreview);
            RunRobotCommand = new ActionCommand(RunRobot);
            _robot = robot;
            _imageProcessing = imageProcessing;
            _camera = camera;
            _programFactory = programFactory;

            _camera.StartRecording();
            _camera.onFrame += Camera_onFrame;
        }

        private void RunRobot()
        {
            _robot.DrawPicture();
        }


        private void Camera_onFrame(ImageSource stream)
        {
            stream.Freeze();
            ImgStream = stream;
            OnPropertyChanged(nameof(ImgStream));
        }

        private void TakePreview()
        {
            ImgPreview = _imageProcessing.CreateEdgesImageSource();
            OnPropertyChanged(nameof(ImgPreview));
            _programFactory.CalculateTime();
            ProgramTime = _programFactory.ProgramTime + " min";
            OnPropertyChanged(nameof(ProgramTime));
        }

        private void TakeImage()
        {
            ImgActual = _camera.GrabImage();
            OnPropertyChanged(nameof(ImgActual));
        }

        public void OnVisibleChanged(bool visible)
        {
            if (visible)
                TakePreview();
        }
    }
}
