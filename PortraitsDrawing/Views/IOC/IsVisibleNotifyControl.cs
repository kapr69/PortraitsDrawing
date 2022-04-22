using System;
using System.Windows.Controls;

namespace PortraitsDrawing.Views.IOC
{
    public class IsVisibleNotifyControl : UserControl
    {
        public IsVisibleNotifyControl()
        {

            IsVisibleChanged += IsVisibleNotifyControl_IsVisibleChanged;
        }

        private void IsVisibleNotifyControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if(DataContext is IIsVisibleNotifiable vm)
            {
                vm.OnVisibleChanged(IsVisible);
            }
        }
    }
}
