using PortraitsDrawing.ViewModels;

namespace PortraitsDrawing.Views
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView
    {
        public HelpView()
        {
            InitializeComponent();
            UseViewModel<HelpViewModel>();
        }
    }
}
