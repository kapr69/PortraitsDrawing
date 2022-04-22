using PortraitsDrawing.GlobalsData.Abstraction;
using PortraitsDrawing.LineFactory.Abstraction;
using PortraitsDrawing.MyObjects;
using PortraitsDrawing.PointMapper;
using PortraitsDrawing.Program.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortraitsDrawing.Program
{
    public class URProgramFactory : IProgramFactory
    {
        //private readonly IDeviceDataProvider _deviceDataProvider;
        //private readonly ITimeCalculator _timeCalculator;
        private readonly IPosesFactory _posesFactory;

        public string ActualProgram { get; set; }
        public string ProgramTime { get; set; }

        public URProgramFactory(IDeviceDataProvider deviceDataProvider, ITimeCalculator timeCalculator, IPosesFactory posesFactory)
        {
            //_deviceDataProvider = deviceDataProvider;
            //_timeCalculator = timeCalculator;
            _posesFactory = posesFactory;
        }

        public void CalculateTime()
        {
            var input = _posesFactory.CreatePoses();
            double deltaS = 0;
            double TotalTime = 0;
            double vMax = 0.05;
            double a = 1.2;
            if(input != null)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    for (int j = 0; j < input[i].Count; j++)
                    {
                        if (j == 0)
                        {
                            if (i != 0)
                            {
                                deltaS = Math.Sqrt(Math.Pow(input[i][j].X - input[i - 1][input[i - 1].Count - 1].X, 2) +
                                Math.Pow(input[i][j].Y - input[i - 1][input[i - 1].Count - 1].Y, 2) +
                                Math.Pow(input[i][j].Z - input[i - 1][input[i - 1].Count - 1].Z, 2));
                            }

                        }
                        else
                        {
                            deltaS = Math.Sqrt(Math.Pow(input[i][j].X - input[i][j - 1].X, 2) +
                                Math.Pow(input[i][j].Y - input[i][j - 1].Y, 2) +
                                Math.Pow(input[i][j].Z - input[i][j - 1].Z, 2));
                        }

                        TotalTime += deltaS/vMax/1000/60;
                    }
                }
                TotalTime = Math.Round(TotalTime, 2);
                ProgramTime = TotalTime.ToString();
            }
        }

        public string CreateProgram()
        {
            var input = _posesFactory.CreatePoses();

            string output = string.Empty;

            //povinna hlavicka
            output += "def trajektorie():\n";

            //definice workobjectu - dodelat
            output += "global Plane_2=p[-0.164925,-0.558607,0.136545,-1.17372,-2.781833,0.379414]";

            output += $"movel(pose_trans(Plane_2, p[0,0,0,2.2628,-2.3495,-0.1884]), a=1.0, v=0.05)";

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Count; j++)
                {
                    string x = Math.Round(input[i][j].X / 1000, 3).ToString().Replace(",", ".");
                    string y = Math.Round(input[i][j].Y / 1000, 3).ToString().Replace(",", ".");
                    string z = Math.Round(input[i][j].Z / 1000, 3).ToString().Replace(",", ".");

                    if (j == 0 || j == input[i].Count - 1)//predelat
                    {
                        output += "movel(pose_trans(Plane_2, p[" + x + "," + y + "," + z + ",2.2628,-2.3495,-0.1884]), a=1.0, v=0.05)";
                    }
                    else
                    {
                        output += "movel(pose_trans(Plane_2, p[" + x + "," + y + "," + "0.002" + ",2.2628,-2.3495,-0.1884]), a=1.0, v=0.05)";
                    }
                }
            }
            output += "popup(\"proveden pohyb 1\", title =\"The Headline in the Blue box\", blocking = True)\n";
            //povinne ukonceni
            output += "end\n";
            ActualProgram = output;
            CalculateTime();
            return output;
        }
    }
}
