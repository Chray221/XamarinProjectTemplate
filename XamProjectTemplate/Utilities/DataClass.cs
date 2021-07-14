using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XamProjectTemplate.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using DryIoc;
using System.Runtime.CompilerServices;
using FastExpressionCompiler.LightExpression;
using XamProjectTemplate.ViewModels;
using System.Linq;

namespace XamProjectTemplate
{
    public class DataClass : ModelBase
    {
        static DataClass dataClass;
        public static DataClass GetInstance
        {
            get
            {
                if (dataClass == null)
                {
                    dataClass = new DataClass();
                }
                return dataClass;
            }
        }

        public DataClass()
        {

        }

        public Location CurrentLocation { get; set; } = new Location(0, 0);

        Server DefaultServer()
        {
            return new Server()
            {
                Name = Constants.SERVER_NAME,
                Api = Constants.URL_STABLE,
                Notification = Constants.URL_FAYE_NOTIFICATION,
                IsSecured = Constants.IS_SERVER_SECURE
            };
        }

        string _LastLatestVersion;
        public string LastLatestVersion
        {
            get { return GetDataProperty(ref _LastLatestVersion); }
            set { SetDataClassProperty(ref _LastLatestVersion, value); }
        }

        Server _CurrentServer;
        public Server CurrentServer
        {
            get { return GetDataClassProperty(ref _CurrentServer); }
            set { SetDataClassProperty(ref _CurrentServer, value); }
        }

        string _token;
        public string Token
        {
            get { return GetDataProperty(ref _token); }
            set { SetDataClassProperty(ref _token, value); }
        }

        string _clientId;
        public string ClientId
        {
            get { return GetDataProperty(ref _clientId); }
            set { SetDataClassProperty(ref _clientId, value); }
        }

        string _uid;
        public string Uid
        {
            get { return GetDataProperty(ref _uid); }
            set { SetDataClassProperty(ref _uid, value); }
        }

        User _User;
        public User User
        {
            
            get { return GetDataClassProperty(ref _User); }
            set { SetDataClassProperty(ref _User, value); }
        }

        bool _IsFirstOpen = true;
        public bool IsFirstOpen
        {
            get { return GetDataProperty(ref _IsFirstOpen); }
            set { SetDataClassProperty(ref _IsFirstOpen, value); }            
        }


        double _BuildNumber;
        public double BuildNumber
        {
            get { return GetDataProperty(ref _BuildNumber); }
            set { SetDataClassProperty(ref _BuildNumber, value); }
        }

        bool? _IsLoggedIn;
        public bool IsLoggedIn
        {
            set { SetDataClassProperty(ref _IsLoggedIn, value); }
            get { return GetDataProperty(ref _IsLoggedIn).Value;}
        }

        public async Task Logout()
        {
            Application.Current.Properties.Remove(nameof(BuildNumber));
            Application.Current.Properties.Remove(nameof(Token));
            Application.Current.Properties.Remove(nameof(ClientId));
            Application.Current.Properties.Remove(nameof(Uid));
            Application.Current.Properties.Remove(nameof(User));
            IsLoggedIn = false;

            Token = null;
            User = null;
            await Application.Current.SavePropertiesAsync();
        }

        public Task SaveData(object data, string key)
        {
            Application.Current.Properties[key] = JsonConvert.SerializeObject(data);
            return Application.Current.SavePropertiesAsync();
        }

        Task SetDataClassProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            oldValue = newValue;
            Application.Current.Properties[propertyName] = JsonConvert.SerializeObject(newValue);
            OnPropertyChanged(propertyName);
            return Application.Current.SavePropertiesAsync();
        }

        T GetDataClassProperty<T>(ref T value, [CallerMemberName] string propertyName = "") where T : new()
        {
            if ((value == null && Application.Current.Properties.ContainsKey(propertyName)))
            {
                value = JsonConvert.DeserializeObject<T>(
                    Application.Current.Properties[propertyName].ToString(),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            }
            if (value == null)
            {
                value = new T();
            }

            return value;
        }

        T GetDataProperty<T>(ref T value, [CallerMemberName] string propertyName = "") 
        {
            if (Application.Current.Properties.ContainsKey(propertyName))
            {
                value = JsonConvert.DeserializeObject<T>(
                    Application.Current.Properties[propertyName].ToString(),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            }

            return value;
        }


    }
}
