
namespace PortraitsDrawing.GlobalsData
{
    public interface ISettingsDataProvider
    {
        public int Blur { get; set; }
        public double CannyMin { get; set; }
        public double CannyMax { get; set; }
        public double Aproximation { get; set; }

        public void GetInitalData();
    }
}
