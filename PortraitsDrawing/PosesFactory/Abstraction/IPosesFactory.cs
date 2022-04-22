using PortraitsDrawing.MyObjects;
using System.Collections.Generic;

namespace PortraitsDrawing.LineFactory.Abstraction
{
    public interface IPosesFactory
    {
        public List<List<Pose>> CreatePoses();
    }
}
