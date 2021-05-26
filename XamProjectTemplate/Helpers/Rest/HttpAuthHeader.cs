using System;
using System.Text;

namespace XamProjectTemplate.Rest
{
    public static class HttpAuthHeader
    {
        public static string BasicAuth(string username, string XamProjectTemplateword)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, XamProjectTemplateword)));
        }
    }
}
