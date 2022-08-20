using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Shapes;
using DesignAndAnimationLab.Demos.GlitchArtDemo;
using Microsoft.Graphics.Canvas.Effects;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos
{
    public sealed partial class BlendMixText : UserControl
    {
        public BlendMixText()
        {
            InitializeComponent();
            AddTextToRoot(BlendEffectMode.ColorBurn);
            AddTextToRoot(BlendEffectMode.ColorDodge);
            AddTextToRoot(BlendEffectMode.Darken);
            AddTextToRoot(BlendEffectMode.DarkerColor);
            AddTextToRoot(BlendEffectMode.Difference);
            AddTextToRoot(BlendEffectMode.Division);
            AddTextToRoot(BlendEffectMode.Exclusion);
            AddTextToRoot(BlendEffectMode.Exclusion);
            AddTextToRoot(BlendEffectMode.HardLight);
            AddTextToRoot(BlendEffectMode.HardMix);
            AddTextToRoot(BlendEffectMode.Lighten);
            AddTextToRoot(BlendEffectMode.LighterColor);
            AddTextToRoot(BlendEffectMode.LinearBurn);
            AddTextToRoot(BlendEffectMode.LinearDodge);
            AddTextToRoot(BlendEffectMode.LinearLight);
            AddTextToRoot(BlendEffectMode.Multiply);
            AddTextToRoot(BlendEffectMode.Overlay);
            AddTextToRoot(BlendEffectMode.PinLight);
            AddTextToRoot(BlendEffectMode.Screen);
            AddTextToRoot(BlendEffectMode.SoftLight);
            AddTextToRoot(BlendEffectMode.Subtract);
            AddTextToRoot(BlendEffectMode.VividLight);
        }

        private Compositor Compositor => Window.Current.Compositor;

        public TextToBrushWrapper CreateTextToBrushWrapper(string text, Color fontColor)
        {
            var result = new TextToBrushWrapper
            {
                Text = text,
                FontSize = 45,
                Width = 400,
                Height = 70,
                FontColor = fontColor
            };
            result.Brush.VerticalAlignmentRatio = 0;
            return result;
        }

        private void AddTextToRoot(BlendEffectMode blendEffectMode)
        {
            var redBrushWrapper = CreateTextToBrushWrapper(blendEffectMode.ToString(), Colors.Red);
            var blueBrushWrapper = CreateTextToBrushWrapper(blendEffectMode.ToString(), Colors.Cyan);
            blueBrushWrapper.Brush.Offset = new Vector2(-4f, 0);
            var blueMaskBrush = CreateGradientBrush();

            var textVisual = Compositor.CreateSpriteVisual();
            textVisual.Brush = CreateBrush(blueBrushWrapper.Brush, redBrushWrapper.Brush, blendEffectMode);
            textVisual.Size = new Vector2(400, 70);
            var background = new Rectangle { Height = 70, Width = 400 };

            ElementCompositionPreview.SetElementChildVisual(background, textVisual);
            Root.Children.Add(background);
        }

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

        private CompositionLinearGradientBrush CreateGradientBrush()
        {
            var gradientBrush = Compositor.CreateLinearGradientBrush();
            gradientBrush.StartPoint = new Vector2(0.5f, 0);
            ;
            gradientBrush.EndPoint = new Vector2(0.5f, 1);

            var startGradientStop = Compositor.CreateColorGradientStop();
            startGradientStop.Offset = 0.49f;
            startGradientStop.Color = Colors.White;
            var endGradientStop = Compositor.CreateColorGradientStop();
            endGradientStop.Offset = 0.5f;
            endGradientStop.Color = Colors.Transparent;
            gradientBrush.ColorStops.Add(startGradientStop);
            gradientBrush.ColorStops.Add(endGradientStop);
            return gradientBrush;
        }
    }
}