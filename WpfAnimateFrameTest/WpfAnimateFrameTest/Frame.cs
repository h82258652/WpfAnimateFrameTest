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

        public IList<PageStackEntry> ForwardStack
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
                return (bool)GetValue(CanGoBackProperty);
            }
            private set
            {
                SetValue(CanGoBackProperty, value);
            }
        }

        public bool CanGoForward
        {
            get
            {
                return (bool)GetValue(CanGoForwardProperty);
            }
            private set
            {
                SetValue(CanGoForwardProperty, value);
            }
        }

        private List<PageStackEntry> TestBackStack
        {
            // TODO remove it
            get;
        } = new List<PageStackEntry>();

        private int _index;

        public void GoBack()
        {
            if (CanGoBack == false)
            {
                // TODO 确认异常类型。
                throw new NotImplementedException();
            }

            var pageStackEntry = TestBackStack[_index];
            var sourcePageType = pageStackEntry.SourcePageType;
            var parameter = pageStackEntry.Parameter;

            NavigateInternal(sourcePageType, parameter, NavigationMode.Back);

            _index--;
        }

        public void GoForward()
        {
            if (CanGoForward == false)
            {
                // TODO 确认异常类型。
                throw new NotImplementedException();
            }

            var pageStackEntry = TestBackStack[_index];
            var sourcePageType = pageStackEntry.SourcePageType;
            var parameter = pageStackEntry.Parameter;

            NavigateInternal(sourcePageType, parameter, NavigationMode.Forward);

            _index++;
        }

        public bool Navigate(Type sourcePageType, object parameter)
        {
            var currentPage = GetCurrentPage();

            NavigateInternal(sourcePageType, parameter, NavigationMode.New);

            if (currentPage != null)
            {
                TestBackStack.Insert(_index, new PageStackEntry(currentPage.GetType(), currentPage._parameter));
                TestBackStack.RemoveRange(_index, 999);
                TestBackStack.Add(new PageStackEntry(currentPage.GetType(), currentPage._parameter));
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
            newPage._parameter = parameter;
            var currentPage = GetCurrentPage();
            if (currentPage != null)
            {
                currentPage.OnNavigatedFrom(new NavigationEventArgs(sourcePageType, navigationMode, parameter));
                Action asyncAction = async () =>
                {
                    await currentPage.PlayLeaveAnimationAsync();
                    _pageHost.Children.Remove(currentPage);
                    currentPage.Frame = this;
                };
                asyncAction.Invoke();
            }
            _pageHost.Children.Insert(0, newPage);
            newPage.OnNavigatedTo(new NavigationEventArgs(sourcePageType, navigationMode, parameter));
        }
    }
}