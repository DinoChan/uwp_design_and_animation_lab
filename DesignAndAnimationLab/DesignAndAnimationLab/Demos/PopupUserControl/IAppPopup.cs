using System;
using Windows.UI.Xaml.Controls.Primitives;

namespace DesignAndAnimationLab.Demos
{
    public interface IAppPopup
    {
        Guid _popupId { get; set; }
        Popup _popup { get; set; }

        void ShowPopup();

        void HidePopup();

        double Width { get; set; }
        double Height { get; set; }
    }
}
