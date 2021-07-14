using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XamProjectTemplate.Extensions
{
    public static class StringExtension
    {
        public static string AddAuth(this string url)
        {
            return url + (url.Contains("?") ? "&" : "?") + "access-token=" + DataClass.GetInstance.Token + "&client=" + DataClass.GetInstance.ClientId + "&uid=" + DataClass.GetInstance.Uid;
        }

        public static bool ContainsKey(this JObject json, string key, string value)
        {
            if (json.ContainsKey(key) && json[key].ToString() == value)
                return true;
            return false;
        }


        public static bool ContainsKey(this JToken jToken, string key)
        {
            
            if (jToken.HasValues && ((JObject)jToken).ContainsKey(key))
                return true;
            return false;
        }

        /// <summary>
        /// 1000 -> 1,000
        /// 1521.52 -> 1,521.52
        /// </summary>
        /// <returns>The comma.</returns>
        /// <param name="number">number.</param>
        public static string ToComma(this int number)
        {
            return number.ToString(number >= 1000 || number <= -1000 ? "0,0.##" : "0.##");
        }

        /// <summary>
        /// 1000 -> 1,000
        /// 1521.52 -> 1,521.52
        /// </summary>
        /// <returns>The comma.</returns>
        /// <param name="number">number.</param>
        public static string ToComma(this double number)
        {
            return number.ToString(number >= 1000 || number <= -1000 ? "0,0.##" : "0.##");
        }

        /// <summary>
        /// Clone the specified source. Para dili ma usab ang source.
        /// </summary>
        /// <returns>The clone.</returns>
        /// <param name="source">Source.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                Regex.IsMatch(email, @"\S+@\S+\.\S+");
                //var addr = new System.Net.Mail.MailAddress(email);
                //return addr.Address == email;
                return Regex.IsMatch(email, @"\S+@\S+\.\S+");
            }
            catch
            {
                return false;
            }
        }

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str,@"(\P{Ll})(\P{Ll}\p{Ll})","$1 $2"),@"(\p{Ll})(\P{Ll})","$1 $2");
        }

        public static string SplitByCapitalLetter(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
        }

        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public static List<string> ToList<T>(this T @enum,bool isSplitByCapitalLetter = true)  where T: System.Enum
        {
            List<string> enumStringList = new List<string>();
            foreach (T enumString in System.Enum.GetValues(typeof(T)))
            {
                if (isSplitByCapitalLetter)
                    enumStringList.Add(enumString.ToString().SplitByCapitalLetter().Replace("And", "&"));
                else
                    enumStringList.Add(enumString.ToString());
            }
            return enumStringList;
        }
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }

    public static class MapExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distanceInKM">In Kilometers</param>
        /// <param name="KPHSpeed">35 - average driving speed (in Kilometers per second)</param>
        /// <returns>int value in minutes</returns>
        public static int GetETA(this double distanceInKM,double KPSSpeed = 35)
        {
            return (int)((distanceInKM * 60) / KPSSpeed);
        }
    }
}
