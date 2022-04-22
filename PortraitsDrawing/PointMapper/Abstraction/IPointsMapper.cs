using Emgu.CV.Util;

namespace PortraitsDrawing.PointMapper
{
    public interface IPointsMapper
    {
        public VectorOfVectorOfPoint GetPoints();

    }
}
