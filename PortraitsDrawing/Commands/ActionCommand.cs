using System;

namespace PortraitsDrawing.Commands
{
    public class ActionCommand : CommandBase
    {
        Action _action;
        public ActionCommand(Action action)
        {
            _action = action;
        }
        public override void Execute(object parameter)
        {
            _action();
        }
    }
}
