using PortraitsDrawing.ViewModels;

namespace PortraitsDrawing.Views
{
    /// <summary>
    /// Interaction logic for DeviceView.xaml
    /// </summary>
    public partial class DeviceView
    {
        public DeviceView()
        {
            InitializeComponent();
            UseViewModel<DeviceViewModel>();
        }
    }
}
