
namespace PortraitsDrawing.GlobalsData
{
    public class SettingsDataProvider : ISettingsDataProvider
    {
        public int Blur { get; set; }
        public double CannyMin { get; set; }
        public double CannyMax { get; set; }
        public double Aproximation { get; set; }

        public void GetInitalData()
        {
            Blur = 3;
            CannyMax = 80;
            CannyMin = 50;
            Aproximation = 5;
        }
    }
}
