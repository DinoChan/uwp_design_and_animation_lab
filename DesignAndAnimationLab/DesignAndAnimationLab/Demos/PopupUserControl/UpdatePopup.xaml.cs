using System;
using System.Collections.Generic;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos
{
    public sealed partial class UpdatePopup : UserControl, IAppPopup
    {
        public Popup _popup { get; set; }
        public Guid _popupId { get; set; }
        string version = "";
        public UpdatePopup()
        {
            this.InitializeComponent();
            this.PopupInit();
        }
        public void ShowPopup()
        {
            this.PopupShow();
            PopupIn.Begin();
        }
        public void HidePopup()
        {
            PopupOut.Begin();
            PopupOut.Completed -= PopupOut_Completed;
            PopupOut.Completed += PopupOut_Completed;
        }
       


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            HidePopup();
        }
        private void PopupOut_Completed(object sender, object e)
        {

            _popup.IsOpen = false;
        }
    }
}
