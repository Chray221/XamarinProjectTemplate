using System;
using System.Text.RegularExpressions;
using XamProjectTemplate;

namespace XamProjectTemplate.Extension
{
    public static class PhoneNumberExtension
	{
        public static bool IsNumber(this string phoneEntry)
        {
            return ToPhoneNumber(phoneEntry).Length < 13;
        }

        public static bool IsValidPhoneNumber(this string phoneEntry)
        {
            return Regex.IsMatch(phoneEntry, @"^(09|\+639)\d{9}$");
        }

        /// <summary>
        /// Convert +63 (912) 345 6789 => +639123456789 
        /// </summary>
        /// <param name="phoneEntry"></param>
        /// <returns></returns>
        public static string ToPhoneNumber(this string phoneEntry)
        {
            App.Log($"PHONE NUMBER: {Regex.Replace(phoneEntry, "[() -]", "")}");
            return Regex.Replace(phoneEntry, "[() -]", "");
        }

        /// <summary>
        /// Convert +63 (912) 345 6789 => +639123456789 => 09123456789
        /// </summary>
        /// <param name="phoneEntry"></param>
        /// <returns></returns>
        public static string ToLocalPhoneNumber(this string phoneEntry)
        {
            //App.Log($"PHONE NUMBER: {Regex.Replace(phoneEntry, "[() -]", "")}");
            if (!phoneEntry.Contains("+63") && phoneEntry[0] != '0')
                phoneEntry = $"0{phoneEntry}";
            return ToPhoneNumber(phoneEntry).Replace("+63","0");
        }



        /// <summary>
        /// Convert +63 (912) 345 6789 => +639123456789
        /// </summary>
        /// <param name="phoneEntry"></param>
        /// <returns></returns>
        public static string ToGlobalPhoneNumber(this string phoneEntry)
        {
            //App.Log($"PHONE NUMBER: {Regex.Replace(phoneEntry, "[() -]", "")}");
            var phonenumber = ToLocalPhoneNumber(phoneEntry);
            if (phoneEntry.IsValidPhoneNumber() && phonenumber[0] == '0')
                return $"+63{phonenumber.Substring(1,10)}";
            return phonenumber;
        }
    }
}
