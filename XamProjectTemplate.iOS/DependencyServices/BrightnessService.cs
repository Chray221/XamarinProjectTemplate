using System;
using XamProjectTemplate.iOS;
using UIKit;
using Xamarin.Forms;
using XamProjectTemplate.DependencyServices;

[assembly: Dependency(typeof(BrightnessService))]
namespace XamProjectTemplate.iOS
{
    public class BrightnessService : IBrightnessService
    {
        static nfloat LastBrightness = 0.5f;
        static bool IsBrightnessAdjusted = false;

        public void ResetBrightness()
        {
            if (IsBrightnessAdjusted)
            {
                IsBrightnessAdjusted = false;
                UIScreen.MainScreen.Brightness = LastBrightness;
            }
        }

        public void SetBrightness(float brightness)
        {
            LastBrightness = UIScreen.MainScreen.Brightness;
            UIScreen.MainScreen.Brightness = brightness;
            IsBrightnessAdjusted = true;
        }
    }
}
