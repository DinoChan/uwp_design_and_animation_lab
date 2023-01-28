using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace DesignAndAnimationLab.Controls
{
    public class InnerShadowBox : ContentControl
    {
        public InnerShadowBox()
        {
            DefaultStyleKey = typeof(InnerShadowBox);
            this.SizeChanged += OnControlSizeChanged;
        }

        private void OnControlSizeChanged(object sender, SizeChangedEventArgs e) => UpdateDropShadowPanelMask();

        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register("BlurRadius", typeof(double), typeof(InnerShadowBox), new PropertyMetadata(0d));



        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(InnerShadowBox), new PropertyMetadata(Colors.Black));



        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register("OffsetX", typeof(double), typeof(InnerShadowBox), new PropertyMetadata(0d));



        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register("OffsetY", typeof(double), typeof(InnerShadowBox), new PropertyMetadata(0d));



        public double OffsetZ
        {
            get { return (double)GetValue(OffsetZProperty); }
            set { SetValue(OffsetZProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetZ.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetZProperty =
            DependencyProperty.Register("OffsetZ", typeof(double), typeof(InnerShadowBox), new PropertyMetadata(0d));




        public double ShadowOpacity
        {
            get { return (double)GetValue(ShadowOpacityProperty); }
            set { SetValue(ShadowOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowOpacityProperty =
            DependencyProperty.Register("ShadowOpacity", typeof(double), typeof(InnerShadowBox), new PropertyMetadata(1d));

        public DropShadowPanel DropShadowPanel { get; private set; }

        public Path RootElement { get; private set; }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DropShadowPanel = GetTemplateChild("DropShadowPanel") as DropShadowPanel;
            RootElement = GetTemplateChild("RootElement") as Path;
            UpdateDropShadowPanelMask();
            //var compositor = Window.Current.Compositor;
            //var visual = ElementCompositionPreview.GetElementVisual(this);

            //var geometry = compositor.CreateEllipseGeometry();
            //geometry.Center = new System.Numerics.Vector2(192, 525);
            //geometry.Radius = Vector2.Zero;

        }

        private void UpdateDropShadowPanelMask()
        {
            if (ActualWidth == 0 || ActualHeight == 0 || RootElement == null)
                return;

            GeometryCreator geometryCreator = new GeometryCreator();
            geometryCreator.GeometryHeight = this.ActualHeight;
            geometryCreator.GeometryWidth = this.ActualWidth;
            geometryCreator.CornerRadius = this.CornerRadius;


            RectangleGeometry rectangleGeometry = new RectangleGeometry
            {
                Rect = new Windows.Foundation.Rect(-100, -100, this.ActualWidth + 200, this.ActualWidth + 200),
            };

            GeometryGroup geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(geometryCreator.Result);
            geometryGroup.Children.Add(rectangleGeometry);
            RootElement.Data = geometryGroup;
        }
    }
}
