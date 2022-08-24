using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DesignAndAnimationLab.AnimationTimelines;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.Gooey
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GooeyDemo : Page
    {
        private ICanvasBrush _brush1;
        private ICanvasBrush _brush2;
        private ICanvasBrush _brush3;
        private GaussianBlurEffect _blurEffect3;
        private GaussianBlurEffect _blurEffect2;
        private Vector2 _centerPoint3;
        private Vector2 _centerPoint2;
        private Vector2 _centerPoint1;
        private ICanvasImage _image3;
        public GooeyDemo()
        {
            this.InitializeComponent();

        }

        private void OnCanvasSizeChanged3(object sender, SizeChangedEventArgs e)
        {
            _centerPoint3 = Canvas3.ActualSize / 2;
        }

        private void OnDraw3(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            var source = new CanvasCommandList(sender);
            var offset = new Vector2(80, 0);
            using (var drawingSession = source.CreateDrawingSession())
            {
                drawingSession.FillCircle(_centerPoint3 - offset, 80, _brush3);
                drawingSession.FillCircle(_centerPoint3 + offset, 80, _brush3);
            }

            _blurEffect3.Source = source;
            args.DrawingSession.DrawImage(_image3);
        }

        private void OnCreateResource3(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            _brush3 = new CanvasSolidColorBrush(sender, Colors.Black);

            _blurEffect3 = new GaussianBlurEffect { BlurAmount = 16f };

            _image3 = new ColorMatrixEffect
            {
                ColorMatrix = new Matrix5x4
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
                    M54 = -7
                },
                Source = _blurEffect3
            };
        }

        private void OnCreateResource2(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _brush2 = new CanvasSolidColorBrush(sender, Colors.Black);
            _blurEffect2 = new GaussianBlurEffect { BlurAmount = 16f };
        }

        private void OnDraw2(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var source = new CanvasCommandList(sender);
            var offset = new Vector2(80, 0);
            using (var drawingSession = source.CreateDrawingSession())
            {
                drawingSession.FillCircle(_centerPoint2 - offset, 80, _brush2);
                drawingSession.FillCircle(_centerPoint2 + offset, 80, _brush2);
            }

            _blurEffect2.Source = source;
            args.DrawingSession.DrawImage(_blurEffect2);
        }

        private void OnCanvasSizeChanged2(object sender, SizeChangedEventArgs e)
        {
            _centerPoint2 = Canvas3.ActualSize / 2;
        }

        private void OnCanvasSizeChanged1(object sender, SizeChangedEventArgs e)
        {
            _centerPoint1 = Canvas3.ActualSize / 2;
        }

        private void OnDraw1(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var source = new CanvasCommandList(sender);
            var offset = new Vector2(80, 0);
            using (var drawingSession = source.CreateDrawingSession())
            {
                drawingSession.FillCircle(_centerPoint1 - offset, 80, _brush1);
                drawingSession.FillCircle(_centerPoint1 + offset, 80, _brush1);
            }

            args.DrawingSession.DrawImage(source);
        }

        private void OnCreateResource1(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _brush1 = new CanvasSolidColorBrush(sender, Colors.Black);
        }
    }
}
