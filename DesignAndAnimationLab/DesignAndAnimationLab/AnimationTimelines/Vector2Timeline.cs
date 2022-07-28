using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DesignAndAnimationLab.AnimationTimelines
{
    public class Vector2Timeline
    {
        private TimelineProgresser _progresser;

        public Vector2Timeline(Vector2 from, Vector2 to, double seconds = 1, bool autoReverse = true, EasingFunctionBase easingFunction = null)
        {
            _progresser = new TimelineProgresser(seconds, autoReverse) { EasingFunction = easingFunction };
            From = from;
            To = to;
            Seconds = seconds;
            AutoReverse = autoReverse;
        }

        public Vector2 From { get; }
        public Vector2 To { get; }
        public double Seconds { get; }
        public bool AutoReverse { get; }

        public Vector2 GetCurrentValue(TimeSpan timeSpan)
        {
            var progress = (float)_progresser.GetCurrentProgress(timeSpan);
            return From + (To - From) * progress;
        }
    }
}
