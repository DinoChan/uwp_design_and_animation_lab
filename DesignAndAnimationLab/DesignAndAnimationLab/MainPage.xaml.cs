using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace DesignAndAnimationLab
{
    /// <summary>
    ///     可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private VisualState currentVisualState = VisualState.Unknown;

        public MainPage()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);
        }

        public NavigationHelper NavigationHelper { get; }

        protected override void OnNavigatedFrom(NavigationEventArgs e) => NavigationHelper.OnNavigatedFrom(e);

        protected override void OnNavigatedTo(NavigationEventArgs e) => NavigationHelper.OnNavigatedTo(e);

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var example = (ExampleDefinition)e.ClickedItem;
            Frame.Navigate(typeof(ExamplePage), example);
        }

        // We track the last visual state we set to avoid redundant GoToState calls
        // (these cause flickering on 8.1 apps running on Win10).
        private enum VisualState
        {
            Unknown,
            Big,
            Small,
            Tiny
        }
    }
}