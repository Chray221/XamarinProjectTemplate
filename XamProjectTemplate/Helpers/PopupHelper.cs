using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ImTools;
using Prism.Navigation;
using XamProjectTemplate.Resources;
using XamProjectTemplate.Views.CustomViews;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace XamProjectTemplate.Helpers
{
    public class PopupHelper
    {
        static object RemoveLoadingLock = new object();
        static TaskCompletionSource<bool> tcs;

        static PopupPage LoadingView;

        static PopupPage GetLoadingView()
        {
            if (LoadingView == null)
                LoadingView = new PopupPage()
                {
                    CloseWhenBackgroundIsClicked = false,
                    Title = "Loading",
                    BackgroundColor = ColorResource.LOADING_BACKGROUNDCOLOR,
                    Content = new Frame()
                    {
                        HeightRequest = 50.ScaleWidth(),
                        WidthRequest = 50.ScaleWidth(),
                        CornerRadius = 5.ScaleWidth(),
                        Padding = new Thickness(5.ScaleHeight()),
                        BackgroundColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Content = new ActivityIndicator()
                        {
                            IsRunning = true,
                            IsEnabled = true,
                            IsVisible = true,
                            Color = ColorResource.MAIN_THEME_COLOR,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill
                        }
                    }
                };
            ((LoadingView.Content as Frame).Content as ActivityIndicator).IsRunning = true;
            return LoadingView;
        }

        public static void ShowLoading()
        {
            lock (RemoveLoadingLock)
            {
                //TODO Add LoadingPage 
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var loadingPage = PopupNavigation.Instance.PopupStack.FirstOrDefault(obj => obj is RGPopupPage rGPopupPage && rGPopupPage.Title == "Loading");
                    if (loadingPage == null)
                    {
                        //DataClass.GetInstance.LoadingTimeoutMessage = message;
                        await PopupNavigation.Instance.PushAsync(GetLoadingView(), false);
                    }
                });
            }
        }

        public static void RemoveLoading(string message = "", bool isBackgroundTransparent = false, bool isTimerOn = true)
        {
            lock (RemoveLoadingLock)
            {
                //TODO Add LoadingPage 
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (PopupNavigation.Instance.PopupStack.Count > 0)
                    {
                        var loadingPage = PopupNavigation.Instance.PopupStack.FirstOrDefault(obj => obj is RGPopupPage rGPopupPage && rGPopupPage.Title == "Loading");
                        if (loadingPage != null)
                        {
                            await PopupNavigation.Instance.RemovePageAsync(loadingPage, false);
                        }
                    }
                });
            }
        }

        public static async Task ShowLoadingAsync()
        {
            TaskCompletionSource<bool> sltcs = new TaskCompletionSource<bool>();
            lock (RemoveLoadingLock)
            {
                //TODO Add LoadingPage 
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var loadingPage = PopupNavigation.Instance.PopupStack.FirstOrDefault(obj => obj is RGPopupPage rGPopupPage && rGPopupPage.Title == "Loading");
                    if (loadingPage == null)
                    {
                        //DataClass.GetInstance.LoadingTimeoutMessage = message;
                        await PopupNavigation.Instance.PushAsync(GetLoadingView(), false);
                        sltcs.TrySetResult(true);
                    }
                });
            }
            await sltcs.Task;
        }

        public static async Task RemoveLoadingAsync(string message = "", bool isBackgroundTransparent = false, bool isTimerOn = true)
        {
            tcs = new TaskCompletionSource<bool>();
            lock (RemoveLoadingLock)
            {
                //TODO Add LoadingPage 
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (PopupNavigation.Instance.PopupStack.Count > 0)
                    {
                        var loadingPage = PopupNavigation.Instance.PopupStack.FirstOrDefault(obj => obj is RGPopupPage rGPopupPage && rGPopupPage.Title == "Loading");
                        if (loadingPage != null)
                        {
                            //loadingPage.StopLoading();
                            await PopupNavigation.Instance.RemovePageAsync(loadingPage, false);
                            tcs?.TrySetResult(true);
                            tcs = null;
                            if (!string.IsNullOrWhiteSpace(message))
                                //ShowToast(message, isBackgroundTransparent, isTimerOn);
                                ShowToast(message);
                        }
                    }
                    tcs?.TrySetResult(false);
                });
            }
            await tcs?.Task;
        }


        //public static Task<bool> RemoveCustomAlert()
        //{
        //    tcs = new TaskCompletionSource<bool>();
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        var alertPage = PopupNavigation.Instance.PopupStack.FirstOrDefault(obj => obj is CustomAlertPopup);
        //        if (alertPage != null && alertPage is CustomAlertPopup customAlertPopup)
        //        {
        //            await PopupNavigation.Instance.RemovePageAsync(customAlertPopup, false);
        //            tcs.TrySetResult(true);
        //        }
        //        else
        //        {
        //            tcs.TrySetResult(false);
        //        }
        //    });

        //    return tcs.Task;
        //}

        //public static Task<bool> ShowCustomAlert(CustomAlertModel customAlertModel)
        //{
        //    tcs = new TaskCompletionSource<bool>();
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        var alertPage = PopupNavigation.Instance.PopupStack.FirstOrDefault(obj => obj is CustomAlertPopup);
        //        if (alertPage == null)
        //        {
        //            PopupNavigation.Instance.PushAsync(new CustomAlertPopup() { CustomAlertContent = customAlertModel, tcs = tcs });
        //        }
        //        else
        //        {
        //            (alertPage as CustomAlertPopup).CustomAlertContent = customAlertModel;
        //            (alertPage as CustomAlertPopup).tcs = tcs;
        //        }
        //    });
        //    //tcs.TrySetResult(true);
        //    return tcs.Task;
        //}

        public static void ShowToast(string message)
        {
            string toastId = Guid.NewGuid().ToString();
            System.Timers.Timer timer = new System.Timers.Timer(3000);
            timer.Elapsed += (sende, elapseEventArgs) =>
            {
                if (PopupNavigation.Instance.PopupStack.FindFirst((page) => page.Title == toastId) is RGPopupPage toastPage)
                {
                    PopupNavigation.Instance.RemovePageAsync(toastPage);
                }
            };
            timer.Start();
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            PopupNavigation.Instance.PushAsync(new RGPopupPage()
            {
                Title = toastId,
                CloseWhenBackgroundIsClicked = true,
                BackgroundColor = Color.Transparent,
                Content =
                //new Shadows()
                //{
                //    CornerRadius = (int)50.ScaleHeight(),
                //    Shades = new List<Shade>()
                //    { new Shade( )
                //        {
                //            BlurRadius = 6,
                //            Offset = new Point(0,0),
                //            Opacity = .5
                //        }

                //    },
                //    Content =
                    new Frame()
                    {
                        Margin = new Thickness(10, 20).ScaleThickness(),
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.End,
                        Padding = 0,
                        CornerRadius = 20.ScaleHeight(),
                        BackgroundColor = Color.LightGray,
                        Content = new Label()
                        {
                            Text = message,
                            TextColor = ColorResource.MAIN_BLACK_COLOR,
                            FontFamily = FontResource.GILROY_REGULAR,
                            FontSize = 15.ScaleFont(),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Margin = new Thickness(10).ScaleThickness()
                        }
                    }
                //}

            });

            //});
        }

    }
}
