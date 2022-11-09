using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using Procesy.Core.ViewModels;

namespace Procesy.WPF.Views
{
    [MvxContentPresentation]
    [MvxViewFor(typeof(DetailViewModel))]
    public partial class DetailView : MvxWpfView
    {
        public DetailView()
        {
            InitializeComponent();
        }
    }
}