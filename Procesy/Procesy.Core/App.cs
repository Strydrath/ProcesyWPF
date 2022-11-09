using MvvmCross;
using MvvmCross.ViewModels;
using Procesy.Core.Services;
using Procesy.Core.ViewModels;

namespace Procesy.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IProcesyService, ProcesyService>();
            Mvx.IoCProvider.RegisterType<IDetailService, DetailService>();
            Mvx.IoCProvider.RegisterType<DetailViewModel, DetailViewModel>();

            RegisterAppStart<ListViewModel>();
        }
    }
}