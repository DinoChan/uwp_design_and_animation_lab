using System.Numerics;
using DesignAndAnimationLab.AnimationTimelines;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.Gooey
{
    /// <summary>
    /// https://codepen.io/Chokcoco/pen/QqWBqV
    /// </summary>
    public sealed partial class GooeyEllipseDemoPage : Page
    {
        private GaussianBlurEffect _effect;
        private ICanvasImage _image;
        private Vector2 _centerPoint;

        private Vector2Timeline _leftTimeline;
        private Vector2Timeline _rightTimeline;

        private ICanvasBrush _leftBrush;
        private ICanvasBrush _rightBrush;

        public GooeyEllipseDemoPage()
        {
            InitializeComponent();
            var easingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut };
            _leftTimeline = new Vector2Timeline(new Vector2(-100, 0), new Vector2(100, 0), 2,null, true, true, easingFunction);
            _rightTimeline = new Vector2Timeline(new Vector2(100, 0), new Vector2(-100, 0), 2,null, true, true, easingFunction);
        }

        private void OnCreateResource(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            _leftBrush = new CanvasSolidColorBrush(sender, Windows.UI.Colors.Black); 
            _rightBrush = new CanvasSolidColorBrush(sender, Windows.UI.Colors.Blue); 
            var effect1 = new GaussianBlurEffect()
            {
                BlurAmount = 20f,
            };

            _effect = effect1;

            var effect2 = new ColorMatrixEffect()
            {
                ColorMatrix = new Matrix5x4()
                {
                    M11 = 1,
                    M12 = 0,
                    M13 = 0,
                    M14 = 0,
                    M21 = 0,
                    M22 = 1,
                    M23 = 0,
                    M24 = 0,
                    M31 = 0,
                    M32 = 0,
                    M33 = 1,
                    M34 = 0,
                    M41 = 0,
                    M42 = 0,
                    M43 = 0,
                    M44 = 18,
                    M51 = 0,
                    M52 = 0,
                    M53 = 0,
                    M54 = -7,
                },
                Source = effect1
            };

            _image = effect2;
        }

        private void OnDraw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            var source = new CanvasCommandList(sender);
            var totalTime = args.Timing.TotalTime;
            using (var ds = source.CreateDrawingSession())
            {
                ds.FillCircle(_centerPoint + _leftTimeline.GetCurrentValue(totalTime), 100, _leftBrush);
                ds.FillCircle(_centerPoint + _rightTimeline.GetCurrentValue(totalTime), 60, _rightBrush);
            }

            _effect.Source = source;
            args.DrawingSession.DrawImage(_image);
        }

        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _centerPoint = Canvas.ActualSize / 2;
        }
    }
}
