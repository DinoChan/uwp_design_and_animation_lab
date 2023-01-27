using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DesignAndAnimationLab.Controls
{
    public class InnerShadowBox : ContentControl
    {


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



    }
}
