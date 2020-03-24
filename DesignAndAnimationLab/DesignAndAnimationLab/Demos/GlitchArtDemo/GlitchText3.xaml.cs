using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    public sealed partial class GlitchText3 : UserControl
    {
        private Compositor Compositor => Window.Current.Compositor;

        public GlitchText3()
        {
            this.InitializeComponent();

            var textWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White,
                Background = new SolidColorBrush(Colors.Black)
            };
            textWrapper.Brush.VerticalAlignmentRatio = 0;
            var backgroundWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Color.FromArgb(230, 255, 0, 0)
            };
            backgroundWrapper.Brush.Offset = new Vector2(2f, 0);
            backgroundWrapper.Brush.VerticalAlignmentRatio = 0;

            var redBrush = CreateBrush(backgroundWrapper.Brush, textWrapper.Brush, BlendEffectMode.Screen);

            var textWrapper2 = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White,
                Background = new SolidColorBrush(Colors.Black)
            };
            textWrapper2.Brush.Offset = new Vector2(-5f, 0);
            var foregroundWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Color.FromArgb(204, 0, 255, 255)
            };
            foregroundWrapper.Brush.Offset = new Vector2(-7f, 0);
            foregroundWrapper.Brush.VerticalAlignmentRatio = 0;
            textWrapper2.Brush.VerticalAlignmentRatio = 0;

            var blueBrush = CreateBrush(foregroundWrapper.Brush, textWrapper2.Brush, BlendEffectMode.Screen);

            var _imageVisual = Compositor.CreateSpriteVisual();
            _imageVisual.Brush = CreateBrush(redBrush, blueBrush, BlendEffectMode.Multiply);
            _imageVisual.Size = new Vector2(800, 110);

            var containerVisual = Compositor.CreateContainerVisual();
            containerVisual.Children.InsertAtBottom(_imageVisual);


            var textWrapper3 = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White
            };

            var textVisual = Compositor.CreateSpriteVisual();
            textVisual.Brush = textWrapper3.Brush;
            textVisual.Size = new Vector2(800, 110);
            containerVisual.Children.InsertAtBottom(textVisual);

            var lineVisual = Compositor.CreateSpriteVisual();
            lineVisual.Brush = Compositor.CreateColorBrush(Colors.Black);
            lineVisual.Size = new Vector2(800, 6);
            containerVisual.Children.InsertAtTop(lineVisual);

            ElementCompositionPreview.SetElementChildVisual(TextBackground, containerVisual);
            Loaded += (s, e) =>
            {
                StartHeightAnimation(backgroundWrapper, new List<(double, double)>() { (0, 1), (20, 80), (60, 15), (100, 105) }, TimeSpan.FromSeconds(1), TimeSpan.Zero);
                StartHeightAnimation(textWrapper, new List<(double, double)>() { (0, 1), (20, 80), (60, 15), (100, 105) }, TimeSpan.FromSeconds(1), TimeSpan.Zero);
                StartHeightAnimation(foregroundWrapper, new List<(double, double)>() { (0, 110), (20, 112.5), (35, 30), (50, 100), (60, 50), (70, 85), (80, 55), (100, 1) }, TimeSpan.FromSeconds(1.5), TimeSpan.Zero);
                StartHeightAnimation(textWrapper2, new List<(double, double)>() { (0, 110), (20, 112.5), (35, 30), (50, 100), (60, 50), (70, 85), (80, 55), (100, 1) }, TimeSpan.FromSeconds(1.5), TimeSpan.Zero);
                StartOfficeAnimation(lineVisual, TimeSpan.FromSeconds(3), TimeSpan.Zero);
            };
        }


        private CompositionBrush CreateBrush(CompositionBrush foreground, CompositionBrush background, BlendEffectMode blendEffectMode)
        {
            var compositor = Window.Current.Compositor;
            var effect = new BlendEffect()
            {
                Mode = blendEffectMode,
                Foreground = new CompositionEffectSourceParameter("Main"),
                Background = new CompositionEffectSourceParameter("Tint"),
            };
            var effectFactory = compositor.CreateEffectFactory(effect);
            var compositionBrush = effectFactory.CreateBrush();
            compositionBrush.SetSourceParameter("Main", foreground);
            compositionBrush.SetSourceParameter("Tint", background);

            return compositionBrush;
        }

        private void StartOfficeAnimation(SpriteVisual visual, TimeSpan duration, TimeSpan delay)
        {
            var offsetAnimation = Compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.Duration = duration;
            offsetAnimation.DelayTime = delay;
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            var easing = Compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.2f, 1f));
            void addKey(float key, float top)
            {
                offsetAnimation.InsertKeyFrame(key, new Vector3(0, top, 0), easing);
            };

            addKey(.08f, 95);
            addKey(.14f, 20);
            addKey(.20f, 105);
            addKey(.32f, 5);
            addKey(.99f, 75);
            visual.StartAnimation(nameof(CompositionSurfaceBrush.Offset), offsetAnimation);
        }



        private void StartHeightAnimation(TextToBrushWrapper brush, List<(double, double)> keyFrames, TimeSpan duration, TimeSpan delay)
        {
            var storyboard = new Storyboard();

            var animation = new DoubleAnimationUsingKeyFrames();
            animation.EnableDependentAnimation = true;
            Storyboard.SetTarget(animation, brush);
            Storyboard.SetTargetProperty(animation, nameof(TextToBrushWrapper.Height));

            foreach (var item in keyFrames)
            {
                animation.KeyFrames.Add(new LinearDoubleKeyFrame { KeyTime = duration / 100 * item.Item1, Value = item.Item2 });
            }

            storyboard.Children.Add(animation);
            storyboard.RepeatBehavior = RepeatBehavior.Forever;

            storyboard.BeginTime = delay;
            storyboard.Begin();
        }
    }
}
