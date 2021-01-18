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

namespace DesignAndAnimationLab
{
    [TemplateVisualState(Name = StateNormal, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StatePointerOver, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StatePressed, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StateDisabled, GroupName = GroupCommon)]
    public class ProgressButton : RangeBase
    {

        internal const string StateNormal = "Normal";
        internal const string StatePointerOver = "PointerOver";
        internal const string StatePressed = "Pressed";
        internal const string StateDisabled = "Disabled";
        internal const string GroupCommon = "CommonStates";
        internal const string StateFocused = "Focused";


        private bool _isPointerCaptured;


        public ProgressButton()
        {
            DefaultStyleKey = typeof(ProgressButton);
            //_gestureRecognizer.GestureSettings = GestureSettings.HoldWithMouse | GestureSettings.Tap | GestureSettings.Hold;
            IsEnabledChanged += OnIsEnabledChanged;

            //_gestureRecognizer.Holding -= gestureRecognizer_Holding;
            //_gestureRecognizer.Tapped -= gestureRecognizer_Tapped;
            //_gestureRecognizer.Holding += gestureRecognizer_Holding;
            //_gestureRecognizer.Tapped += gestureRecognizer_Tapped;
          

        }

        public bool IsPressed { get; private set; }

        public bool IsPointerOver { get; private set; }

        public event RoutedEventHandler Click;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualState(false);
        }

        internal void UpdateVisualState(bool useTransitions = true)
        {
            if (IsEnabled == false)
                VisualStateManager.GoToState(this, StateDisabled, useTransitions);
            else if (IsPressed)
                VisualStateManager.GoToState(this, StatePressed, useTransitions);
            else if (IsPointerOver)
                VisualStateManager.GoToState(this, StatePointerOver, useTransitions);
            else
                VisualStateManager.GoToState(this, StateNormal, useTransitions);
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
            UpdateVisualState();
            //var ps = e.GetIntermediatePoints(null);
            //if (ps != null && ps.Count > 0)
            //{
            //    _gestureRecognizer.ProcessDownEvent(ps[0]);
            //    e.Handled = true;
            //}
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
            UpdateVisualState();
            //var ps = e.GetIntermediatePoints(null);
            //if (ps != null && ps.Count > 0)
            //{
            //    _gestureRecognizer.ProcessUpEvent(ps[0]);
            //    e.Handled = true;
            //    _gestureRecognizer.CompleteGesture();
            //}
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

            UpdateVisualState();
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            IsPointerOver = true;
            UpdateVisualState();
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            IsPointerOver = false;
            UpdateVisualState();
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsEnabled)
            {
                IsPressed = false;
                IsPointerOver = false;
                _isPointerCaptured = false;
            }
            UpdateVisualState();
        }




        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ProgressButton), new PropertyMetadata(""));




    }
}
