using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PortraitsDrawing.Camera
{
    public class MyCamera : ICamera
    {
        private readonly IPortProvider _portProvider;
        public int CameraID { get; set; }
        public ImageSource ImageSource { get; set; }
        public VideoCapture Cap { get; set; }
        public Mat ActualImage { get; set; }

        public delegate void dCamStream(ImageSource stream);

        public bool IsRunning { get; set; }

        public int MyProperty { get; set; }


        public int X { get; }
        public int Y { get; }

        public bool isClosed = false;

        public MyCamera(IPortProvider portProvider)
        {
            _portProvider = portProvider;
        }

        event ICamera.dCamStream ICamera.onFrame
        {
            add
            {
                localonFrame += value;
            }
            remove
            {
                localonFrame -= value;
            }
        }
        event ICamera.dCamStream localonFrame;


        public event dCamStream onFrame;


        public ImageSource GrabImage()
        {
            try
            {
                Mat video = Cap.QueryFrame();
                if (video != null)
                {
                    ActualImage = video;
                    var bitmap = video.ToBitmap();
                    var imageSource = ImageSourceForBitmap(bitmap);
                    ImageSource = ImageSourceForBitmap(bitmap);
                    localonFrame?.Invoke(ImageSource);
                }
            }
            catch (CvException)
            {

            }
            catch (Exception ex)
            {

            }
            return ImageSource;
        }

        public void StartRecording()
        {
            //IsRunning = true; 
            Task t = new Task(Thread);
            t.Start();

        }

        public void Thread()
        {
            if (Cap != null)
            {
                Cap.Stop();
                Cap.Dispose();
                Cap = null;
            }
            IsRunning = true;
            Cap = new VideoCapture(_portProvider.GetActualPort());
            if (Cap == null)
                return;
            Cap.Start();
            while (IsRunning)
            {
                if ((bool)!Cap?.IsOpened)
                    break;
                try
                {
                    Mat video = Cap.QueryFrame();
                    if (video != null)
                    {
                        var bitmap = video.ToBitmap();
                        ImageSource = ImageSourceForBitmap(bitmap);
                        localonFrame?.Invoke(ImageSource);
                    }
                }
                catch (CvException)
                {

                }
                catch (Exception ex)
                {

                }
            }
            isClosed = true;
        }

        public void StopRecording()
        {
            //Cap.Stop();
            IsRunning = false;
            while (!isClosed)
            {
                System.Threading.Thread.Sleep(20);
            }

        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
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
