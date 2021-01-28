using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Media;
using Microsoft.Toolkit.Uwp.UI.Media.Effects;
using Microsoft.Toolkit.Uwp.UI.Media.Pipelines;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AcrylicAndBlurDemo : Page, INotifyPropertyChanged
    {
        private Brush _customPipelineBrush;

        public AcrylicAndBlurDemo()
        {
            this.InitializeComponent();
            CustomPipelineBrush = PipelineBuilder.FromHostBackdrop()
                                        .LuminanceToAlpha()
                                        .Opacity(0.4f)
                                        .Blend(
                                          PipelineBuilder.FromHostBackdrop(),
                                          BlendEffectMode.Multiply)
                                        .Blur(16)
                                        .Shade("#FF222222".ToColor(), 0.4f)
                                        .Blend(
                                          PipelineBuilder.FromTiles("/Assets/BrushAssets/NoiseTexture.png"),
                                          BlendEffectMode.Overlay,
                                          Placement.Background)
                                        .AsBrush();

            HasBlur = true;
            HasLuminanceToAlpha = true;
            HasOpacity = true;
            HasShade = true;
            EffectOpacity = 0.4;
            BlurAmount = 16;
            ShadeColor = "#FF222222".ToColor();
            ShadeIntensity = 0.4;

        }

        public bool HasLuminanceToAlpha { get; set; }

        public bool HasOpacity { get; set; }

        public double EffectOpacity { get; set; }

        public bool HasBlur { get; set; }

        public double BlurAmount { get; set; }


        public Color ShadeColor { get; set; }

        public double ShadeIntensity { get; set; }

        public bool HasShade { get; set; }

        public Brush CustomPipelineBrush
        {
            get => _customPipelineBrush;
            set
            {
                _customPipelineBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomPipelineBrush)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnAcceptCustomBrush(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var builder = PipelineBuilder.FromHostBackdrop();
            if (HasLuminanceToAlpha)
                builder = builder.LuminanceToAlpha();
            if (HasOpacity)
                builder = builder.Opacity((float)EffectOpacity);

            builder = builder.Blend(PipelineBuilder.FromHostBackdrop(), BlendEffectMode.Multiply);

            if (HasBlur)
                builder = builder.Blur((float)BlurAmount);

            if (HasShade)
                builder = builder.Shade(ShadeColor, (float)ShadeIntensity);

            builder = builder.Blend(PipelineBuilder.FromTiles("/Assets/BrushAssets/NoiseTexture.png"),
                      BlendEffectMode.Overlay,
                      Placement.Background);

            CustomPipelineBrush = builder.AsBrush();
        }
    }
}
