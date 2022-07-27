using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Composition;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DesignAndAnimationLab.Demos.GlitchArtDemo
{
    public class TextToBrushWrapper : Control
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
            RegisterPropertyChangedCallback(BackgroundProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                DrawSurface();
            }));
            RegisterPropertyChangedCallback(HeightProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                if (double.IsNaN(Height) || double.IsNaN(Width) || Width == 0 || Height == 0)
                    return;

                DrawingSurface.Resize(new Windows.Graphics.SizeInt32 { Width = (int)Width, Height = (int)Height });
                DrawSurface();
            }));

            RegisterPropertyChangedCallback(WidthProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                if (double.IsNaN(Height) || double.IsNaN(Width) || Width == 0 || Height == 0)
                    return;

                DrawingSurface.Resize(new Windows.Graphics.SizeInt32 { Width = (int)Width, Height = (int)Height });
                DrawSurface();
            }));
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
            if (double.IsNaN(Height) || double.IsNaN(Width) || Width == 0 || Height == 0 || string.IsNullOrWhiteSpace(Text) || DrawingSurface == null)
                return;

            var width = (float)Width;
            var height = (float)Height;

            DrawSurfaceCore(DrawingSurface, width, height);
        }

        protected virtual void DrawSurfaceCore(CompositionDrawingSurface drawingSurface, float width, float height)
        {
            using (var session = CanvasComposition.CreateDrawingSession(drawingSurface))
            {
                session.Clear(Colors.Transparent);
                if (Background is SolidColorBrush solidColorBrush)
                    session.FillRectangle(new Windows.Foundation.Rect(0, 0, width, height), solidColorBrush.Color);

                using (var textFormat = new CanvasTextFormat()
                {
                    FontSize = (float)FontSize,
                    Direction = CanvasTextDirection.LeftToRightThenTopToBottom,
                    VerticalAlignment = CanvasVerticalAlignment.Top,
                    HorizontalAlignment = CanvasHorizontalAlignment.Center,
                    FontWeight = FontWeight,
                    FontFamily = FontFamily.Source,
                })
                {
                    using (var textLayout = new CanvasTextLayout(session, Text, textFormat, width, height))
                    {
                        var fullSizeGeometry = CanvasGeometry.CreateRectangle(session, 0, 0, width, height);
                        var textGeometry = CanvasGeometry.CreateText(textLayout);
                        var finalGeometry = fullSizeGeometry.CombineWith(textGeometry, Matrix3x2.Identity, CanvasGeometryCombine.Exclude);
                        using (var layer = session.CreateLayer(1, fullSizeGeometry))
                        {
                            using (var bitmap = new CanvasRenderTarget(session, width, height))
                            {
                                using (var bitmapSession = bitmap.CreateDrawingSession())
                                {
                                    DrawText(bitmapSession, textLayout, ShadowColor);
                                }
                                using (var blur = new GaussianBlurEffect
                                {
                                    BlurAmount = (float)ShadowBlurAmount,
                                    Source = bitmap,
                                    BorderMode = EffectBorderMode.Hard
                                })
                                {
                                    session.DrawImage(blur, (float)ShadowOffsetX, (float)ShadowOffsetY);
                                }
                            }
                        }
                        DrawText(session, textLayout, FontColor);
                    }
                }
            }
        }

        protected void DrawText(CanvasDrawingSession session, CanvasTextLayout textLayout, Color color)
        {
            if (ShowNonOutlineText)
                session.DrawTextLayout(textLayout, 0, 0, color);

            using (var textGeometry = CanvasGeometry.CreateText(textLayout))
            {
                using (var dashedStroke = new CanvasStrokeStyle())
                {
                    dashedStroke.DashStyle = DashStyle;
                    session.DrawGeometry(textGeometry, OutlineColor, (float)StrokeWidth, dashedStroke);
                }
            }
        }

        /// <summary>
        /// 获取或设置BlurAmount的值
        /// </summary>
        public double ShadowBlurAmount
        {
            get => (double)GetValue(ShadowBlurAmountProperty);
            set => SetValue(ShadowBlurAmountProperty, value);
        }

        /// <summary>
        /// 标识 BlurAmount 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ShadowBlurAmountProperty =
            DependencyProperty.Register(nameof(ShadowBlurAmount), typeof(double), typeof(TextToBrushWrapper), new PropertyMetadata(10d, OnShadowBlurAmountChanged));

        private static void OnShadowBlurAmountChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (double)args.OldValue;
            var newValue = (double)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnShadowBlurAmountChanged(oldValue, newValue);
        }

        /// <summary>
        /// BlurAmount 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">BlurAmount 属性的旧值。</param>
        /// <param name="newValue">BlurAmount 属性的新值。</param>
        protected virtual void OnShadowBlurAmountChanged(double oldValue, double newValue)
        {
            DrawSurface();
        }

        /// <summary>
        /// 获取或设置ShadowOffsetX的值
        /// </summary>
        public double ShadowOffsetX
        {
            get => (double)GetValue(ShadowOffsetXProperty);
            set => SetValue(ShadowOffsetXProperty, value);
        }

        /// <summary>
        /// 标识 ShadowOffsetX 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ShadowOffsetXProperty =
            DependencyProperty.Register(nameof(ShadowOffsetX), typeof(double), typeof(TextToBrushWrapper), new PropertyMetadata(default(double), OnShadowOffsetXChanged));

        private static void OnShadowOffsetXChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (double)args.OldValue;
            var newValue = (double)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnShadowOffsetXChanged(oldValue, newValue);
        }

        /// <summary>
        /// ShadowOffsetX 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">ShadowOffsetX 属性的旧值。</param>
        /// <param name="newValue">ShadowOffsetX 属性的新值。</param>
        protected virtual void OnShadowOffsetXChanged(double oldValue, double newValue)
        {
            DrawSurface();
        }

        /// <summary>
        /// 获取或设置ShadowOffsetY的值
        /// </summary>
        public double ShadowOffsetY
        {
            get => (double)GetValue(ShadowOffsetYProperty);
            set => SetValue(ShadowOffsetYProperty, value);
        }

        /// <summary>
        /// 标识 ShadowOffsetY 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ShadowOffsetYProperty =
            DependencyProperty.Register(nameof(ShadowOffsetY), typeof(double), typeof(TextToBrushWrapper), new PropertyMetadata(default(double), OnShadowOffsetYChanged));

        private static void OnShadowOffsetYChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (double)args.OldValue;
            var newValue = (double)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnShadowOffsetYChanged(oldValue, newValue);
        }

        /// <summary>
        /// ShadowOffsetY 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">ShadowOffsetY 属性的旧值。</param>
        /// <param name="newValue">ShadowOffsetY 属性的新值。</param>
        protected virtual void OnShadowOffsetYChanged(double oldValue, double newValue)
        {
            DrawSurface();
        }

        /// <summary>
        /// 获取或设置ShadowColor的值
        /// </summary>
        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        /// <summary>
        /// 标识 ShadowColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register(nameof(ShadowColor), typeof(Color), typeof(TextToBrushWrapper), new PropertyMetadata(Colors.Black, OnShadowColorChanged));

        private static void OnShadowColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (Color)args.OldValue;
            var newValue = (Color)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as TextToBrushWrapper;
            target?.OnShadowColorChanged(oldValue, newValue);
        }

        /// <summary>
        /// ShadowColor 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">ShadowColor 属性的旧值。</param>
        /// <param name="newValue">ShadowColor 属性的新值。</param>
        protected virtual void OnShadowColorChanged(Color oldValue, Color newValue)
        {
            DrawSurface();
        }
    }
}
