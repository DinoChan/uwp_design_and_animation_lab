using DesignAndAnimationLab.Demos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        private Storyboard _progressStoryboard;
        private bool _isAnimateBegin;
        public BlankPage1()
        {
            this.InitializeComponent();
            _progressStoryboard = CreateStoryboard();
        }

        private void OnLikeButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            if (LikeButton.State != ProgressState.Completed)
                LikeButton.State = ProgressState.Completed;
            else
                LikeButton.State = ProgressState.Idle;
        }



        private void OnGestureRecognizerTapped(object sender, Windows.UI.Input.TappedEventArgs e)
        {
            var progressButton = sender as ProgressButton;
            if (progressButton.State == ProgressState.Idle)
                progressButton.State = ProgressState.Completed;
            else
                progressButton.State = ProgressState.Idle;
        }



        private void OnGestureRecognizerHolding(object sender, Windows.UI.Input.HoldingEventArgs e)
        {
            var progressButton = sender as ProgressButton;
            if (e.HoldingState == HoldingState.Started)
            {
                if (!_isAnimateBegin)
                {
                    _isAnimateBegin = true;
                    (_progressStoryboard.Children[0] as DoubleAnimation).From = progressButton.Minimum;
                    (_progressStoryboard.Children[0] as DoubleAnimation).To = progressButton.Maximum;
                    Storyboard.SetTarget(_progressStoryboard.Children[0] as DoubleAnimation, progressButton);
                    _progressStoryboard.Begin();
                }
            }
            else
            {
                _isAnimateBegin = false;
                _progressStoryboard.Stop();
            }
        }


        private Storyboard CreateStoryboard()
        {
            var animation = new DoubleAnimation
            {
                EnableDependentAnimation = true,
                Duration = TimeSpan.FromSeconds(2)
            };

            Storyboard.SetTargetProperty(animation, nameof(ProgressButton.Value));
            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Completed += OnProgressStoryboardCompleted;
            storyboard.FillBehavior = FillBehavior.Stop;
            return storyboard;
        }

        private async void OnProgressStoryboardCompleted(object sender, object e)
        {
            LikeButton.State = ProgressState.Completed;
            CoinButton.State = ProgressState.Completed;
            FavoriteButton.State = ProgressState.Completed;
            await Task.Delay( TimeSpan.FromSeconds(2));
            var _updatePopup = new UpdatePopup();
            _updatePopup.ShowPopup();

        }
    }
}
