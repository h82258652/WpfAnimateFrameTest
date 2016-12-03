using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfAnimateFrameTest
{
    public class Page : UserControl
    {
        public static readonly DependencyProperty FrameProperty = DependencyProperty.Register(nameof(Frame), typeof(Frame), typeof(Page), new PropertyMetadata(default(Frame)));

        internal object _parameter;

        public Page()
        {
            Loaded += Page_Loaded;
        }

        public Frame Frame
        {
            get
            {
                return (Frame)GetValue(FrameProperty);
            }
            internal set
            {
                SetValue(FrameProperty, value);
            }
        }

        protected internal virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        protected internal virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        protected internal virtual Task PlayLeaveAnimationAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task PlayEnterAnimationAsync()
        {
            return Task.CompletedTask;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PlayEnterAnimationAsync();
        }
    }
}