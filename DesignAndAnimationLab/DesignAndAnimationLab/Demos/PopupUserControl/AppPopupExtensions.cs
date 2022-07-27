using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace DesignAndAnimationLab.Demos
{
    public static class AppPopupExtensions
    {
        public static void PopupInit(this IAppPopup popup)
        {
            popup._popup = new Popup();
            popup._popupId = Guid.NewGuid();
            popup._popup.Child = popup as UIElement;
        }

        public static void PopupShow(this IAppPopup popup)
        {
            popup.Width = Window.Current.Bounds.Width;
            popup.Height = Window.Current.Bounds.Height;
            popup._popup.IsOpen = true;
        }
    }
}
