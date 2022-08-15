using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace DesignAndAnimationLab.Demos
{
    public sealed partial class UpdatePopup : UserControl, IAppPopup
    {
        private string version = "";

        public UpdatePopup()
        {
            InitializeComponent();
            this.PopupInit();
        }

        public Popup _popup { get; set; }
        public Guid _popupId { get; set; }

        public void HidePopup()
        {
            PopupOut.Begin();
            PopupOut.Completed -= PopupOut_Completed;
            PopupOut.Completed += PopupOut_Completed;
        }

        public void ShowPopup()
        {
            this.PopupShow();
            PopupIn.Begin();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => HidePopup();

        private void PopupOut_Completed(object sender, object e) => _popup.IsOpen = false;
    }
}