using PortraitsDrawing.ViewModels;

namespace PortraitsDrawing.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
            UseViewModel<HomeViewModel>();

        }
    }
}
