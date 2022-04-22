using Microsoft.Extensions.DependencyInjection;
using PortraitsDrawing.Camera;
using PortraitsDrawing.Communication;
using PortraitsDrawing.GlobalsData;
using PortraitsDrawing.GlobalsData.Abstraction;
using PortraitsDrawing.ImagePreProcessing;
using PortraitsDrawing.ImagePreProcessing.Abstraction;
using PortraitsDrawing.LineFactory;
using PortraitsDrawing.LineFactory.Abstraction;
using PortraitsDrawing.PointMapper;
using PortraitsDrawing.Program;
using PortraitsDrawing.Program.Abstraction;
using PortraitsDrawing.Robot;
using PortraitsDrawing.ViewModels;

namespace PortraitsDrawing.Services
{
    public static class ServiceBuilder
    {
        public static IServiceCollection CreateConfiguration(this IServiceCollection services)
        {
            services.RegisterViewModels();
            services.RegistreLogic();
            return services;
        }

        public static IServiceCollection RegistreLogic(this IServiceCollection services)
        {
            services.AddSingleton<IRobot, UR3>();
            services.AddSingleton<ICommunication, SocketCommunication>();
            services.AddSingleton<IProgramFactory, URProgramFactory>();
            services.AddSingleton<IPortProvider, PortProvider>();
            services.AddSingleton<ISettingsDataProvider, SettingsDataProvider>();
            services.AddSingleton<IDeviceDataProvider, DeviceDataProvider>();
            services.AddSingleton<ITimeCalculator, TimeCalculator>();
            services.AddSingleton<IPosesFactory, MyPoseFactory>();
            services.AddSingleton<IPointsMapper, PointsMapper>();
            services.AddSingleton<IImageProcessing, ImageProcessing>();
            services.AddSingleton<ICamera, MyCamera>();

            return services;
        }
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<DeviceViewModel>();
            services.AddSingleton<HelpViewModel>();

            return services;
        }
    }
}
