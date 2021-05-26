using System;
using XamProjectTemplate;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamProjectTemplate
{
    public static class Constants
    {
        //App Link
        public const string DATA_SCHEME = "XamProjectTemplate";
        public const string DATA_HOST = "apps.XamProjectTemplate.com";
        public const string DATA_PATH_PREFIX= "/";
        public const string APP_LINK = DATA_SCHEME+"://"+ DATA_HOST+ DATA_PATH_PREFIX;


        public static readonly string VERSION_FALLBACK_URL = "http://demo2630086.mockable.io/XamProjectTemplate/mobile_version";
        public static readonly string SERVER_LIST_URL = "http://demo2697298.mockable.io/XamProjectTemplate/server_list";
        //public static readonly string MOBILE_VERSION_URL = "/assets/mobile_version.json";
        public static readonly string MOBILE_VERSION_URL = "http://demo2697298.mockable.io/XamProjectTemplate/mobile_version";

        public static readonly string NO_INTERNET_TITLE = "No Internet Connection!";
        public static readonly string NO_INTERNET_CONTENT = "Please check your internet connection, and try again!";
        public static readonly string BAD_GATEWAY_CONTENT = "Something went wrong. Please check on us later.";

        public static readonly string BASE64_PASSKEY = "c8bpe14ac22s01csa43q0fcuhd4ag78rh4laidrn6k9tytfie2rn0e1ef2ch1bdebbdr2c16oc5d8d0s8d394seae80d561k889a7ed5a2uod8t5c4e";
        public static readonly string SYNCFUSION_LICENSE = "MjI3OTg5QDMxMzcyZTM0MmUzMEdObHVjMFFZS0dzWDZudWxieDB2TnFlcWlMS3ZhWFFwa29SYXZZeVM3dkk9";
        //SYNCFUSION_LICENSE 2
        //NDAwOTJAMzEzNjJlMzMyZTMwam9pVHdLWGYxbjd5QmZSaGswVUhmeFBHWTVtVFpXSXM2eVJqVzFUY0lLdz0
        public static readonly string APP_CENTER_KEY = "ios=01980a50-4a14-45e3-a8c7-a1326e820a05;" +
                                                       "android=d40ef8ee-07fd-4807-b4d2-85a853b97be5";
        //DIRECTION API KEY
        public static readonly string GOOGLE_DIRECTION_KEY = "AIzaSyAYCoq6N_OCaWSfb1-p7VkcJSN1y1YSitE";

        //VALUES
        public static readonly double NAVIGATION_HEIGHT = 32.ScaleHeight();
        public static readonly double STATUS_BAR_HEIGHT = App.StatusBarHeight;
        public static Thickness STATUSBAR_PADDING => new Thickness(0, App.StatusBarHeight, 0, 0);
        public static Thickness NEG_STATUSBAR_PADDING => new Thickness(0, -App.StatusBarHeight, 0, 0);
        public static Thickness BOTTOM_PADDING => new Thickness(0, 0, 0, App.BottomInsets);

        public static readonly double BORDER_HEIGHT = .5D.ScaleHeight();

        public static readonly Thickness LOGO_MARGIN = new Thickness(0, App.IsAddNavHeight ? 65.ScaleHeight() : 47.ScaleHeight(), 0, 0);
        public static readonly Thickness NAV_TITLE_MARGIN = App.IsAddNavHeight ? new Thickness(0, 41.ScaleHeight(), 0, 0) : new Thickness(0, 17.ScaleHeight(), 0, 0);

#region GOOGLE OAUTH API
        public static readonly string SCOPE_URL = "https://www.googleapis.com/auth/userinfo.email";
        public static readonly string AUTHORIZE_URL = "https://accounts.google.com/o/oauth2/auth";
        public static readonly string ANDROID_REDIRECT_URL = "ks://accounts.google.com";
        public static readonly string TOKEN_URL = "https://accounts.google.com/o/oauth2/token";

        public static readonly string ANDROID_CLIENTID = "https://accounts.google.com/o/oauth2/token";
        public static readonly string iOS_CLIENTID = "https://accounts.google.com/o/oauth2/token";



        #endregion
#if DEBUG == false
        /// <summary> /users </summary>
        public static readonly string USERS_URL = "/users";

        /// <summary>
        /// add '?' if first parameter or '&amp;' when not first paramameter
        /// <para>EXAMPLE
        ///     <code>"samplesite.com/fetch_api?access_token=asdadasdad123123123"</code>
        ///     <code>"samplesite.com/fetch_api?code=1234&amp;access_token=asdadasdad123123123"</code>
        /// </para>
        /// <para>RETURN:
        ///     <code>"access_token=aslkjzxlvkaksuo123aszx"</code>
        /// </para>
        /// </summary>
        public static string ACCESS_TOKEN { get { return $"access_token={DataClass.GetInstance.Token}"; } }

#endif

        //#region OSM API
        //public static readonly string OSM_URL = "http://167.71.207.11";
        //public static readonly string OSM_ROOT_URL = OSM_URL + "/api/v1";
        //public static readonly string OSM_USERNAME = "appstone";
        //public static readonly string OSM_XamProjectTemplateWORD = "EG6f!zaN@aYI|OGwxwerFfR&&C8$oM";
        //public static readonly string PLACES_URL = "/places";
        //public static readonly string REVERSE_GEOCODE_URL = "/reverse_geocode";
        //public static readonly string PLACES_SEARCH_URL = "/search";
        //#endregion


        public static readonly string XamProjectTemplate_TERMS_URL = "/terms/XamProjectTemplateenger";
        public static readonly string XamProjectTemplate_PRIVACY_URL = "/privacy";
        public static readonly string TERMS_URL_PRODUCTION = URL + XamProjectTemplate_TERMS_URL;
        public static readonly string PRIVACY_URL_PRODUCTION = URL + XamProjectTemplate_PRIVACY_URL;

        public static readonly string VERSION_NUMBER = $"V.{AppInfo.VersionString}.{AppInfo.BuildString}";

        #region Server Setting

        public static string DEFAULT_AUTH { get { return $"{{ \"client\": \"{DataClass.GetInstance.ClientId ?? ""}\", \"uid\": \"{DataClass.GetInstance.Uid ?? ""}\",\"access-token\": \"{DataClass.GetInstance.Token ?? ""}\" }}"; } }

        // Production Server
        public static readonly string SERVER_NAME = "Production Server";
        public static readonly bool IS_SERVER_SECURE = false;
        public static readonly string URL_STABLE = "https://apps.XamProjectTemplate.ph";
        public static readonly string URL_FAYE_NOTIFICATION = "ws://apps.XamProjectTemplate.ph:9292/faye";

        //Running Server
        public static string RUNNING_SERVER_NAME = SERVER_NAME;
        public static string URL = URL_STABLE;
        public static string FAYE_NOTIFICATION_URL = URL_FAYE_NOTIFICATION;
        public static string ROOT_URL { get { return URL + "/v1"; } }
        public static string ROOT_URL_V2 { get { return URL + "/v2"; } }
        public static string ROOT_URL_V3 { get { return URL + "/v3"; } }
        public static string ROOT_API_URL { get { return URL + "/api" + "/v1"; } }
        public static string ROOT_API_V2_URL { get { return URL + "/api" + "/v2"; } }
        //public static string SIGN_UP_URL = URL + sign;

        public static void ApplyServerSettings()
        {
            URL = DataClass.GetInstance.CurrentServer.Api;
            FAYE_NOTIFICATION_URL = DataClass.GetInstance.CurrentServer.Notification;
            RUNNING_SERVER_NAME = DataClass.GetInstance.CurrentServer.Name.Equals(SERVER_NAME) ? SERVER_NAME : DataClass.GetInstance.CurrentServer.Name;
            //ROOT_API_URL = URL + "/api" + "/v1";
            //ROOT_API_V2_URL = URL + "/api" + "/v2";
            //SIGN_UP_URL = URL + REGISTRATION_PAGE_URL;
            //Faye.Instance.Disconnect();
            App.Log($"SERVER NAME: - {RUNNING_SERVER_NAME}");
            App.Log($"URL - {URL}");
            App.Log($"FAYE - {FAYE_NOTIFICATION_URL}");
            App.Log($"ROOT URL - {ROOT_URL}");
        }
        #endregion
    }
}