// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//

using System;
using DesignAndAnimationLab.Common;

namespace DesignAndAnimationLab.AnimationTimelines
{
    /// <summary>
    ///     This class implements an easing function that gives an exponential curve
    /// </summary>
    public class ExponentialEase : EasingFunctionBase
    {
        /// <summary>
        ///     Specifies the factor which controls the shape of easing.
        /// </summary>
        public double Exponent { get; set; } = 2;

        protected override double EaseInCore(double normalizedTime)
        {
            var factor = Exponent;
            if (DoubleUtil.IsZero(factor))
            {
                return normalizedTime;
            }

            return (Math.Exp(factor * normalizedTime) - 1.0) / (Math.Exp(factor) - 1.0);
        }
    }
}