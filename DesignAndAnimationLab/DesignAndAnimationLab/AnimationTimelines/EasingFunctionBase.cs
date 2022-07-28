using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignAndAnimationLab.AnimationTimelines
{
    public abstract class EasingFunctionBase
    {    /// <summary>
         /// Specifies the easing behavior.
         /// </summary>
        public EasingMode EasingMode { get; set; } = EasingMode.EaseOut;

        /// <summary>
        ///     Transforms normalized time to control the pace of an animation.
        /// </summary>
        /// <param name="normalizedTime">normalized time (progress) of the animation</param>
        /// <returns>transformed progress</returns>
        /// <remarks>Uses EasingMode in conjunction with EaseInCore to evaluate the easing function.</remarks>
        public double Ease(double normalizedTime)
        {
            switch (EasingMode)
            {
                case EasingMode.EaseIn:
                    return EaseInCore(normalizedTime);
                case EasingMode.EaseOut:
                    // EaseOut is the same as EaseIn, except time is reversed & the result is flipped.
                    return 1.0 - EaseInCore(1.0 - normalizedTime);
                case EasingMode.EaseInOut:
                default:
                    // EaseInOut is a combination of EaseIn & EaseOut fit to the 0-1, 0-1 range.
                    return (normalizedTime < 0.5) ?
                               EaseInCore(normalizedTime * 2.0) * 0.5 :
                        (1.0 - EaseInCore((1.0 - normalizedTime) * 2.0)) * 0.5 + 0.5;
            }
        }

        /// <summary>
        ///     Transforms normalized time to control the pace of an animation for the EaseIn EasingMode
        /// </summary>
        /// <param name="normalizedTime">normalized time (progress) of the animation</param>
        /// <returns>transformed progress</returns>
        /// <remarks>
        ///     You only have to specifiy your easing function for the 'EaseIn' case because the implementation 
        ///     of Ease will handle transforming normalizedTime & the result of this method to handle 'EaseOut' & 'EaseInOut'.
        /// </remarks>
        protected abstract double EaseInCore(double normalizedTime);
    }
}
