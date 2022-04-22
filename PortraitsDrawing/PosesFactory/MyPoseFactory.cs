using PortraitsDrawing.LineFactory.Abstraction;
using PortraitsDrawing.MyObjects;
using PortraitsDrawing.PointMapper;
using System.Collections.Generic;

namespace PortraitsDrawing.LineFactory
{
    public class MyPoseFactory : IPosesFactory
    {
        private readonly IPointsMapper _pointsMapper;
        public Format Format { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }

        public MyPoseFactory(IPointsMapper pointsMapper)
        {
            _pointsMapper = pointsMapper;
            //bude potreba device provider
            Format = new Format();
            Format.X = 200;
            Format.Y = 137;
        }

        private void GetScale()
        {
            var input = _pointsMapper.GetPoints(); 
            double maxX = 0;
            double maxY = 0;
            if(input != null)
            {
                for (int i = 0; i < input.Size; i++)
                {
                    for (int j = 0; j < input[i].Size; j++)
                    {
                        if (maxX < input[i][j].X)
                        {
                            maxX = input[i][j].X;
                        }

                        if (maxY < input[i][j].Y)
                        {
                            maxY = input[i][j].Y;
                        }
                    }
                }
                ScaleX = Format.X / maxX;
                ScaleY = Format.Y / maxY;
            }   
        }

        public List<List<Pose>> CreatePoses()
        {
            var input = _pointsMapper.GetPoints();
            GetScale();
            List<List<Pose>> poses = new List<List<Pose>>();
            if(input != null)
            {
                for (int i = 0; i < input.Size; i++)
                {
                    List<Pose> localTrajectory = new List<Pose>();
                    for (int j = 0; j < input[i].Size; j++)
                    {
                        if (j == 0 || j == input[i].Size - 1)
                        {
                            if (j == 0)
                            {
                                localTrajectory.Add(new Pose(input[i][j].X * ScaleX, input[i][j].Y * ScaleY, -10));
                                localTrajectory.Add(new Pose(input[i][j].X * ScaleX, input[i][j].Y * ScaleY, 2));
                            }
                            if (j == input[i].Size - 1)
                            {
                                localTrajectory.Add(new Pose(input[i][j].X * ScaleX, input[i][j].Y * ScaleY, 2));
                                localTrajectory.Add(new Pose(input[i][j].X * ScaleX, input[i][j].Y * ScaleY, -10));
                            }
                        }
                        else
                        {
                            localTrajectory.Add(new Pose(input[i][j].X * ScaleX, input[i][j].Y * ScaleY, 2));
                        }
                    }
                    poses.Add(localTrajectory);
                }
                return poses;
            }
            else
            {
                return null;
            }
            
        }
    }
}
