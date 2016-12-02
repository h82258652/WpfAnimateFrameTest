using System;
using System.Windows;

namespace WpfAnimateFrameTest
{
    public sealed class PageStackEntry : DependencyObject
    {
        public static readonly DependencyProperty SourcePageTypeProperty = DependencyProperty.Register(nameof(SourcePageType), typeof(Type), typeof(PageStackEntry), new PropertyMetadata(default(Type)));

        public PageStackEntry(Type sourcePageType, object parameter)
        {
            if (sourcePageType == null)
            {
                throw new ArgumentNullException(nameof(sourcePageType));
            }

            SourcePageType = sourcePageType;
            Parameter = parameter;
        }

        public object Parameter
        {
            get;
        }

        public Type SourcePageType
        {
            get
            {
                return (Type)GetValue(SourcePageTypeProperty);
            }
            private set
            {
                SetValue(SourcePageTypeProperty, value);
            }
        }
    }
}