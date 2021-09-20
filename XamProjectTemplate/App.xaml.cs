using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamProjectTemplate.Views;

namespace XamProjectTemplate
{
    public partial class App : PrismApplication
    {
        public static float ScreenWidth { get; set; }
        public static float ScreenHeight { get; set; }
        public static float OriginalHeight { get; set; }
        public static float AdjustedHeight { get; set; }
        public static float OriginalWidth { get; set; }
        public static float AdjustedWidth { get; set; }
        public static float AndroidHeightPixels { get; set; }
        public static float AppScale { get; set; }
        public static int DeviceType { get; set; }
        public static string UDID { get; set; } // firebase token
        public static bool IsPhone { get; set; }
        public static float StatusBarHeight { get; set; }
        /// <summary>
        /// Uses for iOS only
        /// </summary>
        public static float TopInsets { get; set; }
        /// <summary>
        /// Uses for iOS only
        /// </summary>
        public static float BottomInsets { get; set; }
        public static string SystemVersion { get; set; }
        /// <summary>
        /// Uses for iOS only
        /// </summary>
        public static bool IsAddNavHeight { get; set; }
        public static string SMSHash { get; set; } = string.Empty;

        public static Action OnAppResume { get; set; }
        public static Action OnAppSleep { get; set; }

        public static string AppScheme { get; } = "XamProjectTemplate://";
        public static string AppRootRoute { get; } = "XamProjectTemplate.ph/";
        public static string AppNavigationRootRoute { get { return $"{AppScheme}{AppRootRoute}"; } }

        public App() : this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        static INavigationService _NavigationService;
        //readonly DataClass dataClass = DataClass.GetInstance;
        protected override void OnInitialized()
        {
            SetUpScreenSize();
            SetupGlobalOptions();
            InitializeComponent();
            //Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            _NavigationService = NavigationService;
            Xamarin.Forms.Device.SetFlags(new string[] { "Shapes_Experimental", "RadioButton_Experimental", "Brush_Experimental", "SwipeView_Experimental" });
            

            //if (DataClass.GetInstance.IsLoggedIn)
            //    ToHomePage();
            //else
            //    ToMainPage();

            ForTesting();

        }

        async void ForTesting()
        {
            //var result = await NavigationService?.NavigateAsync($"{nameof(NavigationPage)}/{nameof(CalculatorPage)}");
            var result = await NavigationService?.NavigateAsync($"{nameof(NavigationPage)}/{nameof(XamProjectTemplate.MainPage)}");
            //var result = await NavigationService?.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MyFlyoutMenuPage)}");
            if (!result.Success)
            {
                App.LogException(result.Exception);
            }

            //var ViewModel = new LabelPageViewModel(NavigationService,new PageDialogService(new ApplicationProvider()));
            //MainPage = new NavigationPage(new LabelPage() { BindingContext = ViewModel});
        }

        protected override void OnStart()
        {
#if DEBUG
            Microsoft.AppCenter.AppCenter.Start(Constants.APP_CENTER_KEY, typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Crashes));
            Crashes.GetErrorAttachments += Crashes_GetErrorAttachments;
#endif
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async Task ToHomePage()
        {
            //TODO
            var result = await _NavigationService.NavigateAsync($"{AppNavigationRootRoute}{nameof(MainPage)}");
            if (!result.Success)
            {
                App.LogException(result.Exception);
            }
        }

        public static async void ToMainPage()
        {
            var result = await _NavigationService?.NavigateAsync($"{AppNavigationRootRoute}{nameof(NavigationPage)}/{nameof(XamProjectTemplate.MainPage)}");
            if (!result.Success)
            {
                App.LogException(result.Exception);
            }
        }

        public static async void Logout()
        {
            await DataClass.GetInstance.Logout();
            ToMainPage();
        }

        private IEnumerable<ErrorAttachmentLog> Crashes_GetErrorAttachments(ErrorReport report)
        {
            var UserId = DataClass.GetInstance.User.Id;
            var IsLoggedIn = string.IsNullOrEmpty(DataClass.GetInstance.Token) == false;

            return new ErrorAttachmentLog[]
            {
                    ErrorAttachmentLog.AttachmentWithText("UserId: " + UserId.ToString(), "txt.txt"),
                    ErrorAttachmentLog.AttachmentWithText("IsLoggedIn: " + IsLoggedIn.ToString(), "txt.txt"),
                    ErrorAttachmentLog.AttachmentWithText("URL: " + Constants.URL, "txt.txt")
            };

            //return null;
        }

        void SetUpScreenSize()
        {
            DisplayInfo displayInfo = DeviceDisplay.MainDisplayInfo;
            App.AppScale = (float)displayInfo.Density;
            App.ScreenHeight = (float)(displayInfo.Height / AppScale);
            App.ScreenWidth = (float)(displayInfo.Width / AppScale);
            App.DeviceType = Device.RuntimePlatform == Device.Android ? 1 : 0;
            App.OriginalHeight = App.ScreenHeight;
            App.OriginalWidth = App.ScreenWidth;
            App.IsPhone = Device.Idiom == TargetIdiom.Phone;
            switch (Xamarin.Forms.Device.Idiom)
            {
                case TargetIdiom.Phone:
                    App.ScreenHeight = (16 * App.ScreenWidth) / 9;
                    App.AdjustedHeight = App.ScreenHeight;
                    //App.IsPhone = true;

                    // But wait! If device has navigationbar? and screen ratio is already 16:9
                    if ((int)App.OriginalHeight <= (int)App.ScreenHeight)
                    {
                        App.ScreenHeight = (float)(displayInfo.Height / displayInfo.Density);
                        App.ScreenWidth = (9 * App.ScreenHeight) / 16;
                        App.AdjustedHeight = App.ScreenHeight;
                    }
                    break;
                case TargetIdiom.Tablet:
                    App.ScreenWidth = (9 * App.ScreenHeight) / 16;
                    App.AdjustedWidth = App.ScreenWidth;
                    //App.IsPhone = false;
                    break;
                default:
                    App.ScreenWidth = (9 * App.ScreenHeight) / 16;
                    App.AdjustedWidth = App.ScreenWidth;
                    //App.IsPhone = false;
                    break;
            }
        }

        public void SetupGlobalOptions()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }
            };
        }

        public static void Log(string msg, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            msg = DateTime.Now.ToString("HH:mm:ss:ff tt") + " [XamProjectTemplate]-[" + memberName + "]: " + msg;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(msg);
#elif RELEASE

#else
            Console.WriteLine(msg);
#endif
        }

        public static void LogException(Exception msg, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            string message = $"{DateTime.Now:HH:mm:ss:ff tt} [{typeof(App).Namespace}]-[Exception]-[{memberName}]: TITLE:{msg.GetType()} \n\tMESSAGE: {msg.Message} \n\tSTACKTRACE: {msg.StackTrace}";
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#elif RELEASE

#else
            Console.WriteLine(message);
#endif
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<CalculatorPage>();
        }
    }
}
