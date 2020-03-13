using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DesignAndAnimationLab.Demos.GooeyButtonDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GooeyButtonDemoPage : Page
    {
        public GooeyButtonDemoPage()
        {
            this.InitializeComponent();
            strings = new ObservableCollection<Symbol>()
            {
                Symbol.AddFriend,
                Symbol.Forward,
                Symbol.Share
            };
        }

        ObservableCollection<Symbol> strings { get; set; }
        Random rnd = new Random();

        private void gooeyButton_Invoked(object sender, GooeyButton.GooeyButtonInvokedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Invoked");
        }

        private void gooeyButton_ItemInvoked(object sender, GooeyButton.GooeyButtonItemInvokedEventArgs args)
        {
            if (args.Item is Symbol symbol)
            {
                if (symbol == Symbol.AddFriend)
                {
                    if (strings.Count == 3)
                    {
                        strings.Add(Symbol.Home);
                        gooeyButton.Distance += 20;
                    }
                    else
                    {
                        strings.RemoveAt(3);
                        gooeyButton.Distance -= 20;
                    }
                }
                else if (symbol == Symbol.Forward)
                {
                    var pos = (int)(gooeyButton.ItemsPosition) + 1;
                    if (pos == 4) pos = 0;
                    gooeyButton.ItemsPosition = (GooeyButtonItemsPosition)pos;
                }
                else if (symbol == Symbol.Share)
                {
                    gooeyButton.Distance = rnd.Next(80, 300);
                }
            }

            System.Diagnostics.Debug.WriteLine(args.Item.ToString());
        }
    }
}
