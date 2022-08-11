using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas.Geometry;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace DesignAndAnimationLab.Demos
{
    public class WaveCircle : FrameworkElement
    {
        public WaveCircle()
        {
            SizeChanged += OnSizeChanged;
        }

        private CompositionPath GetLine(Vector2 pt1, Vector2 pt2)
        {
            CanvasGeometry result;
            using (var builder = new CanvasPathBuilder(null))
            {
                builder.BeginFigure(pt1);
                builder.AddLine(pt2);
                builder.EndFigure(CanvasFigureLoop.Open);
                result = CanvasGeometry.CreatePath(builder);
            }
            return new CompositionPath(result);
        }

        private void OnSizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            var length = (float)Math.Min(e.NewSize.Width, e.NewSize.Height) * 0.95;
            var centerX = (float)e.NewSize.Width / 2;
            var centerY = (float)e.NewSize.Height / 2;

            var points = new List<Vector2>();
            var r = length / 2;
            var r2 = r * 1.06;
            var r3 = r * 0.951;
            int index = 0;
            int segments = 100;
            for (int i = 0; i < segments; i += 2)
            {
                var x = r * Math.Cos(i * 2 * Math.PI / segments) + centerX;
                var y = r * Math.Sin(i * 2 * Math.PI / segments) + centerY;

                points.Add(new Vector2((float)x, (float)y));
                var currentR = index++ % 2 == 0 ? r2 : r3;
                x = currentR * Math.Cos((i + 1) * 2 * Math.PI / segments) + centerX;
                y = currentR * Math.Sin((i + 1) * 2 * Math.PI / segments) + centerY;

                points.Add(new Vector2((float)x, (float)y));
            }

            points.Add(points[0]);

            CanvasGeometry result;
            using (var builder = new CanvasPathBuilder(null))
            {
                builder.BeginFigure(points[0]);
                for (int i = 0; i < points.Count - 2; i += 2)
                {
                    var currentPoint = points[i];
                    var centerPoint = points[i + 1];
                    var nextPoint = points[i + 2];
                    builder.AddCubicBezier(currentPoint, centerPoint, nextPoint);
                }
                builder.EndFigure(CanvasFigureLoop.Open);

                result = CanvasGeometry.CreatePath(builder);
            }
            var compositor = Window.Current.Compositor;
            var path = new CompositionPath(result);
            var line3 = compositor.CreatePathGeometry();
            line3.Path = path;
            var shape3 = compositor.CreateSpriteShape(line3);
            shape3.FillBrush = compositor.CreateColorBrush(Colors.Red);
            var visual = compositor.CreateShapeVisual();
            visual.Shapes.Add(shape3);
            visual.Size = e.NewSize.ToVector2();
            ElementCompositionPreview.SetElementChildVisual(this, visual);
        }
    }
}