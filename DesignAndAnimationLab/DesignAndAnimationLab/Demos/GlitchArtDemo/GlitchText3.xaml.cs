using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    public sealed partial class GlitchText3 : UserControl
    {
        public GlitchText3()
        {
            this.InitializeComponent();

            Text = "鉄血  鉄血  鉄血";

            var redBrushWrapper = CreateTextToBrushWrapper(3, Color.FromArgb(230, 255, 0, 0));
            var (redBrush, redMaskBrush) = CreateMaskedBrush(redBrushWrapper.Brush);

            var blueBrushWrapper = CreateTextToBrushWrapper(-3, Color.FromArgb(204, 0, 255, 255));
            blueBrushWrapper.Brush.Offset = new Vector2(-7f, 0);
            var (blueBrush, blueMaskBrush) = CreateMaskedBrush(blueBrushWrapper.Brush);

            var containerVisual = Compositor.CreateContainerVisual();
            var foregroundVisual = Compositor.CreateSpriteVisual();

            foregroundVisual.Brush = CreateBrush(blueBrush, redBrush, BlendEffectMode.Multiply);
            foregroundVisual.Size = new Vector2(800, 110);
            containerVisual.Children.InsertAtBottom(foregroundVisual);

            var whiteBrushWrapper = CreateTextToBrushWrapper(0, Colors.White);

            var textVisual = Compositor.CreateSpriteVisual();
            textVisual.Brush = whiteBrushWrapper.Brush;
            textVisual.Size = new Vector2(800, 110);
            containerVisual.Children.InsertAtBottom(textVisual);

            var lineVisual = Compositor.CreateSpriteVisual();
            lineVisual.Brush = Compositor.CreateColorBrush(Colors.Black);
            lineVisual.Size = new Vector2(800, 6);
            containerVisual.Children.InsertAtTop(lineVisual);
            //https://github.com/Microsoft/WindowsCompositionSamples/issues/128
            ElementCompositionPreview.SetElementChildVisual(TextBackground, containerVisual);
            Loaded += async (s, e) =>
            {
                //StartHeightAnimation(redBrushWrapper, new List<(double, double)>() { (0, 1), (20, 80), (60, 15), (100, 105) }, TimeSpan.FromSeconds(1), TimeSpan.Zero);
                StartHeightAnimation(blueBrushWrapper, new List<(double, double)>() { (18, 110), (20, 112.5), (25, 110) }, TimeSpan.FromSeconds(1.5), TimeSpan.Zero);
                StartOffseteAnimation(lineVisual, TimeSpan.FromSeconds(3), TimeSpan.Zero);
                StartScaleAnimation(redMaskBrush, new List<(float, float)>() { (0, 0.01f), (.20f, .73f), (.60f, .14f), (1, .95f) }, TimeSpan.FromSeconds(1), TimeSpan.Zero);
                StartScaleAnimation(blueMaskBrush, new List<(float, float)>() { (0, 1), (.20f, 1), (.35f, .27f), (.50f, .91f), (.60f, .45f), (.70f, .77f), (.80f, .5f), (1, 0) }, TimeSpan.FromSeconds(1.5), TimeSpan.Zero);
                var words = new List<string> { "鉄血  鉄血  鉄血", "熱血  熱血  熱血", "冷血  冷血  冷血" };
                int index = 0;
                while (true)
                {
                    redBrushWrapper.Text = words[index % 3];
                    blueBrushWrapper.Text = words[index % 3];
                    whiteBrushWrapper.Text = words[index % 3];
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    index++;
                }
            };
        }

        public string Text { get; }
        private Compositor Compositor => Window.Current.Compositor;

        public TextToBrushWrapper CreateTextToBrushWrapper(double shadowOffsetX, Color shadowColor)
        {
            var result = new TextToBrushWrapper
            {
                Text = Text,
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White,
                Background = new SolidColorBrush(Colors.Black),
                ShadowBlurAmount = 0,
                ShadowOffsetX = shadowOffsetX,
                ShadowColor = shadowColor,
                FontFamily = new FontFamily("Arial, Helvetica"),
                FontWeight = FontWeights.Thin
            };
            result.Brush.VerticalAlignmentRatio = 0;
            return result;
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

        private (CompositionBrush, CompositionSurfaceBrush) CreateMaskedBrush(CompositionBrush source)
        {
            var compositor = Window.Current.Compositor;
            var effect = new AlphaMaskEffect()
            {
                Source = new CompositionEffectSourceParameter("Source"),
                AlphaMask = new CompositionEffectSourceParameter("Mask"),
            };

            var opacityMaskSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/Images/mask.Png"));
            var opacityBrush = Compositor.CreateSurfaceBrush(opacityMaskSurface);
            opacityBrush.Stretch = CompositionStretch.UniformToFill;

            var effectFactory = compositor.CreateEffectFactory(effect);
            var compositionBrush = effectFactory.CreateBrush();
            compositionBrush.SetSourceParameter("Source", source);
            compositionBrush.SetSourceParameter("Mask", opacityBrush);
            return (compositionBrush, opacityBrush);
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

        private void StartOffseteAnimation(SpriteVisual visual, TimeSpan duration, TimeSpan delay)
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

        private void StartScaleAnimation(CompositionSurfaceBrush brush, List<(float, float)> keyFrames, TimeSpan duration, TimeSpan delay)
        {
            var offsetAnimation = Compositor.CreateVector2KeyFrameAnimation();
            offsetAnimation.Duration = duration;
            offsetAnimation.DelayTime = delay;
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            //var easing = Compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.2f, 1f));

            foreach (var item in keyFrames)
            {
                offsetAnimation.InsertKeyFrame(item.Item1, new Vector2(1, item.Item2 * 2));
            }

            brush.StartAnimation(nameof(CompositionSurfaceBrush.Scale), offsetAnimation);
        }
    }
}