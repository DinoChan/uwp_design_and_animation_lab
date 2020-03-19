using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    class TextToBrushWrapper : Control
    {
        private Compositor Compositor => Window.Current.Compositor;

        protected CompositionDrawingSurface DrawingSurface { get; private set; }
        private CompositionGraphicsDevice _graphicsDevice;
        private SpriteVisual _spriteTextVisual;

        public CompositionSurfaceBrush Brush { get; private set; }

        public TextToBrushWrapper()
        {
            _graphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(Compositor, CanvasDevice.GetSharedDevice());
            _spriteTextVisual = Compositor.CreateSpriteVisual();
            DrawingSurface = _graphicsDevice.CreateDrawingSurface(new Windows.Foundation.Size(10, 10), DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied);
            Brush = Compositor.CreateSurfaceBrush(DrawingSurface);
            RegisterPropertyChangedCallback(FontSizeProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                DrawSurface();
            }));
        }


        /// <summary>
        /// 获取或设置Size的值
        /// </summary>
        public Vector2 Size
        {
            get => (Vector2)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        /// 标识 Size 依赖属性。
        /// </summary>
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(Vector2), typeof(TextToBrushWrapper), new PropertyMetadata(default(Vector2), OnSizeChanged));

        private static void OnSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (Vector2)args.OldValue;
            var newValue = (Vector2)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnSizeChanged(oldValue, newValue);
        }

        /// <summary>
        /// Size 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">Size 属性的旧值。</param>
        /// <param name="newValue">Size 属性的新值。</param>
        protected virtual void OnSizeChanged(Vector2 oldValue, Vector2 newValue)
        {
            DrawingSurface.Resize(new Windows.Graphics.SizeInt32 { Width = (int)newValue.X, Height = (int)newValue.Y });
            DrawSurface();
        }

        /// <summary>
        /// 获取或设置OutlineColor的值
        /// </summary>
        public Color OutlineColor
        {
            get => (Color)GetValue(OutlineColorProperty);
            set => SetValue(OutlineColorProperty, value);
        }

        /// <summary>
        /// 标识 OutlineColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty OutlineColorProperty =
            DependencyProperty.Register(nameof(OutlineColor), typeof(Color), typeof(TextToBrushWrapper), new PropertyMetadata(Colors.Black, OnOutlineColorChanged));

        private static void OnOutlineColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (Color)args.OldValue;
            var newValue = (Color)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnOutlineColorChanged(oldValue, newValue);
        }

        /// <summary>
        /// OutlineColor 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">OutlineColor 属性的旧值。</param>
        /// <param name="newValue">OutlineColor 属性的新值。</param>
        protected virtual void OnOutlineColorChanged(Color oldValue, Color newValue)
        {
            DrawSurface();
        }



        /// <summary>
        /// 获取或设置FontColor的值
        /// </summary>
        public Color FontColor
        {
            get => (Color)GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }

        /// <summary>
        /// 标识 FontColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty FontColorProperty =
            DependencyProperty.Register(nameof(FontColor), typeof(Color), typeof(TextToBrushWrapper), new PropertyMetadata(Colors.Black, OnFontColorChanged));

        private static void OnFontColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (Color)args.OldValue;
            var newValue = (Color)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnFontColorChanged(oldValue, newValue);
        }

        /// <summary>
        /// FontColor 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">FontColor 属性的旧值。</param>
        /// <param name="newValue">FontColor 属性的新值。</param>
        protected virtual void OnFontColorChanged(Color oldValue, Color newValue)
        {
            DrawSurface();
        }


        /// <summary>
        /// 获取或设置Text的值
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// 标识 Text 依赖属性。
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextToBrushWrapper), new PropertyMetadata(default(string), OnTextChanged));

        private static void OnTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (string)args.OldValue;
            var newValue = (string)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnTextChanged(oldValue, newValue);
        }

        /// <summary>
        /// Text 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">Text 属性的旧值。</param>
        /// <param name="newValue">Text 属性的新值。</param>
        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            DrawSurface();
        }



        /// <summary>
        /// 获取或设置StrokeStyle的值
        /// </summary>
        public CanvasDashStyle DashStyle
        {
            get => (CanvasDashStyle)GetValue(DashStyleProperty);
            set => SetValue(DashStyleProperty, value);
        }

        /// <summary>
        /// 标识 StrokeStyle 依赖属性。
        /// </summary>
        public static readonly DependencyProperty DashStyleProperty =
            DependencyProperty.Register(nameof(DashStyle), typeof(CanvasDashStyle), typeof(TextToBrushWrapper), new PropertyMetadata(default(CanvasDashStyle), OnDashStyleChanged));

        private static void OnDashStyleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (CanvasDashStyle)args.OldValue;
            var newValue = (CanvasDashStyle)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnDashStyleChanged(oldValue, newValue);
        }

        /// <summary>
        /// StrokeStyle 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">StrokeStyle 属性的旧值。</param>
        /// <param name="newValue">StrokeStyle 属性的新值。</param>
        protected virtual void OnDashStyleChanged(CanvasDashStyle oldValue, CanvasDashStyle newValue)
        {
            DrawSurface();
        }



        /// <summary>
        /// 获取或设置ShowNonOutlineText的值
        /// </summary>
        public bool ShowNonOutlineText
        {
            get => (bool)GetValue(ShowNonOutlineTextProperty);
            set => SetValue(ShowNonOutlineTextProperty, value);
        }

        /// <summary>
        /// 标识 ShowNonOutlineText 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ShowNonOutlineTextProperty =
            DependencyProperty.Register(nameof(ShowNonOutlineText), typeof(bool), typeof(TextToBrushWrapper), new PropertyMetadata(true, OnShowNonOutlineTextChanged));

        private static void OnShowNonOutlineTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (bool)args.OldValue;
            var newValue = (bool)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnShowNonOutlineTextChanged(oldValue, newValue);
        }

        /// <summary>
        /// ShowNonOutlineText 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">ShowNonOutlineText 属性的旧值。</param>
        /// <param name="newValue">ShowNonOutlineText 属性的新值。</param>
        protected virtual void OnShowNonOutlineTextChanged(bool oldValue, bool newValue)
        {
            DrawSurface();
        }


        /// <summary>
        /// 获取或设置StrokeWidth的值
        /// </summary>
        public double StrokeWidth
        {
            get => (double)GetValue(StrokeWidthProperty);
            set => SetValue(StrokeWidthProperty, value);
        }

        /// <summary>
        /// 标识 StrokeWidth 依赖属性。
        /// </summary>
        public static readonly DependencyProperty StrokeWidthProperty =
            DependencyProperty.Register(nameof(StrokeWidth), typeof(double), typeof(TextToBrushWrapper), new PropertyMetadata(default(double), OnStrokeWidthChanged));

        private static void OnStrokeWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (double)args.OldValue;
            var newValue = (double)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnStrokeWidthChanged(oldValue, newValue);
        }

        /// <summary>
        /// StrokeWidth 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">StrokeWidth 属性的旧值。</param>
        /// <param name="newValue">StrokeWidth 属性的新值。</param>
        protected virtual void OnStrokeWidthChanged(double oldValue, double newValue)
        {
            DrawSurface();
        }

        protected void DrawSurface()
        {
            if (Size.X == 0 || Size.Y == 0 || string.IsNullOrWhiteSpace(Text) || DrawingSurface == null)
                return;

            var width = (float)Size.X;
            var height = (float)Size.Y;

            DrawSurfaceCore(DrawingSurface, width, height);
        }

        protected virtual void DrawSurfaceCore(CompositionDrawingSurface drawingSurface, float width, float height)
        {
            using (var session = CanvasComposition.CreateDrawingSession(drawingSurface))
            {
                session.Clear(Colors.Transparent);
                using (var textFormat = new CanvasTextFormat()
                {
                    FontSize = (float)FontSize,
                    Direction = CanvasTextDirection.LeftToRightThenTopToBottom,
                    VerticalAlignment = CanvasVerticalAlignment.Center,
                    HorizontalAlignment = CanvasHorizontalAlignment.Center,
                    FontWeight = FontWeight,
                    FontFamily = FontFamily.Source
                })
                {
                    using (var textLayout = new CanvasTextLayout(session, Text, textFormat, width, height))
                    {
                        DrawText(session, textLayout);
                    }
                }
            }
        }


        protected void DrawText(CanvasDrawingSession session, CanvasTextLayout textLayout)
        {
            if (ShowNonOutlineText)
                session.DrawTextLayout(textLayout, 0, 0, FontColor);

            using (var textGeometry = CanvasGeometry.CreateText(textLayout))
            {
                using (var dashedStroke = new CanvasStrokeStyle())
                {
                    dashedStroke.DashStyle = DashStyle;
                    session.DrawGeometry(textGeometry, OutlineColor, (float)StrokeWidth, dashedStroke);
                }
            }
        }

    }
}
