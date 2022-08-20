// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//

using System;

namespace DesignAndAnimationLab.AnimationTimelines
{
    /// <summary>
    ///     This class implements an easing function that gives a circular curve toward the destination.
    /// </summary>
    public class CircleEase : EasingFunctionBase
    {
        protected override double EaseInCore(double normalizedTime)
        {
            normalizedTime = Math.Max(0.0, Math.Min(1.0, normalizedTime));
            return 1.0 - Math.Sqrt(1.0 - normalizedTime * normalizedTime);
        }
    }
}