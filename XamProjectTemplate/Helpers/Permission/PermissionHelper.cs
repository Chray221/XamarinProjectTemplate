using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace XamProjectTemplate.Helpers.Permission
{
    public static class PermissionHelper
    {

        public static async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission) where T : Permissions.BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }
        static bool IsLocationDialog;
        public static async Task<Location> GetLocationAsync()
        {
            Location location = null;
            if (IsLocationDialog == false)
            {
                try
                {
                    IsLocationDialog = true;
                    
                    var locationPermission = await CheckAndRequestPermissionAsync(new Permissions.LocationWhenInUse()); // iOS and Android
                    if (locationPermission != PermissionStatus.Granted)
                    {
                        location = await Geolocation.GetLastKnownLocationAsync();
                        var result = await App.Current.MainPage.DisplayAlert("Permission Denied", "Location permission is required to Check In and Check out. Please go to your phone's Settings and turn on permissions for the app.",
                             "Open Settings", "Cancel");
                        //Utilities.ShowCustomMessagePopup(OpenSettings, DialogBackground, new CustomMessageModel() { Title = "Permission Denied", Content = "Location permission is required to scan a traveler. Please go to your phone's Settings and turn on permissions for the app.", Button = "Open Settings", Icon = "exclamation-circle", IconType = 2 });
                        if (result)
                        {
                            AppInfo.ShowSettingsUI();
                        }
                    }
                    else
                    {
                        //location = await Geolocation.GetLocationAsync(new GeolocationRequest() { DesiredAccuracy = GeolocationAccuracy.Medium });
                        location = await Geolocation.GetLastKnownLocationAsync();
                    }

                    if (location != null)
                    {
                        App.Log($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        DataClass.GetInstance.CurrentLocation = location;
                    }
                    IsLocationDialog = false;
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                    App.LogException(fnsEx);
                    IsLocationDialog = false;
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                    App.LogException(fneEx);
                    IsLocationDialog = false;
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                    App.LogException(pEx);
                    IsLocationDialog = false;
                }
                catch (Exception ex)
                {
                    // Unable to get location
                    App.LogException(ex);
                    IsLocationDialog = false;
                }
            }
            else
            {
                location = await Geolocation.GetLastKnownLocationAsync();
            }
            return location;
        }


    }
}
