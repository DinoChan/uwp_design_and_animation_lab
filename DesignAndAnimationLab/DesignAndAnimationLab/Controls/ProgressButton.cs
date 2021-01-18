using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace DesignAndAnimationLab
{
    public class ProgressButton : RangeBase
    {

        public ProgressButton()
        {
            _gestureRecognizer.GestureSettings = GestureSettings.HoldWithMouse | GestureSettings.Tap | GestureSettings.Hold;
            IsEnabledChanged += OnIsEnabledChanged;
        }


        private bool _isAnimateBegin = false;
        GestureRecognizer _gestureRecognizer = new GestureRecognizer();
        private bool _isPointerCaptured;
        public event EventHandler Click;
        public event EventHandler<bool> Hold;

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ProgressButton), new PropertyMetadata(false, new PropertyChangedCallback(IsCheck_Changed)));

        private static void IsCheck_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool v = (bool)e.NewValue;
            var instance = d as ProgressButton;
            if (v)
                VisualStateManager.GoToState(instance, "Checked", true);
            else
                VisualStateManager.GoToState(instance, "Default", true);
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ProgressButton), new PropertyMetadata(""));

      


        public bool CanHolding
        {
            get { return (bool)GetValue(CanHoldingProperty); }
            set { SetValue(CanHoldingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanHolding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanHoldingProperty =
            DependencyProperty.Register("CanHolding", typeof(bool), typeof(ProgressButton), new PropertyMetadata(false));


        private void PressStoryBoard_Completed(object sender, object e)
        {
            Hold?.Invoke(this, true);
            ShowBubble();
            PressProgressBar.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _gestureRecognizer.Holding -= gestureRecognizer_Holding;
            _gestureRecognizer.Tapped -= gestureRecognizer_Tapped;
            _gestureRecognizer.Holding += gestureRecognizer_Holding;
            _gestureRecognizer.Tapped += gestureRecognizer_Tapped;
        }

        private void gestureRecognizer_Tapped(GestureRecognizer sender, TappedEventArgs args)
        {
            Click?.Invoke(this, EventArgs.Empty);
        }

        void gestureRecognizer_Holding(GestureRecognizer sender, HoldingEventArgs args)
        {
            if (Hold != null && CanHolding)
            {
                if (args.HoldingState == HoldingState.Started)
                {
                    PressProgressBar.Visibility = Visibility.Visible;
                    if (!_isAnimateBegin)
                    {
                        _isAnimateBegin = true;
                        PressStoryBoard.Begin();
                    }
                }
                else
                {
                    _isAnimateBegin = false;
                    PressStoryBoard.Stop();
                    PressProgressBar.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Handled)
                return;

            if (IsEnabled == false)
                return;

            e.Handled = true;
            _isPointerCaptured = IconContainer.CapturePointer(e.Pointer);
            if (_isPointerCaptured == false)
                return;

            Focus(FocusState.Pointer);
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                _gestureRecognizer.ProcessDownEvent(ps[0]);
                e.Handled = true;
            }
        }

        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            _gestureRecognizer.ProcessMoveEvents(e.GetIntermediatePoints(null));
            e.Handled = true;
        }

        private void Grid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Handled)
                return;

            if (IsEnabled == false)
                return;

            ReleasePointerCapture(e.Pointer);
            _isPointerCaptured = false;

            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                _gestureRecognizer.ProcessUpEvent(ps[0]);
                e.Handled = true;
                _gestureRecognizer.CompleteGesture();
            }
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsEnabled)
            {
                _isPointerCaptured = false;
            }
        }


        public void ShowBubble()
        {
            //BubbleView.IsBubbing = true;
        }


    }
}
