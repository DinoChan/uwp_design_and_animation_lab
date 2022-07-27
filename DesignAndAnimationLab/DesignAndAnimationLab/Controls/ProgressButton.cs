using System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

namespace DesignAndAnimationLab
{
    [TemplateVisualState(GroupName = ProgressStatesGroupName, Name = IdleStateName)]
    [TemplateVisualState(GroupName = ProgressStatesGroupName, Name = InProgressStateName)]
    [TemplateVisualState(GroupName = ProgressStatesGroupName, Name = CompletedStateName)]
    [TemplateVisualState(GroupName = ProgressStatesGroupName, Name = FaultedStateName)]
    [TemplateVisualState(Name = StateNormal, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StatePointerOver, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StatePressed, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StateDisabled, GroupName = GroupCommon)]
    [ContentProperty(Name = nameof(Content))]
    public partial class ProgressButton : RangeBase
    {
        private bool _isPointerCaptured;
        private GestureRecognizer _gestureRecognizer = new GestureRecognizer();

        public ProgressButton()
        {
            DefaultStyleKey = typeof(ProgressButton);
            _gestureRecognizer.GestureSettings = GestureSettings.HoldWithMouse | GestureSettings.Tap | GestureSettings.Hold;
            _gestureRecognizer.Holding += OnGestureRecognizerHolding;
            _gestureRecognizer.Tapped += OnGestureRecognizerTapped;
            IsEnabledChanged += OnIsEnabledChanged;
        }

        public event EventHandler StateChanged;

        public event EventHandler<HoldingEventArgs> GestureRecognizerHolding;

        public event EventHandler<TappedEventArgs> GestureRecognizerTapped;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualStates(false);
        }

        protected virtual void OnStateChanged(ProgressState oldValue, ProgressState newValue)
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
            UpdateVisualStates(true);
        }

        protected override void OnPointerCaptureLost(PointerRoutedEventArgs e)
        {
            base.OnPointerCaptureLost(e);
            _isPointerCaptured = false;
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (e.Handled)
                return;

            if (IsEnabled == false)
                return;

            e.Handled = true;
            _isPointerCaptured = CapturePointer(e.Pointer);
            if (_isPointerCaptured == false)
                return;

            IsPressed = true;
            Focus(FocusState.Pointer);
            UpdateVisualStates();
            var points = e.GetIntermediatePoints(null);
            if (points != null && points.Count > 0)
            {
                _gestureRecognizer.ProcessDownEvent(points[0]);
                e.Handled = true;
            }
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            if (e.Handled)
                return;

            if (IsEnabled == false)
                return;

            e.Handled = true;

            IsPressed = false;
            ReleasePointerCapture(e.Pointer);
            _isPointerCaptured = false;
            UpdateVisualStates();

            var points = e.GetIntermediatePoints(null);
            if (points != null && points.Count > 0)
            {
                _gestureRecognizer.ProcessUpEvent(points[0]);
                e.Handled = true;
                _gestureRecognizer.CompleteGesture();
            }
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
            _gestureRecognizer.ProcessMoveEvents(e.GetIntermediatePoints(null));
            e.Handled = true;
            if (_isPointerCaptured == false)
                return;

            var position = e.GetCurrentPoint(this).Position;
            if (position.X < 0 || position.Y < 0 || position.X > ActualWidth || position.Y > ActualHeight)
                IsPressed = false;
            else
                IsPressed = true;

            UpdateVisualStates();
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            IsPointerOver = true;
            UpdateVisualStates();
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            IsPointerOver = false;
            UpdateVisualStates();
        }

        private void UpdateVisualStates(bool useTransitions = true)
        {
            switch (State)
            {
                case ProgressState.Idle:
                    VisualStateManager.GoToState(this, IdleStateName, useTransitions);
                    break;

                case ProgressState.InProgress:
                    VisualStateManager.GoToState(this, InProgressStateName, useTransitions);
                    break;

                case ProgressState.Completed:
                    VisualStateManager.GoToState(this, CompletedStateName, useTransitions);
                    break;

                case ProgressState.Faulted:
                    VisualStateManager.GoToState(this, FaultedStateName, useTransitions);
                    break;

                default:
                    VisualStateManager.GoToState(this, IdleStateName, useTransitions);
                    break;
            }

            if (IsEnabled == false)
                VisualStateManager.GoToState(this, StateDisabled, useTransitions);
            else if (IsPressed)
                VisualStateManager.GoToState(this, StatePressed, useTransitions);
            else if (IsPointerOver)
                VisualStateManager.GoToState(this, StatePointerOver, useTransitions);
            else
                VisualStateManager.GoToState(this, StateNormal, useTransitions);
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsEnabled)
            {
                IsPressed = false;
                IsPointerOver = false;
                _isPointerCaptured = false;
            }

            UpdateVisualStates();
        }

        private void OnGestureRecognizerTapped(GestureRecognizer sender, TappedEventArgs args)
        {
            GestureRecognizerTapped?.Invoke(this, args);
        }

        private void OnGestureRecognizerHolding(GestureRecognizer sender, HoldingEventArgs args)
        {
            GestureRecognizerHolding?.Invoke(this, args);
        }
    }
}
