using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Microsoft.Graphics.Canvas.Effects;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    public sealed partial class GlitchText : UserControl
    {
        public GlitchText()
        {
            InitializeComponent();

            var backgroundWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.Red
            };

            var foregroundWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.Cyan
            };

            var imageVisual = Compositor.CreateSpriteVisual();
            imageVisual.Brush = CreateBrush(backgroundWrapper.Brush, foregroundWrapper.Brush, BlendEffectMode.Lighten);
            imageVisual.Size = new Vector2(800, 110);

            var containerVisual = Compositor.CreateContainerVisual();
            containerVisual.Children.InsertAtBottom(imageVisual);

            var lineVisual = Compositor.CreateSpriteVisual();
            lineVisual.Brush = Compositor.CreateColorBrush(Colors.Black);
            lineVisual.Size = new Vector2(800, 2);
            containerVisual.Children.InsertAtTop(lineVisual);

            ElementCompositionPreview.SetElementChildVisual(TextBackground, containerVisual);
            Loaded += (s, e) =>
            {
                StartOffsetAnimation(backgroundWrapper.Brush, TimeSpan.FromSeconds(0.95), TimeSpan.Zero);
                StartOffsetAnimation(foregroundWrapper.Brush, TimeSpan.FromSeconds(1.1), TimeSpan.FromSeconds(0.2));
                StartOffsetAnimation(lineVisual, TimeSpan.FromSeconds(10), TimeSpan.Zero);
            };
        }

        private Compositor Compositor => Window.Current.Compositor;

        private CompositionBrush CreateBrush(CompositionBrush foreground, CompositionBrush background,
            BlendEffectMode blendEffectMode)
        {
            var compositor = Window.Current.Compositor;
            var effect = new BlendEffect
            {
                Mode = blendEffectMode,
                Foreground = new CompositionEffectSourceParameter("Main"),
                Background = new CompositionEffectSourceParameter("Tint")
            };
            var effectFactory = compositor.CreateEffectFactory(effect);
            var compositionBrush = effectFactory.CreateBrush();
            compositionBrush.SetSourceParameter("Main", foreground);
            compositionBrush.SetSourceParameter("Tint", background);

            return compositionBrush;
        }

        private void StartOffsetAnimation(SpriteVisual visual, TimeSpan duration, TimeSpan delay)
        {
            var offsetAnimation = Compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.Duration = duration;
            offsetAnimation.DelayTime = delay;
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            var easing = Compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.2f, 1f));

            void addKey(float key, float top)
            {
                offsetAnimation.InsertKeyFrame(key, new Vector3(0, top, 0), easing);
            }

            ;

            addKey(.9f, 95);
            addKey(.14f, 20);
            addKey(.18f, 105);
            addKey(.22f, 3);
            addKey(.32f, 80);
            addKey(.34f, 30);
            addKey(.4f, 65);
            addKey(.43f, 18);
            addKey(.99f, 75);
            visual.StartAnimation(nameof(CompositionSurfaceBrush.Offset), offsetAnimation);
        }

        private void StartOffsetAnimation(CompositionSurfaceBrush brush, TimeSpan duration, TimeSpan delay)
        {
            var offsetAnimation = Compositor.CreateVector2KeyFrameAnimation();
            offsetAnimation.Duration = duration;
            offsetAnimation.DelayTime = delay;
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;

            void addKey(float key, float top, float left)
            {
                offsetAnimation.InsertKeyFrame(key, new Vector2(top * 2.5f, left * 2.5f));
            }

            ;
            addKey(.1f, -0.4f, -1.1f);
            addKey(.2f, 0.4f, -0.2f);
            addKey(.3f, 0f, .5f);
            addKey(.4f, -0.3f, -0.7f);
            addKey(.5f, 0, .2f);
            addKey(.6f, 1.8f, 1.2f);
            addKey(.7f, -1f, .1f);
            addKey(.8f, -0.4f, -0.9f);
            addKey(.9f, 0, 1.2f);
            addKey(1, 0, -1.2f);
            brush.StartAnimation(nameof(CompositionSurfaceBrush.Offset), offsetAnimation);
        }
    }
}