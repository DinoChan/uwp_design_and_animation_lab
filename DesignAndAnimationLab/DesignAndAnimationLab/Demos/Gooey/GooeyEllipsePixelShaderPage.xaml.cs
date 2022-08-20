using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DesignAndAnimationLab.AnimationTimelines;
using DesignAndAnimationLab.Common;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.Gooey
{
    /// <summary>
    ///     https://codepen.io/Chokcoco/pen/QqWBqV
    /// </summary>
    public sealed partial class GooeyEllipsePixelShaderPage : Page
    {
        private readonly Vector2Timeline _leftTimeline;
        private readonly Vector2Timeline _rightTimeline;
        private GaussianBlurEffect _blurEffect;
        private Vector2 _centerPoint;
        private ICanvasImage _image;
        private ICanvasBrush _leftBrush;
        private ICanvasBrush _rightBrush;

        public GooeyEllipsePixelShaderPage()
        {
            InitializeComponent();
            var easingFunction = new ExponentialEase { EasingMode = EasingMode.EaseInOut };
            _leftTimeline = new Vector2Timeline(new Vector2(-100, 0), new Vector2(100, 0), 2, null, true, true, easingFunction);
            _rightTimeline = new Vector2Timeline(new Vector2(100, 0), new Vector2(-100, 0), 2, null, true, true, easingFunction);
        }


        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) => _centerPoint = Canvas.ActualSize / 2;

        private async void OnCreateResource(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _leftBrush = new CanvasSolidColorBrush(sender, Colors.MediumTurquoise);
            _rightBrush = new CanvasSolidColorBrush(sender, Colors.Purple);
            _blurEffect = new GaussianBlurEffect { BlurAmount = 20f };

            var pixelShaderEffect = new PixelShaderEffect(await Utils.ReadAllBytes("Shaders/SmoothOpacityThreshold.bin")) { Source1 = _blurEffect, Source1BorderMode = EffectBorderMode.Hard, Source1Mapping = SamplerCoordinateMapping.Offset, MaxSamplerOffset = (int)Math.Ceiling(sender.Dpi / 96) };

            pixelShaderEffect.Properties["UpperThresh"] = .52F;
            pixelShaderEffect.Properties["LowerThresh"] = .50F;
            
            _image = pixelShaderEffect;
        }

        private void OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            if(_image==null)
                return;
            
            var source = new CanvasCommandList(sender);
            var totalTime = args.Timing.TotalTime;
            using (var drawingSession = source.CreateDrawingSession())
            {
                drawingSession.FillCircle(_centerPoint + _leftTimeline.GetCurrentValue(totalTime), 100, _leftBrush);
                drawingSession.FillCircle(_centerPoint + _rightTimeline.GetCurrentValue(totalTime), 60, _rightBrush);
            }

            _blurEffect.Source = source;
            args.DrawingSession.DrawImage(_image);
        }
    }
}