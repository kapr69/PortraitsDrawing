using PortraitsDrawing.Communication;
using PortraitsDrawing.Program;

namespace PortraitsDrawing.Robot
{
    public class UR3 : IRobot
    {
        private readonly ICommunication _communication;
        private readonly IProgramFactory _program;

        public UR3(ICommunication communication, IProgramFactory program)
        {
            _communication = communication;
            _program = program;
        }

        public void DrawPicture()
        {
            _communication.SendData(_program.CreateProgram());
        }
    }
}
