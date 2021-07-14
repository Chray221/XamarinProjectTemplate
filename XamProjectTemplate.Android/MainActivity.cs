using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Graphics;
using Android.Views;
using Android.Content;
using Java.Security;
using Android.Content.Res;

namespace XamProjectTemplate.Droid
{
    [Activity(Label = "XamProjectTemplate", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ViewTreeObserver.IOnGlobalLayoutListener
    {
        // use for entry shift up when keyboard shows
        public static Action GlobalLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            StaticFontSize();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            #region Scale Helper

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat) // Extend layout beyond statusbar and navigationbar
            {
                var newUiOptions = 0;

                newUiOptions |= (int)SystemUiFlags.LayoutStable;
                newUiOptions |= (int)SystemUiFlags.LayoutFullscreen;
                newUiOptions |= (int)SystemUiFlags.HideNavigation;
                newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
            }

            if (Build.VERSION.SdkInt == BuildVersionCodes.Lollipop) // Don't set statusbar to transparent since we can't set statusbar text color
            {
                Window.SetStatusBarColor(Color.Black);
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                SetWindowFlag(WindowManagerFlags.TranslucentStatus, false);
                Window.SetStatusBarColor(Color.Transparent);
            }

            var density = Resources.DisplayMetrics.Density;
            int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                App.StatusBarHeight = Resources.GetDimensionPixelSize(resourceId) / density;
            }

            //App.SystemVersion = Build.VERSION.Sdk;
            #endregion
            base.OnCreate(savedInstanceState);

            // init ffimageloading plugin
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            //var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);

            // init rg popup plugin
            global::Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //init cross current activity plugin
            //CrossCurrentActivity.Current.Init(this, savedInstanceState);

            // Init xam maps plugin
            //Xamarin.FormsMaps.Init(this,savedInstanceState);

            // init Shadow Plugin
            //_ = new Sharpnado.Shades.Droid.AndroidShadowsRenderer(this);

            // init mapbox plugin
            //Mapbox.GetInstance(this, "pk.eyJ1IjoiY2hyaXN0aWFuMjIxIiwiYSI6ImNra2M5cGc4ZDBtYWEyb294eXBiZXgweWMifQ.eIPoI4WkZUv45N8J4qGDmg");
            //Com.Mapbox.Mapboxsdk.Mapbox.Telemetry.SetDebugLoggingEnabled(true);

            LoadApplication(new App());
#if DEBUG
            //print hash keys
            //PrintHashKey(this);
#endif
            //FirebasePushNotificationManager.ProcessIntent(this, Intent);

            //init local notification
            //CreateNotificationFromIntent(Intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        public static void PrintHashKey(Context pContext)
        {
            try
            {
                PackageInfo info = Android.App.Application.Context.PackageManager.GetPackageInfo(Android.App.Application.Context.PackageName, PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA1");
                    md.Update(signature.ToByteArray());
                    App.Log($"SHA1 {Convert.ToBase64String(md.Digest())}");
                    App.Log($"SHA1 {signature.ToString()}");
                }
            }
            catch (NoSuchAlgorithmException e)
            {
                App.Log($"StackTrace: {e.StackTrace}\nMESSAGE: {e.Message}");
            }
            catch (Exception e)
            {
                App.Log($"StackTrace: {e.StackTrace}\nMESSAGE: {e.Message}");
            }
        }

        [Obsolete]
        void StaticFontSize()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)1;
            Android.Util.DisplayMetrics metrics = new Android.Util.DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            metrics.ScaledDensity = configuration.FontScale * metrics.Density;
            BaseContext.Resources.UpdateConfiguration(configuration, metrics);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        public void SetWindowFlag(WindowManagerFlags bits, bool on)
        {
            var windows = Window;
            var attrib = windows.Attributes;
            if (on)
                attrib.Flags |= bits;
            else
                attrib.Flags &= ~bits;

            windows.Attributes = attrib;
        }

        // Android JellyBeanMr1 and Up Only API 17
        [Obsolete]
        public void GetScreenSize(ref Point realSize, ref Point usableSize)
        {
            IWindowManager windowManager = Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            Display display = windowManager.DefaultDisplay;

            display.GetRealSize(realSize);
            display.GetSize(usableSize);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public override void SetContentView(View view)
        {
            base.SetContentView(view);
            view.ViewTreeObserver.AddOnGlobalLayoutListener(this);
        }

        public void OnGlobalLayout()
        {
            GlobalLayout?.Invoke();
            //ScreenFullscreen();
        }

        void ScreenFullscreen()
        {
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
        }

    }
}