using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace XamProjectTemplate.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            #region Scale Helper Init
            App.StatusBarHeight = (float)UIApplication.SharedApplication.StatusBarFrame.Size.Height;

            App.TopInsets = 0.0f;
            App.BottomInsets = 0.0f;

            UIWindow w = new UIWindow();
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                if (w.SafeAreaInsets.Top > 0 && w.SafeAreaInsets.Bottom > 0)
                {
                    App.IsAddNavHeight = true;
                    App.TopInsets = (float)w.SafeAreaInsets.Top;
                    App.BottomInsets = (float)w.SafeAreaInsets.Bottom;
                }
            }

            App.SystemVersion = UIDevice.CurrentDevice.SystemVersion;
            #endregion

            //Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();

            // ini ffimageloading plugin
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            //var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);

            // init shadow plugin
            //Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();

            // init mapbox plugin
            //Mapbox.MGLAccountManager.AccessToken = "pk.eyJ1IjoiY2hyaXN0aWFuMjIxIiwiYSI6ImNra2M5cGc4ZDBtYWEyb294eXBiZXgweWMifQ.eIPoI4WkZUv45N8J4qGDmg";
            //new Naxam.Controls.Mapbox.Platform.iOS.MapViewRenderer();

            // default LoadApplication
            //LoadApplication(new App());

            // prism LoadApplication
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
