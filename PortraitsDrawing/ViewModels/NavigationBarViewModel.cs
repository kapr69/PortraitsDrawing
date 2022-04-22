using PortraitsDrawing.Commands;
using System.Windows;

namespace PortraitsDrawing.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {

        public ActionCommand CloseCommand { get; set; }
        public ActionCommand MinimalizeCommand { get; set; }

        public NavigationBarViewModel()
        {
            CloseCommand = new ActionCommand(CloseWindow);
            MinimalizeCommand = new ActionCommand(MinimalizeWindow);
        }

        private void MinimalizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }                
    }
}
