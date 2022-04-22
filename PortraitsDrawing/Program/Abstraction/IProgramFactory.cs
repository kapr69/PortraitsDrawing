
namespace PortraitsDrawing.Program
{
    public interface IProgramFactory
    {
        public string ActualProgram { get; set; }
        public string ProgramTime { get; set; }
        public string CreateProgram();
        public void CalculateTime();
    }
}
