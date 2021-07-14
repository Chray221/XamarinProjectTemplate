using System;
using Android.App;
using Android.OS;
using Android.Views;
using XamProjectTemplate.Droid.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamProjectTemplate.DependencyServices;

[assembly: Dependency(typeof(ChangeBarColor))]
namespace XamProjectTemplate.Droid.DependencyServices
{
    public class ChangeBarColor : IChangeBarColor
    {
        [Obsolete]
        public void ChangeColor(BarStyle barStyle)
        {
            if(Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var context = Xamarin.Essentials.Platform.CurrentActivity ?? Xamarin.Essentials.Platform.AppContext;
                if (context != null)
                {
                    var windows = ((Activity)context).Window;
                    var flags = (int)windows.DecorView.SystemUiVisibility;
                    switch (barStyle)
                    {
                        case BarStyle.Light: // Set text to white
                            flags &= ~(int)SystemUiFlags.LightStatusBar;
                            break;
                        case BarStyle.Dark: // Set text to black
                            flags |= (int)SystemUiFlags.LightStatusBar;
                            break;
                    }
                    //windows.SetFlags(flags)
                    windows.DecorView.SystemUiVisibility = (StatusBarVisibility)flags;
                }
                else
                {
                    App.Log("Unable to detect current Activity or App Context. Please ensure Xamarin.Essentials is installed in your Android project initialized.");
                }
            }
        }
    }
}
