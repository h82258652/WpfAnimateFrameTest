using System;

namespace WpfAnimateFrameTest
{
    public class PageStackEntry
    {
        public PageStackEntry(Type sourcePageType, object parameter)
        {
            SourcePageType = sourcePageType;
            Parameter = parameter;
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