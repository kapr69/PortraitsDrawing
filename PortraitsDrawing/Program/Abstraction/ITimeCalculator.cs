
namespace PortraitsDrawing.Program.Abstraction
{
    public interface ITimeCalculator
    {
        public long RobotCycleTime { get; set; }
        public void CalculateTime();

    }
}
