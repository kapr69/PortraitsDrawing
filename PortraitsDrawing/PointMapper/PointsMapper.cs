using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using PortraitsDrawing.GlobalsData;
using PortraitsDrawing.ImagePreProcessing.Abstraction;
using System.Linq;

namespace PortraitsDrawing.PointMapper
{
    public class PointsMapper : IPointsMapper
    {
        private readonly IImageProcessing _imageProcessing;
        private readonly ISettingsDataProvider _settingsDataProvider;

        public PointsMapper(IImageProcessing imageProcessing, ISettingsDataProvider settingsDataProvider)
        {
            _imageProcessing = imageProcessing;
            _settingsDataProvider = settingsDataProvider;
        }

        public VectorOfVectorOfPoint GetPoints()
        {
            var input = _imageProcessing.CreateEdgesImageSource();
            VectorOfVectorOfPoint points = new VectorOfVectorOfPoint();
            Mat hier = new Mat();
            if(input != null)
            {
                CvInvoke.FindContours(_imageProcessing.EdgeImage, points, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                VectorOfVectorOfPoint usedpoints = new VectorOfVectorOfPoint();

                for (int i = 0; i < points.Size; i++)
                {
                    if (points[i].Size > 2)
                        usedpoints.Push(points[i]);
                }

                for (int i = 0; i < usedpoints.Size; i++)
                {
                    //var epsilon = _settingsDataProvider.Aproximation * CvInvoke.ArcLength(usedpoints[i], false);
                    CvInvoke.ApproxPolyDP(usedpoints[i], usedpoints[i], _settingsDataProvider.Aproximation, false);
                }

                System.Drawing.Point[][] sortedPoints = usedpoints.ToArrayOfArray();
                VectorOfVectorOfPoint finalPoints = new VectorOfVectorOfPoint();
                if(sortedPoints.Length > 0)
                    finalPoints.Push(new VectorOfPoint(sortedPoints[0]));

                int minIndex = 0;
                int distance = 0;
                while(sortedPoints.Length > 0)
                {
                    for (int i = 1; i < sortedPoints.Length; i++)
                    {
                        int newDistance = 
                            (sortedPoints[0][sortedPoints[0].Length - 1].X - sortedPoints[i][0].X) * (sortedPoints[0][sortedPoints[0].Length - 1].X - sortedPoints[i][0].X)
                            + (sortedPoints[0][sortedPoints[0].Length - 1].Y - sortedPoints[i][0].Y) * (sortedPoints[0][sortedPoints[0].Length - 1].Y - sortedPoints[i][0].Y);
                        if (i == 1)
                        {
                            distance = newDistance;
                            minIndex = 1;
                        }

                        if (newDistance < distance)
                        {
                            minIndex = i;
                            distance = newDistance;
                        }
                    }
                    if(sortedPoints.Length > 1)
                    {
                        VectorOfPoint temp = new VectorOfPoint(sortedPoints[minIndex]);
                        finalPoints.Push(temp);
                        sortedPoints = sortedPoints.Where((source, index) => index != minIndex).ToArray();
                    }
                    else
                    {
                        return finalPoints;
                    }
                    
                }
                //sortedPoints = sortedPoints.Except(new System.Drawing.Point[0][]).ToArray();
                //sortedPoints = sortedPoints.Where((source, index) => index != 0).ToArray();

                //MessageBox.Show(usedpoints[0][0].ToString());

                return finalPoints;
            }
            else
            {
                return null;
            }            
        }       
    }
}
