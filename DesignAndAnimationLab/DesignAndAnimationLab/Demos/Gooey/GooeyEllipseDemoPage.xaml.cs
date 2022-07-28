using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using DesignAndAnimationLab.Common;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.Gooey
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GooeyEllipseDemoPage : Page
    {
        private GaussianBlurEffect _effect;
        private ICanvasImage _image;
        private Vector2 _canvasActualSize;
        private DoubleProgresser _leftProgresser;
        private DoubleProgresser _rightProgresser;

        public GooeyEllipseDemoPage()
        {
            this.InitializeComponent();

            var visual = ElementCompositionPreview.GetElementVisual(this);
            var compositor = visual.Compositor;
            var containerVisual = compositor.CreateContainerVisual();

            ElementCompositionPreview.SetElementChildVisual(Root, containerVisual);

            _leftProgresser = new DoubleProgresser(4, true) { EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut } };
            _rightProgresser = new DoubleProgresser(4, true) { EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut } };

        }

        private void OnCreateResource(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
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
            Windows.UI.Xaml.Media.Animation.clo
            var source = new CanvasCommandList(sender);
            using (var ds = source.CreateDrawingSession())
            {
                ds.FillCircle(_canvasActualSize / 2, 100, new CanvasSolidColorBrush(sender, Windows.UI.Colors.Black));
                ds.FillCircle(_canvasActualSize / 2 + new Vector2(100, 0), 60, new CanvasSolidColorBrush(sender, Windows.UI.Colors.Blue));
              
              
            }

            _effect.Source = source;
            args.DrawingSession.DrawImage(_image);
        }

        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _canvasActualSize = Canvas.ActualSize;
        }
    }
}
