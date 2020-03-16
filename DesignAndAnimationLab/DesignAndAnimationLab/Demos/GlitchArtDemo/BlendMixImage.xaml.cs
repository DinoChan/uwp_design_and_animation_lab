﻿using Microsoft.Graphics.Canvas.Effects;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    public sealed partial class BlendMixImage : UserControl
    {
        public BlendMixImage()
        {
            this.InitializeComponent();

            var _compositor = Window.Current.Compositor;

            var _imageBrush = _compositor.CreateSurfaceBrush();
            var _imageBrush2= _compositor.CreateSurfaceBrush();
            // The loadedSurface has a size of 0x0 till the image has been been downloaded, decoded and loaded to the surface. We can assign the surface to the CompositionSurfaceBrush and it will show up once the image is loaded to the surface.
            var _loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/Images/sea.jpg"));
            _imageBrush.Surface = _loadedSurface;
            var _loadedSurface2 = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/Images/sea.jpg"));
            _imageBrush2.Surface = _loadedSurface;
            _imageBrush2.Offset = new Vector2(10, 0);


            var foregroundColorEffect = new BlendEffect()
            {
                Mode = BlendEffectMode.LighterColor,
                Foreground = new CompositionEffectSourceParameter("Main"),
                Background = new CompositionEffectSourceParameter("Tint"),
            }; 
             var effectFactory = _compositor.CreateEffectFactory(foregroundColorEffect);
            var foreground = effectFactory.CreateBrush();
            foreground.SetSourceParameter("Main", _imageBrush);
            foreground.SetSourceParameter("Tint", _compositor.CreateColorBrush(Colors.Cyan));
      
            var graphicsEffect = new BlendEffect()
            {
                Mode = BlendEffectMode.Screen,
                Foreground = new CompositionEffectSourceParameter("Main"),
                Background = new CompositionEffectSourceParameter("Tint"),
            };

             effectFactory = _compositor.CreateEffectFactory(graphicsEffect);
            var brush = effectFactory.CreateBrush();
            brush.SetSourceParameter("Main", foreground);
            //brush.SetSourceParameter("Tint", _imageBrush2);

            var _imageVisual = _compositor.CreateSpriteVisual();
            _imageVisual.Brush = brush;
            _imageVisual.Size = new Vector2(578, 384);
           

            ElementCompositionPreview.SetElementChildVisual(Background, _imageVisual);
        }
    }
}
