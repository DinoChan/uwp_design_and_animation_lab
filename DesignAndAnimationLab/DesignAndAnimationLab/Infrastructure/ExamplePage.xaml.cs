// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DesignAndAnimationLab
{
    public sealed partial class ExamplePage : Page
    {
        public ExamplePage()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);

            if (DesignMode.DesignModeEnabled)
            {
                DataContext = new ExampleDefinition("An Example", null);
            }

            if (NavigationHelper.HasHardwareButtons)
            {
                backButton.Visibility = Visibility.Collapsed;
            }
        }

        public NavigationHelper NavigationHelper { get; }

        protected override void OnNavigatedFrom(NavigationEventArgs e) => NavigationHelper.OnNavigatedFrom(e);

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationHelper.OnNavigatedTo(e);

            var example = e.Parameter as ExampleDefinition;
            if (example != null)
            {
                DataContext = example;
                if (example.Control != null)
                {
                    var control = Activator.CreateInstance(example.Control) as FrameworkElement;
                    RequestedTheme = control.RequestedTheme;
                    exampleContent.Children.Add(control);
                }
            }
        }
    }
}