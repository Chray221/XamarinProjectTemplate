using System;
using Xamarin.Forms.Internals;

namespace XamProjectTemplate.DependencyServices
{
    [Preserve(AllMembers = true)]
    public interface IChangeBarColor
    {
        void ChangeColor(BarStyle barStyle);
    }

    [Preserve(AllMembers = true)]
    public enum BarStyle
    {
        Light,
        Dark
    }
}
