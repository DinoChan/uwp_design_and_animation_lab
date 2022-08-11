using System;
using Windows.UI.Xaml;

namespace DesignAndAnimationLab.AnimationTimelines
{
    public class DoubleTimeline
    {
        private TimelineProgresser _progresser;

        public DoubleTimeline(double from = 0, double to = 1, double seconds = 1, TimeSpan? beginTime = null, bool autoReverse = true, bool forever = true, EasingFunctionBase easingFunction = null)
        {
            _progresser = new TimelineProgresser(seconds, autoReverse) { EasingFunction = easingFunction, BeginTime = beginTime, Forever = forever };
            From = from;
            To = to;
            Duration = new Duration(TimeSpan.FromSeconds(seconds));
            AutoReverse = autoReverse;
            BeginTime = beginTime;
            Forever = forever;
        }

        public bool AutoReverse { get; }
        public TimeSpan? BeginTime { get; }
        public Duration Duration { get; }
        public bool Forever { get; }
        public double From { get; }
        public double To { get; }

        public double GetCurrentProgress(TimeSpan timeSpan)
        {
            var progress = _progresser.GetCurrentProgress(timeSpan);
            return From + (To - From) * progress;
        }
    }
}