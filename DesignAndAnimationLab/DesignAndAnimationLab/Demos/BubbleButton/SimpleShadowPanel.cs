using System.Numerics;
using DesignAndAnimationLab.Common;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace DesignAndAnimationLab.Demos.BubbleButton
{
    public class SimpleShadowPanel : ContentControl
    {
        private Compositor _Compositor;

        private DropShadow _Shadow;

        private SpriteVisual _ShadowVisual;

        private Visual HostVisual;

        private Rectangle ShadowHost;

        public SimpleShadowPanel()
        {
            this.DefaultStyleKey = typeof(SimpleShadowPanel);
            RegisterPropertyChangedCallback(ContentProperty, ContentPropertyChanged);
            RegisterPropertyChangedCallback(BackgroundProperty, BackgroundPropertyChanged);
            this.SizeChanged += SimpleShadowPanel_SizeChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ShadowHost = GetTemplateChild("ShadowHost") as Rectangle;

            SetupComposition();
            UpdateShadow();
        }

        private void BackgroundPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            UpdateShadow();
        }

        private void ContentPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            UpdateShadow();
        }

        private Color GetBackgroundColor()
        {
            if (Background is SolidColorBrush brush)
            {
                return brush.Color;
            }
            return Colors.Transparent;
        }

        private void SetupComposition()
        {
            if (DesignMode.DesignModeEnabled) return;
            HostVisual = ElementCompositionPreview.GetElementVisual(ShadowHost);
            _Compositor = HostVisual.Compositor;

            _ShadowVisual = _Compositor.CreateSpriteVisual();
            _ShadowVisual.BindSize(HostVisual);

            _Shadow = _Compositor.CreateDropShadow();
            _Shadow.Offset = Vector3.Zero;
            _Shadow.BlurRadius = 25f;

            _ShadowVisual.Shadow = _Shadow;

            ElementCompositionPreview.SetElementChildVisual(ShadowHost, _ShadowVisual);
        }

        private void SimpleShadowPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateShadow();
        }

        private void UpdateShadow()
        {
            if (_Shadow != null)
            {
                _Shadow.Color = GetBackgroundColor();
                if (Content is Rectangle rect)
                {
                    _Shadow.Mask = rect.GetAlphaMask();
                }
            }
        }
    }
}