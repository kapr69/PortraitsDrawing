using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PortraitsDrawing.ImagePreProcessing
{
    public static class MyExtensions
    {
        public static Mat ToGray(this Mat mat)
        {
            Mat grayImage = new Mat();
            CvInvoke.CvtColor(mat, grayImage, ColorConversion.Bgr2Gray);
            return grayImage;
        }
        public static Mat GaussianBlur(this Mat mat, int kernelSize, double sigma)
        {
            Mat bluredImage = new Mat();
            CvInvoke.GaussianBlur(mat, bluredImage, new System.Drawing.Size(kernelSize, kernelSize), sigma);
            return bluredImage;
        }
        public static Mat CannnyFilter(this Mat mat, double threshold1, double threshold2)
        {
            Mat cannyImage = new Mat();
            CvInvoke.Canny(mat, cannyImage, threshold1, threshold2);
            return cannyImage;
        }
        public static VectorOfVectorOfPoint FindPoints(this Mat mat)
        {
            VectorOfVectorOfPoint points = new VectorOfVectorOfPoint();
            Mat hier = new Mat();
            CvInvoke.FindContours(mat, points, hier, RetrType.List, ChainApproxMethod.ChainApproxSimple);
            return points;
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceForBitmap(this Mat bmp)
        {
            
            var handle = bmp.ToBitmap().GetHbitmap();
            try
            {
                ImageSource newSource = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(handle);
                return newSource;
            }
            catch (Exception)
            {
                DeleteObject(handle);
                return null;
            }
        }
    }
}
