using System;
using System.Windows.Navigation;

namespace WpfAnimateFrameTest
{
    public sealed class NavigationEventArgs
    {
        internal NavigationEventArgs(Type sourcePageType, NavigationMode navigationMode, object parameter)
        {
            if (sourcePageType == null)
            {
                throw new ArgumentNullException(nameof(sourcePageType));
            }

            SourcePageType = sourcePageType;
            NavigationMode = navigationMode;
            Parameter = parameter;
        }

        public NavigationMode NavigationMode
        {
            get;
        }

        public object Parameter
        {
            get;
        }

        public Type SourcePageType
        {
            get;
        }
    }
}