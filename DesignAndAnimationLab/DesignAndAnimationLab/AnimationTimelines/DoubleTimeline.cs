using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignAndAnimationLab.AnimationTimelines
{
    public class DoubleTimeline
    {
        private TimelineProgresser _progresser;

        public DoubleTimeline(double from = 0, double to = 1, double seconds = 1, bool autoReverse = true, EasingFunctionBase easingFunction = null)
        {
            _progresser = new TimelineProgresser(seconds, autoReverse) { EasingFunction = easingFunction };
            From = from;
            To = to;
            Seconds = seconds;
            AutoReverse = autoReverse;
        }

        public double From { get; }
        public double To { get; }
        public double Seconds { get; }
        public bool AutoReverse { get; }

        public double GetCurrentProgress(TimeSpan timeSpan)
        {
            var progress = _progresser.GetCurrentProgress(timeSpan);
            return From + (To - From) * progress;
        }
    }
}
