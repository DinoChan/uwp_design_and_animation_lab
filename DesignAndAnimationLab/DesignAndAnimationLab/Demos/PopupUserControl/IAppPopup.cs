using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;


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
