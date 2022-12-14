using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using Procesy.Core.ViewModels;

namespace Procesy.WPF.Views
{
    [MvxContentPresentation]
    [MvxViewFor(typeof(ListViewModel))]
    public partial class ListView : MvxWpfView
    {
        public ListView()
        {
            InitializeComponent();
        }
        
    }
}
