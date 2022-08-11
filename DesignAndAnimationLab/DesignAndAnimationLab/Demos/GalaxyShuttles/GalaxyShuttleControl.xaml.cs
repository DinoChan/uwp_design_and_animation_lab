using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.GalaxyShuttles
{
    public sealed partial class GalaxyShuttleControl : UserControl
    {
        public GalaxyShuttleControl()
        {
            this.InitializeComponent();
            Loaded += GalaxyShettleControl_Loaded;
        }

        public TimeSpan Delay { get; set; }

        private async void GalaxyShettleControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(Delay);
            Move.Begin();
            Fade.Begin();
        }
    }
}