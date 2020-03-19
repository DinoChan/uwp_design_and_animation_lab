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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    public sealed partial class GlitchText : UserControl
    {
        private Compositor Compositor => Window.Current.Compositor;

        public GlitchText()
        {
            this.InitializeComponent();

            var backgroundWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 100,
                Size = new Vector2(800, 400),
                FontColor = Colors.Red
            };

            var foregroundWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 100,
                Size = new Vector2(800, 400),
                FontColor = Colors.Cyan
            };

            var _imageVisual = Compositor.CreateSpriteVisual();
            _imageVisual.Brush = CreateBrush(backgroundWrapper.Brush, foregroundWrapper.Brush, BlendEffectMode.Lighten);
            _imageVisual.Size = new Vector2(800, 400);

            var containerVisual = Compositor.CreateContainerVisual();
            containerVisual.Children.InsertAtBottom(_imageVisual);


            var lineVisual = Compositor.CreateSpriteVisual();
            lineVisual.Brush = Compositor.CreateColorBrush(Colors.Black);
            lineVisual.Size = new Vector2(800, 1);
            containerVisual.Children.InsertAtTop(lineVisual);

            ElementCompositionPreview.SetElementChildVisual(TextBackground, containerVisual);
            Loaded += (s, e) =>
              {
                  StartOfficeAnimation(backgroundWrapper.Brush, TimeSpan.FromSeconds(0.95), TimeSpan.Zero);
                  StartOfficeAnimation(foregroundWrapper.Brush, TimeSpan.FromSeconds(1.1), TimeSpan.FromSeconds(0.2));
              };
        }

        private (CompositionBrush compositionBrush, CompositionSurfaceBrush compositionSurfaceBrush) CreateBrush(string uri, Color color)
        {
            var compositor = Window.Current.Compositor;
            var loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/Images/sea.jpg"));
            var compositionSurfaceBrush = compositor.CreateSurfaceBrush();
            compositionSurfaceBrush.Surface = loadedSurface;
            var compositionBrush = CreateBrush(compositionSurfaceBrush, compositor.CreateColorBrush(color), BlendEffectMode.Lighten);
            return (compositionBrush, compositionSurfaceBrush);
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

        private void StartOfficeAnimation(CompositionSurfaceBrush brush, TimeSpan duration, TimeSpan delay)
        {
            var offsetAnimation = Compositor.CreateVector2KeyFrameAnimation();
            offsetAnimation.Duration = duration;
            offsetAnimation.DelayTime = delay;
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;

            void addKey(float key, float top, float left)
            {
                offsetAnimation.InsertKeyFrame(key, new Vector2(top, left));
            };
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
