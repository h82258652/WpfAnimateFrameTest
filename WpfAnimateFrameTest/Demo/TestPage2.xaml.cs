using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Demo
{
    public partial class TestPage2
    {
        public TestPage2()
        {
            InitializeComponent();
        }

        protected override async Task PlayEnterAnimationAsync()
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation()
            {
                From = ActualWidth,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            Storyboard.SetTarget(animation, RootGrid);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            storyboard.Children.Add(animation);
            await storyboard.BeginAsync();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}