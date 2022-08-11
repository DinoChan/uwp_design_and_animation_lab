using System;
using Windows.UI.Xaml.Controls.Primitives;

namespace DesignAndAnimationLab.Demos
{
    public interface IAppPopup
    {
        Popup _popup { get; set; }
        Guid _popupId { get; set; }
        double Height { get; set; }

        double Width { get; set; }

        void HidePopup();

        void ShowPopup();
    }
}