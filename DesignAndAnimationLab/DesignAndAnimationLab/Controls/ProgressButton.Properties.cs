using Windows.UI.Xaml;

namespace DesignAndAnimationLab
{
    public partial class ProgressButton
    {
        /// <summary>
        /// 标识 Content 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(ProgressButton), new PropertyMetadata(default(object)));

        /// <summary>
        /// 标识 State 依赖属性。
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressState), typeof(ProgressButton), new PropertyMetadata(ProgressState.Idle, OnStateChanged));

        /// <summary>
        /// 获取或设置Content的值
        /// </summary>
        public object Content
        {
            get => (object)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public bool IsPointerOver { get; private set; }

        public bool IsPressed { get; private set; }

        /// <summary>
        /// 获取或设置State的值
        /// </summary>
        public ProgressState State
        {
            get { return (ProgressState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ProgressButton target = obj as ProgressButton;
            ProgressState oldValue = (ProgressState)args.OldValue;
            ProgressState newValue = (ProgressState)args.NewValue;
            if (oldValue != newValue)
                target.OnStateChanged(oldValue, newValue);
        }
    }
}