using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAnimateFrameTest
{
    public class Frame : ContentControl
    {
        public static readonly DependencyProperty BackStackProperty = DependencyProperty.Register(nameof(BackStack), typeof(IList<PageStackEntry>), typeof(Frame), new PropertyMetadata(default(IList<PageStackEntry>)));

        public static readonly DependencyProperty CanGoBackProperty = DependencyProperty.Register(nameof(CanGoBack), typeof(bool), typeof(Frame), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty CanGoForwardProperty = DependencyProperty.Register(nameof(CanGoForward), typeof(bool), typeof(Frame), new PropertyMetadata(default(bool)));

        private const string RootHostTemplateName = "PART_RootHost";

        private readonly Grid _pageHost = new Grid();

        private List<PageStackEntry> TestBackStack
        {
            // TODO
            get;
        } = new List<PageStackEntry>();

        static Frame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Frame), new FrameworkPropertyMetadata(typeof(Frame)));
        }

        public IList<PageStackEntry> BackStack
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CanGoBack
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CanGoForward
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void GoForward()
        {
            throw new NotImplementedException();
        }

        public bool Navigate(Type sourcePageType, object parameter)
        {
            var currentPage = GetCurrentPage();

            NavigateInternal(sourcePageType, parameter, NavigationMode.New);

            if (currentPage != null)
            {
                // TODO parameter is current page parameter.
                TestBackStack.Add(new PageStackEntry(currentPage.GetType(), null));
            }

            return true;
        }

        public bool Navigate(Type sourcePageType)
        {
            return Navigate(sourcePageType, null);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var rootHost = (Border)GetTemplateChild(RootHostTemplateName);
            Debug.Assert(rootHost != null);
            rootHost.Child = _pageHost;
        }

        private Page GetCurrentPage()
        {
            var pages = _pageHost.Children;
            var count = pages.Count;
            if (count > 0)
            {
                return (Page)_pageHost.Children[count - 1];
            }
            return null;
        }

        private void NavigateInternal(Type sourcePageType, object parameter, NavigationMode navigationMode)
        {
            if (typeof(Page).IsAssignableFrom(sourcePageType) == false)
            {
                throw new NotSupportedException();
            }

            var newPage = (Page)Activator.CreateInstance(sourcePageType);
            newPage.Frame = this;
            var currentPage = GetCurrentPage();
            if (currentPage != null)
            {
                currentPage.OnNavigatedFrom(new NavigationEventArgs());
                Action asyncAction = async () =>
                {
                    await currentPage.PlayLeaveAnimationAsync();
                    _pageHost.Children.Remove(currentPage);
                    currentPage.Frame = this;
                };
                asyncAction.Invoke();
            }
            _pageHost.Children.Insert(0, newPage);
            newPage.OnNavigatedTo(new NavigationEventArgs());
        }
    }
}