using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DesignAndAnimationLab
{
    public partial class ProgressButton
    {

        /// <summary>
        /// 获取或设置State的值
        /// </summary>  
        public ProgressState State
        {
            get { return (ProgressState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        /// <summary>
        /// 标识 State 依赖属性。
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressState), typeof(ProgressButton), new PropertyMetadata(ProgressState.Idle, OnStateChanged));

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ProgressButton target = obj as ProgressButton;
            ProgressState oldValue = (ProgressState)args.OldValue;
            ProgressState newValue = (ProgressState)args.NewValue;
            if (oldValue != newValue)
                target.OnStateChanged(oldValue, newValue);
        }

        public bool IsPressed { get; private set; }

        public bool IsPointerOver { get; private set; }
    }
}
