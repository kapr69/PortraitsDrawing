
using System.Collections.Generic;

namespace PortraitsDrawing.MyObjects
{
    public class Line
    {
        public Line(List<Point> poses, bool isAligned)
        {
            Poses = poses;
            IsAligned = isAligned;
        }
        public List<Point> Poses { get; set; }
        public bool IsAligned { get; set; }
    }
}
