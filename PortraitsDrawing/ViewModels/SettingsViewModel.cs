using PortraitsDrawing.GlobalsData;
using PortraitsDrawing.ImagePreProcessing.Abstraction;
using PortraitsDrawing.Views.IOC;
using System.Windows.Media;

namespace PortraitsDrawing.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IIsVisibleNotifiable
    {
        private readonly IImageProcessing _imageProcessing;
        private readonly ISettingsDataProvider _settingsDataProvider;

        private int _blur;
        private double _cannyMin;
        private double _cannyMax;
        private double _aproximation;

        public ImageSource ImgPreview { get; set; }

        public int Blur
        {
            get { return _blur; }
            set
            {
                if (value < 0)
                    value = 0;
                if(value % 2 == 0)
                {
                    _blur = ++value;
                    _settingsDataProvider.Blur = (int)Blur;
                    Update();
                }
            }
        }

        public double CannyMin
        {
            get { return _cannyMin; }
            set 
            { 
                _cannyMin = value;
                _settingsDataProvider.CannyMin = CannyMin;
                Update();
            }
        }

        public double CannyMax
        {
            get { return _cannyMax; }
            set 
            {
                _cannyMax = value;
                _settingsDataProvider.CannyMax = CannyMax;
                Update();
            }
        }
        public double Aproximation
        {
            get { return _aproximation; }
            set
            {
                _aproximation = value;
                _settingsDataProvider.Aproximation = Aproximation;
                Update();
            }
        }

        public SettingsViewModel(IImageProcessing imageProcessing, ISettingsDataProvider settingsDataProvider)
        {
            
            _imageProcessing = imageProcessing;
            _settingsDataProvider = settingsDataProvider;
            _settingsDataProvider.GetInitalData();
            Blur = _settingsDataProvider.Blur;
            CannyMin = _settingsDataProvider.CannyMin;
            CannyMax = _settingsDataProvider.CannyMax;
            ImgPreview = _imageProcessing.CreateEdgesImageSource();
        }

        public void OnVisibleChanged(bool visible)
        {
            if (visible)
            {
                ImgPreview = _imageProcessing.CreateEdgesImageSource();

                OnPropertyChanged(nameof(ImgPreview));
            }
                
        }

        private void Update ()
        {
            ImgPreview = _imageProcessing.CreateEdgesImageSource();
            OnPropertyChanged(nameof(ImgPreview));
        }
    }
}
