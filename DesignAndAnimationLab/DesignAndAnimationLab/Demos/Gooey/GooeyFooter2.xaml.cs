using System;
using System.Collections.Generic;
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
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GooeyFooter2 : Page
    {
        private GaussianBlurEffect _effect;
        private ICanvasImage _image;
        private Vector2 _centerPoint;

        private Vector2Timeline _leftTimeline;
        private Vector2Timeline _rightTimeline;

        private ICanvasBrush _brush;
        private ICanvasBrush _rightBrush;
        private List<GooeyBubble> _bubbles;

        public GooeyFooter2()
        {
            InitializeComponent();
            var easingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut };
            _leftTimeline = new Vector2Timeline(new Vector2(-100, 0), new Vector2(100, 0), 2, null, true, true, easingFunction);
            _rightTimeline = new Vector2Timeline(new Vector2(100, 0), new Vector2(-100, 0), 2, null, true, true, easingFunction);
            _bubbles = new List<GooeyBubble>();
            var unit = 16;
            for (int i = 0; i < 128; i++)
            {
                Random random = new Random();
                var seconds = 2 + random.NextDouble() * 2;
                var delay = TimeSpan.FromSeconds(2 + random.NextDouble() * 2);

                var offsetTimeline = new DoubleTimeline(-(6 + random.NextDouble() * 4) * unit, 10 * unit, seconds, delay, false);
                var sizeTimeline = new DoubleTimeline((2 + random.NextDouble() * 4) * unit, 0, seconds, delay, false);
                var x = random.NextDouble();
                _bubbles.Add(new GooeyBubble { X = x, OffsetTimeline = offsetTimeline, SizeTimeline = sizeTimeline });
            }
        }

        private void OnCreateResource(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            _brush = new CanvasSolidColorBrush(sender, Windows.UI.Color.FromArgb(114, 255, 85, 101));
            //_brush = new CanvasSolidColorBrush(sender, Windows.UI.Colors.Black);
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
                    M44 = 255,
                    M51 = 0,
                    M52 = 0,
                    M53 = 0,
                    M54 = 0,
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
                ds.FillRectangle(-100, _centerPoint.Y, _centerPoint.X * 2 + 200, _centerPoint.Y + 100, _brush);
                //ds.FillCircle(_centerPoint + _leftTimeline.GetCurrentValue(totalTime), 100, _brush);
                //ds.FillCircle(_centerPoint + _rightTimeline.GetCurrentValue(totalTime), 60, _rightBrush);

                foreach (var bubble in _bubbles)
                {
                    var x = bubble.X * _centerPoint.X * 2;
                    var y = _centerPoint.Y - bubble.OffsetTimeline.GetCurrentProgress(totalTime);
                    var size = bubble.SizeTimeline.GetCurrentProgress(totalTime);
                    ds.FillCircle(new Vector2((float)x, (float)y), (float)size, _brush);
                }

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
