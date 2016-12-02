using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Demo
{
    public partial class TestPage1
    {
        public TestPage1()
        {
            InitializeComponent();
        }

        protected override async Task PlayLeaveAnimationAsync()
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = 0 - ActualWidth,
                Duration = TimeSpan.FromSeconds(1)
            };
            Storyboard.SetTarget(animation, RootGrid);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            storyboard.Children.Add(animation);
            await storyboard.BeginAsync();
        }

        private void NavigateToPage2Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TestPage2));
        }
    }
}