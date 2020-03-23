using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class GlitchText2 : UserControl
    {
        private Compositor Compositor => Window.Current.Compositor;

        public GlitchText2()
        {
            this.InitializeComponent();

            var redBrushWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White,
                Background = new SolidColorBrush(Colors.Black),
                ShadowBlurAmount = 0,
                ShadowOffsetX = 2,
                ShadowColor = Color.FromArgb(230, 255, 0, 0)
            };
            redBrushWrapper.Brush.VerticalAlignmentRatio = 0;

            var redBrush = redBrushWrapper.Brush;

            var blueBrushWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White,
                Background = new SolidColorBrush(Colors.Black),
                ShadowBlurAmount = 0,
                ShadowOffsetX = -2,
                ShadowColor = Color.FromArgb(204, 0, 255, 255)
            };
            blueBrushWrapper.Brush.Offset = new Vector2(-7f, 0);
            blueBrushWrapper.Brush.VerticalAlignmentRatio = 0;

            var blueBrush = blueBrushWrapper.Brush;


            var containerVisual = Compositor.CreateContainerVisual();

            var foregroundVisual = Compositor.CreateSpriteVisual();
            foregroundVisual.Brush = CreateBrush(redBrush, blueBrush, BlendEffectMode.Multiply);
            foregroundVisual.Size = new Vector2(800, 110);
            containerVisual.Children.InsertAtBottom(foregroundVisual);


            var whiteBrushWrapper = new TextToBrushWrapper
            {
                Text = "TextAnimation",
                FontSize = 90,
                Width = 800,
                Height = 110,
                FontColor = Colors.White
            };

            var textVisual = Compositor.CreateSpriteVisual();
            textVisual.Brush = whiteBrushWrapper.Brush;
            textVisual.Size = new Vector2(800, 110);
            containerVisual.Children.InsertAtBottom(textVisual);

            var lineVisual = Compositor.CreateSpriteVisual();
            lineVisual.Brush = Compositor.CreateColorBrush(Colors.Black);
            lineVisual.Size = new Vector2(800, 6);
            containerVisual.Children.InsertAtTop(lineVisual);

            ElementCompositionPreview.SetElementChildVisual(TextBackground, containerVisual);
            Loaded +=async (s, e) =>
              {
                  StartHeightAnimation(redBrushWrapper, new List<(double, double)>() { (0, 1), (20, 80), (60, 15), (100, 105) }, TimeSpan.FromSeconds(1), TimeSpan.Zero);
                  StartHeightAnimation(blueBrushWrapper, new List<(double, double)>() { (0, 110), (20, 112.5), (35, 30), (50, 100), (60, 50), (70, 85), (80, 55), (100, 1) }, TimeSpan.FromSeconds(1.5), TimeSpan.Zero);
                  StartOfficeAnimation(lineVisual, TimeSpan.FromSeconds(3), TimeSpan.Zero);

                  //var words = new List<string> { "铁血", "热血", "冷血" };
                  //int index = 0;
                  //while (true)
                  //{
                  //    redBrushWrapper.Text = "铁血 热血 冷血";// words[index % 3];
                  //    blueBrushWrapper.Text = "铁血 热血 冷血";// words[index % 3];
                  //    whiteBrushWrapper.Text = "铁血 热血 冷血"; //words[index % 3];
                  //    index++;
                  //    await Task.Delay(TimeSpan.FromSeconds(3));
                  //}
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
