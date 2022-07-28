using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace DesignAndAnimationLab.Common
{
    public class DoubleProgresser
    {
        public DoubleProgresser(double seconds, bool autoReverse)
        {
            Duration = new Duration(TimeSpan.FromSeconds(seconds));
            AutoReverse = autoReverse;
        }

        public EasingFunctionBase EasingFunction { get; set; }

        public Duration Duration { get; set; } = new Duration(TimeSpan.FromSeconds(1));

        public bool AutoReverse { get; set; }

        public double GetCurrentProgress(TimeSpan timeSpan)
        {
            var scalingFactor = AutoReverse ? 2d : 1d;
            var offsetFromBegin = timeSpan.Ticks % (Duration.TimeSpan.Ticks * scalingFactor) % Duration.TimeSpan.Ticks;
            double progress = offsetFromBegin / Duration.TimeSpan.Ticks;

            if (EasingFunction != null)
                progress = EasingFunction.Ease(progress);

            return progress;
        }
    }
}
