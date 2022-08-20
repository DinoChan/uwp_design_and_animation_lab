using System;
using DesignAndAnimationLab.Demos;
using DesignAndAnimationLab.Demos.BubbleButton;
using DesignAndAnimationLab.Demos.GalaxyShuttles;
using DesignAndAnimationLab.Demos.GlitchArtDemo;
using DesignAndAnimationLab.Demos.Gooey;
using DesignAndAnimationLab.Demos.GooeyButtonDemo;
using DesignAndAnimationLab.Demos.LikeButtons;

namespace DesignAndAnimationLab
{
    public class ExampleDefinition
    {
        public ExampleDefinition(string name, Type control) : this(name, control, null)
        {
        }

        public ExampleDefinition(string name, Type control, Uri inspiredBy)
        {
            Name = name;
            Control = control;
            InspiredBy = inspiredBy;
        }

        public Type Control { get; }
        public Uri InspiredBy { get; }
        public string Name { get; }

        public string ThumbnailNarrow => "ms-appx:///Thumbnails/" + ThumbnailFilename("");

        public string ThumbnailWide => "ms-appx:///Thumbnails/" + ThumbnailFilename("");

        public string ThumbnailFilename(string suffix) => Name.Replace(" ", "") + suffix + ".png";

        public override string ToString() => Name;
    }

    public class ExampleDefinitions
    {
        public static ExampleDefinition[] Definitions { get; } =
        {
            new ExampleDefinition("Gooey Ellipse", typeof(GooeyEllipseDemoPage),
                new Uri("https://codepen.io/Chokcoco/pen/QqWBqV")),
            new ExampleDefinition("Gooey Ellipse 2", typeof(GooeyEllipseDemo2Page),
                new Uri("https://codepen.io/Chokcoco/pen/QqWBqV")),
            new ExampleDefinition("Gooey Ellipse PixelShader", typeof(GooeyEllipsePixelShaderPage),
                new Uri("https://codepen.io/Chokcoco/pen/QqWBqV")),
            new ExampleDefinition("Gooey Footer", typeof(GooeyFooter),
                new Uri("https://www.cnblogs.com/coco1s/p/16445448.html")),
            new ExampleDefinition("Gooey Footer2", typeof(GooeyFooter2),
                new Uri("https://www.cnblogs.com/coco1s/p/16445448.html")),
            new ExampleDefinition("Text Morph", typeof(TextMorph), new Uri("https://codepen.io/Valgo/pen/PowZaNY")),
            new ExampleDefinition("Three Actions With One Click", typeof(ThreeActionsWithOneClick)),
            new ExampleDefinition("Popup UserControl", typeof(PopupUserControlDemo)),
            new ExampleDefinition("Gooey Button", typeof(GooeyButtonDemoPage)),
            new ExampleDefinition("Glitch Art", typeof(GlitchArtDemoPage)),
            new ExampleDefinition("Glitch Art Many Words", typeof(GlitchArtWithManyWordsDemoPage)),
            new ExampleDefinition("Blend Mix Image", typeof(BlendMixImage)),
            new ExampleDefinition("Blend Mix Text", typeof(BlendMixText)),
            new ExampleDefinition("Bubble Button", typeof(BubbleButtonDemo),
                new Uri("https://github.com/cnbluefire/BubbleButton")),
            new ExampleDefinition("Twitter Like Button", typeof(MattHenleysLikeButton)),
            new ExampleDefinition("Walking Cat", typeof(WalkingCat)),
            new ExampleDefinition("Transparent Cube", typeof(TransparentCube)),
            new ExampleDefinition("Acrylic and Blur", typeof(AcrylicAndBlurDemo)),
            new ExampleDefinition("Control Center", typeof(ControlCenterDemo)),
            new ExampleDefinition("Galaxy Shuttle", typeof(GalaxyShuttleDemo))

            //// Visual demos.
            //new ExampleDefinition("Burning Text", typeof(BurningTextExample)),
            //new ExampleDefinition("Mandelbrot", typeof(Mandelbrot)),
            //new ExampleDefinition("Game of Life", typeof(GameOfLife)),
            //new ExampleDefinition("Camera Effect", typeof(CameraEffectExample)),
            //new ExampleDefinition("Video Effect", typeof(BasicVideoEffectExample)),
            //new ExampleDefinition("Particle System", typeof(ParticleExample)),
            //new ExampleDefinition("Sprite Sheets", typeof(SpriteSheets)),
            //new ExampleDefinition("Vector Art", typeof(VectorArt)),

            //// Show capabilities of the API.
            //new ExampleDefinition("Effects", typeof(EffectsExample)),
            //new ExampleDefinition("Custom Effects", typeof(CustomEffects)),
            //new ExampleDefinition("Geometry Operations", typeof(GeometryOperations)),
            //new ExampleDefinition("Layers", typeof(LayersExample)),
            //new ExampleDefinition("Ink", typeof(InkExample)),
            //new ExampleDefinition("Shapes", typeof(ShapesExample)),
            //new ExampleDefinition("Stroke Styles", typeof(StrokeStyles)),
            //new ExampleDefinition("Arc Options", typeof(ArcOptions)),
            //new ExampleDefinition("SVG", typeof(SvgExample)),
            //new ExampleDefinition("Gradient Mesh", typeof(GradientMeshExample)),
            //new ExampleDefinition("Histogram", typeof(HistogramExample)),
            //new ExampleDefinition("Text Layouts", typeof(TextLayouts)),
            //new ExampleDefinition("Text Outlines", typeof(TextOutlines)),
            //new ExampleDefinition("Custom Fonts", typeof(CustomFonts)),
            //new ExampleDefinition("Text Directions", typeof(TextDirectionsExample)),
            //new ExampleDefinition("Subscript And Superscript", typeof(SubscriptSuperscript)),
            //new ExampleDefinition("Typography", typeof(TypographyExample)),
            //new ExampleDefinition("Glyph Rotation", typeof(GlyphRotation)),
            //new ExampleDefinition("Font Metrics", typeof(FontMetrics)),
            //new ExampleDefinition("Custom Text Layouts", typeof(CustomTextLayouts)),

            //// Show how to use the API.
            //new ExampleDefinition("Custom Control", typeof(CustomControlExample)),
            //new ExampleDefinition("Virtual Control", typeof(VirtualControlExample)),
            //new ExampleDefinition("Animated Control", typeof(AnimatedControlExample)),
            //new ExampleDefinition("Virtual Bitmap", typeof(VirtualBitmapExample)),
            //new ExampleDefinition("Direct3D Interop", typeof(Direct3DInteropExample)),
            //new ExampleDefinition("Keyboard Input", typeof(KeyboardInputExample)),
            //new ExampleDefinition("Background Task", typeof(BackgroundTaskExample)),
            //new ExampleDefinition("Printing", typeof(PrintingExample)),

            //// Tests for internal use by the Win2D team.
            //new ExampleDefinition("Image Source Update Region", typeof(ImageSourceUpdateRegion)),
            //new ExampleDefinition("DPI", typeof(DpiExample)),
            //new ExampleDefinition("Effect Region Mapping", typeof(EffectRegionMapping)),
            //new ExampleDefinition("Control Transforms", typeof(ControlTransforms)),
            //new ExampleDefinition("DrawImage Emulations", typeof(DrawImageEmulations)),
            //new ExampleDefinition("Bitmap Rotation", typeof(BitmapRotation)),
            //new ExampleDefinition("SpriteBatch Performance", typeof(SpriteBatchPerf)),
            //new ExampleDefinition("Developer Tools", typeof(DeveloperTools)),
            //new ExampleDefinition("About", typeof(About)),
        };
    }
}