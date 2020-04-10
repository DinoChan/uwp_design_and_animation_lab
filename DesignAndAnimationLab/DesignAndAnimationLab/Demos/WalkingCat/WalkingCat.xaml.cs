using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos
{
    public sealed partial class WalkingCat : UserControl
    {
        public WalkingCat()
        {
            this.InitializeComponent();
        }

        private void OnCatLoaded(object sender, RoutedEventArgs e)
        {
            var transform = (sender as Image).RenderTransform as CompositeTransform;
            var storyboard = new Storyboard
            {
                FillBehavior = FillBehavior.Stop,
                RepeatBehavior = RepeatBehavior.Forever
            };

            var keyFrames = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(keyFrames, transform);
            Storyboard.SetTargetProperty(keyFrames, nameof(CompositeTransform.TranslateY));
            for (var i = 0; i < 12; i++)
            {
                var keyFrame = new DiscreteDoubleKeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds((i + 1d) / 12d),
                    Value = -(i + 1) * 2391d / 12d
                };
                keyFrames.KeyFrames.Add(keyFrame);
            }

            storyboard.Children.Add(keyFrames);
            storyboard.Begin();
        }
    }
}
