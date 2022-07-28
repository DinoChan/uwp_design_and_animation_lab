using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace DesignAndAnimationLab.AnimationTimelines
{
    public class TimelineProgresser
    {
        public TimelineProgresser(double seconds, bool autoReverse)
        {
            Duration = new Duration(TimeSpan.FromSeconds(seconds));
            AutoReverse = autoReverse;
        }

        public EasingFunctionBase EasingFunction { get; set; }

        public Duration Duration { get; set; } = new Duration(TimeSpan.FromSeconds(1));

        public bool AutoReverse { get; set; }

        public double GetCurrentProgress(TimeSpan timeSpan)
        {
            var durationTicks = Duration.TimeSpan.Ticks;
            var scalingFactor = AutoReverse ? 2d : 1d;
            var offsetFromBegin = timeSpan.Ticks % (durationTicks * scalingFactor);
            
            if (offsetFromBegin > durationTicks)
                offsetFromBegin = durationTicks * 2 - offsetFromBegin;
           
            double progress = offsetFromBegin / durationTicks;

            if (EasingFunction != null)
                progress = EasingFunction.Ease(progress);

            return progress;
        }
    }
}
