using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
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

        public ProgressButton()
        {
            DefaultStyleKey = typeof(ProgressButton);
            IsEnabledChanged += OnIsEnabledChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualStates(false);
        }

        protected virtual void OnStateChanged(ProgressState oldValue, ProgressState newValue)
        {
            UpdateVisualStates(true);
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
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
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
            string progressState;
            switch (State)
            {
                case ProgressState.Idle:
                    progressState = IdleStateName;
                    break;
                case ProgressState.InProgress:
                    progressState = InProgressStateName;
                    break;
                case ProgressState.Completed:
                    progressState = CompletedStateName;
                    break;
                case ProgressState.Faulted:
                    progressState = FaultedStateName;
                    break;
                default:
                    progressState = IdleStateName;
                    break;
            }

            VisualStateManager.GoToState(this, progressState, useTransitions);

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
    }
}
