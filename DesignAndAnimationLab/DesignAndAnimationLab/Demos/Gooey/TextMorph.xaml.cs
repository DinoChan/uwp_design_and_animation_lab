﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DesignAndAnimationLab.AnimationTimelines;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Text;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.Gooey
{
    /// <summary>
    /// https://codepen.io/Valgo/pen/PowZaNY
    /// </summary>
    public sealed partial class TextMorph : Page
    {
        private Vector2 _centerPoint;
        private ColorMatrixEffect _colorMatrixEffect;
        private GaussianBlurEffect _effect;
        private List<MorphItem> _morphItems;

        private CanvasTextFormat _textFormat;

        private string[] _texts = new string[]{
            "Why",
            "is",
            "this",
            "so",
            "satisfying",
            "to",
            "watch?"
       };
        public TextMorph()
        {
            this.InitializeComponent();
            int i = 0;
            var easingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut };
            _morphItems = _texts.Select(t =>
            {
                return new MorphItem { Text = t, Timeline = new DoubleTimeline(0, 1, 1, TimeSpan.FromSeconds(2 + i++ * 1.7), true, false, easingFunction) };
            }).Reverse().ToList();

            _textFormat = new CanvasTextFormat()
            {
                FontSize = 100f,
                Direction = CanvasTextDirection.LeftToRightThenTopToBottom,
                VerticalAlignment = CanvasVerticalAlignment.Center,
                HorizontalAlignment = CanvasHorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                FontFamily = FontFamily.Source,
            };
        }

        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _centerPoint = Canvas.ActualSize / 2;
        }

        private void OnCreateResource(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            var effect1 = new GaussianBlurEffect()
            {
                BlurAmount = 0f,
            };

            _effect = effect1;
            var effect3 = new ColorMatrixEffect();
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

            _colorMatrixEffect = effect2;
        }

        private void OnDraw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            var source = new CanvasCommandList(sender);

            var totalTime = TimeSpan.FromSeconds(args.Timing.TotalTime.TotalSeconds % 15);
            double maxProgress = 0;
            using (var ds = source.CreateDrawingSession())
            {
                foreach (var item in _morphItems)
                {
                    var progress = item.Timeline.GetCurrentProgress(totalTime);
                    maxProgress = Math.Max(maxProgress, progress);
                    ds.DrawText(item.Text, _centerPoint, new CanvasSolidColorBrush(sender, Colors.Black)
                    {
                        Opacity = Convert.ToSingle(progress)
                    }, _textFormat);
                }
            }

            _effect.BlurAmount = Convert.ToSingle(20 * (1 - maxProgress));
            _effect.Source = source;
            //_colorMatrixEffect.ColorMatrix = new Matrix5x4()
            //{
            //    M11 = 1,
            //    M12 = 0,
            //    M13 = 0,
            //    M14 = 0,
            //    M21 = 0,
            //    M22 = 1,
            //    M23 = 0,
            //    M24 = 0,
            //    M31 = 0,
            //    M32 = 0,
            //    M33 = 1,
            //    M34 = 0,
            //    M41 = 0,
            //    M42 = 0,
            //    M43 = 0,
            //    M44 = Convert.ToSingle(18 - maxProgress * 17),
            //    M51 = 0,
            //    M52 = 0,
            //    M53 = 0,
            //    M54 = Convert.ToSingle(-7 + 7 * maxProgress),
            //};
            args.DrawingSession.DrawImage(_colorMatrixEffect);
        }

        private class MorphItem
        {
            public string Text { get; set; }

            public DoubleTimeline Timeline { get; set; }
        }
    }
}
