using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PortraitsDrawing.Camera;
using PortraitsDrawing.GlobalsData;
using PortraitsDrawing.ImagePreProcessing.Abstraction;
using System.Drawing;
using System.Windows.Media;

namespace PortraitsDrawing.ImagePreProcessing
{
    public class ImageProcessing : IImageProcessing
    {
        public Mat InputImage { get; set; }
        public Mat EdgeImage { get; set; }
        private readonly ICamera _camera;
        private readonly ISettingsDataProvider _settingsDataProvider;

        public ImageProcessing(ICamera camera, ISettingsDataProvider settingsDataProvider)
        {
            _camera = camera;
            _settingsDataProvider = settingsDataProvider;
        }

        public ImageSource CreateEdgesImageSource()
        {
            InputImage = _camera.ActualImage;
            Mat temp = new Mat();
            if (InputImage != null)
            {
                EdgeImage = InputImage.ToGray().GaussianBlur(_settingsDataProvider.Blur, 1.5).CannnyFilter(_settingsDataProvider.CannyMin, _settingsDataProvider.CannyMax);
                VectorOfVectorOfPoint points = new VectorOfVectorOfPoint();
                Mat hier = new Mat();
                CvInvoke.FindContours(EdgeImage, points, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                VectorOfVectorOfPoint newPoints = new VectorOfVectorOfPoint();

                
                for (int i = 0; i < points.Size; i++)
                {
                    //var epsilon = 5 * CvInvoke.ArcLength(newPoints[i], false);
                    CvInvoke.ApproxPolyDP(points[i], points[i], _settingsDataProvider.Aproximation, true);
                }
                for (int i = 0; i < points.Size; i++)
                {
                    //double lenght = CvInvoke.ArcLength(points[i], false);

                    if (CvInvoke.ArcLength(points[i], false) >= 2)
                    {
                        newPoints.Push(points[i]);
                    }
                }

                temp = new Mat(new Size(EdgeImage.Cols, EdgeImage.Rows),DepthType.Cv8U, 3);
                temp.SetTo(new MCvScalar(190, 190, 190));
                
                CvInvoke.DrawContours(temp, newPoints, -1, new MCvScalar(0, 0, 0), 2);
                return temp.ImageSourceForBitmap();
            }
            else
            {
                return null;
            }    
            
        }
    }
}
