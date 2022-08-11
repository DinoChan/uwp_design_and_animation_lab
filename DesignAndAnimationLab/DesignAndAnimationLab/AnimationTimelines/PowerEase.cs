// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//

using System;

namespace DesignAndAnimationLab.AnimationTimelines
{
    /// <summary>
    ///     This class implements an easing function that gives a polynomial curve of arbitrary degree.
    ///     If the curve you desire is cubic, quadratic, quartic, or quintic it is better to use the
    ///     specialized easing functions.
    /// </summary>
    public class PowerEase : EasingFunctionBase
    {
        public PowerEase()
        {
        }

        /// <summary>
        /// Specifies the power for the polynomial equation.
        /// </summary>
        public double Power { get; set; } = 2;

        protected override double EaseInCore(double normalizedTime)
        {
            double power = Math.Max(0.0, Power);
            return Math.Pow(normalizedTime, power);
        }
    }
}