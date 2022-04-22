using Microsoft.Extensions.DependencyInjection;
using PortraitsDrawing.ViewModels.Abstraction;
using PortraitsDrawing.Views.IOC;
using System;
using System.ComponentModel;


namespace PortraitsDrawing.Views
{
    public abstract class IocControl : IsVisibleNotifyControl
    {
        private static IServiceProvider? _serviceProvider;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected void UseViewModel<TViewModel>() where TViewModel : class, IViewModel
        {
#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
#endif
            var viewModel = _serviceProvider?.GetRequiredService<TViewModel>();
            DataContext = viewModel;
        }






    }
}
