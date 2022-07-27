using System;
using Windows.UI.Composition;

namespace DesignAndAnimationLab.Common
{
    public static class Extensions
    {
        public static void BindSize(this Visual target, Visual source)
        {
            var exp = target.Compositor.CreateExpressionAnimation("host.Size");
            exp.SetReferenceParameter("host", source);
            target.StartAnimation("Size", exp);
        }

        public static void BindCenterPoint(this Visual target)
        {
            var exp = target.Compositor.CreateExpressionAnimation("Vector3(this.Target.Size.X / 2, this.Target.Size.Y / 2, 0f)");
            target.StartAnimation("CenterPoint", exp);
        }

        public static ImplicitAnimationCollection CreateImplicitAnimation(this ImplicitAnimationCollection source, string Target, TimeSpan? Duration = null)
        {
            KeyFrameAnimation animation = null;
            switch (Target.ToLower())
            {
                case "offset":
                case "scale":
                case "centerPoint":
                case "rotationAxis":
                    animation = source.Compositor.CreateVector3KeyFrameAnimation();
                    break;

                case "size":
                    animation = source.Compositor.CreateVector2KeyFrameAnimation();
                    break;

                case "opacity":
                case "blueRadius":
                case "rotationAngle":
                case "rotationAngleInDegrees":
                    animation = source.Compositor.CreateScalarKeyFrameAnimation();
                    break;

                case "color":
                    animation = source.Compositor.CreateColorKeyFrameAnimation();
                    break;
            }
            if (animation == null) throw new ArgumentNullException("未知的Target");
            if (!Duration.HasValue) Duration = TimeSpan.FromSeconds(0.2d);
            animation.InsertExpressionKeyFrame(1f, "this.FinalValue");
            animation.Duration = Duration.Value;
            animation.Target = Target;

            source[Target] = animation;
            return source;
        }
    }
}
