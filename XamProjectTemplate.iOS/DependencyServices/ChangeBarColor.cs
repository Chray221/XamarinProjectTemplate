using System;
using XamProjectTemplate.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;
using XamProjectTemplate.DependencyServices;

[assembly: Dependency(typeof(ChangeBarColor))]
namespace XamProjectTemplate.iOS.DependencyServices
{
    public class ChangeBarColor : IChangeBarColor
    {
        public void ChangeColor(BarStyle barStyle)
        {
            switch (barStyle)
            {
                case BarStyle.Light:
                    (UIApplication.SharedApplication).SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
                    break;
                case BarStyle.Dark:
                    (UIApplication.SharedApplication).SetStatusBarStyle(UIStatusBarStyle.Default, false);
                    break;
            }
        }
    }
}
