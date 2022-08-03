using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using DesignAndAnimationLab.AnimationTimelines;
using DesignAndAnimationLab.Common;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.Gooey
{
    /// <summary>
    /// https://codepen.io/Valgo/pen/PowZaNY
    /// </summary>
    public sealed partial class TextMorph : Page
    {
        private GaussianBlurEffect _effect;
        private ICanvasImage _image;
        private Vector2 _centerPoint;

        private string[] _texts = new string[]{
            "Why",
            "is",
            "this",
            "so",
            "satisfying",
            "to",
            "watch?"
       };

        private List<MorphItem> _morphItems;

        private ICanvasBrush _textBrush;

        private CanvasTextFormat _textFormat;

        public TextMorph()
        {
            this.InitializeComponent();
            int i = 0;
            var easingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut };
            _morphItems = _texts.Select(t =>
            {
                return new MorphItem { Text = t, Timeline = new DoubleTimeline(0, 1,2, TimeSpan.FromSeconds(0 + i++ * 2), true, false, easingFunction) };
            }).Reverse().ToList();

            _textFormat = new CanvasTextFormat()
            {
                FontSize = 100f,
                Direction = CanvasTextDirection.LeftToRightThenTopToBottom,
                VerticalAlignment = CanvasVerticalAlignment.Center,
                HorizontalAlignment = CanvasHorizontalAlignment.Center,
                FontWeight = FontWeight,
                FontFamily = FontFamily.Source,
            };
        }

        private class MorphItem
        {
            public string Text { get; set; }

            public DoubleTimeline Timeline { get; set; }
        }
        private void OnCreateResource(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            _textBrush = new CanvasSolidColorBrush(sender, Windows.UI.Colors.Black);

            var effect1 = new GaussianBlurEffect()
            {
                BlurAmount = 0f,
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

            var totalTime = TimeSpan.FromSeconds(args.Timing.TotalTime.TotalSeconds % 20);
            double progress = 0;
            using (var ds = source.CreateDrawingSession())
            {
                foreach (var item in _morphItems)
                {
                    progress = item.Timeline.GetCurrentProgress(totalTime);
                    //if (progress > 0.4)
                    //{
                        ds.DrawText(item.Text, _centerPoint, new CanvasSolidColorBrush(sender, Windows.UI.Colors.Black)
                        {
                            Opacity = Convert.ToSingle(progress)
                        }, _textFormat);

                    //    break;
                    //}
                }
                //ds.FillCircle(_centerPoint + _leftTimeline.GetCurrentValue(totalTime), 100, _leftBrush);
                //ds.FillCircle(_centerPoint + _rightTimeline.GetCurrentValue(totalTime), 60, _rightBrush);
            }
            //_effect.BlurAmount = Convert.ToSingle(20 * (1 - progress));
            _effect.Source = source;
            args.DrawingSession.DrawImage(_image);
        }

        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _centerPoint = Canvas.ActualSize / 2;
        }
    }
}
