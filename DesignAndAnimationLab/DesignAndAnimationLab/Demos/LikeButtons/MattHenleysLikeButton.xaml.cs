using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.LikeButtons
{
    /// <summary>
    /// https://codepen.io/mattbhenley/pen/gQbWgd
    /// </summary>
    public sealed partial class MattHenleysLikeButton : UserControl
    {
        private Storyboard _checkStoryboard;
        private CompositeTransform _heartTransform;

        public MattHenleysLikeButton()
        {
            InitializeComponent();
        }

        private void OnHeartLoaded(object sender, RoutedEventArgs e)
        {
            _heartTransform = (sender as Image).RenderTransform as CompositeTransform;
            _checkStoryboard = new Storyboard();


            var keyFrames = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(keyFrames, _heartTransform);
            Storyboard.SetTargetProperty(keyFrames, nameof(CompositeTransform.TranslateX));
            TimeSpan start = TimeSpan.Zero;
            for (var i = 0; i < 28; i++)
            {
                var keyFrame = new DiscreteDoubleKeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds((i + 1d) / 28d),
                    Value = -(i + 1) * 100
                };
                keyFrames.KeyFrames.Add(keyFrame);
            }

            _checkStoryboard.Children.Add(keyFrames);

            _checkStoryboard.FillBehavior = FillBehavior.HoldEnd;
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            _checkStoryboard.Begin();
        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            _checkStoryboard.Stop();
            _heartTransform.TranslateX = 0;
        }
    }
}
