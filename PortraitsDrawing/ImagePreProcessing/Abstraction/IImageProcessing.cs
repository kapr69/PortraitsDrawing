using Emgu.CV;
using PortraitsDrawing.Camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PortraitsDrawing.ImagePreProcessing.Abstraction
{
    public interface IImageProcessing
    {
        public Mat EdgeImage { get; set; }
        public ImageSource CreateEdgesImageSource();
    }
}
