using System;
using System.Globalization;
using System.Runtime.InteropServices;

public static class HttpCookie
{

    [DllImport("__Internal")]
    private static extern string getHttpCookie(string name);
   
    [DllImport("__Internal")]
    private static extern void setHttpCookie(string name, string value, string expires, string path, string domain, string secure);

    public static string GetCookie(string name)
    {
        return getHttpCookie(name);
    }
 
    public static void RemoveCookie(string name)
    {
        setHttpCookie(name, string.Empty, "0", string.Empty, string.Empty, string.Empty);
    }
  
    public static void SetCookie(string name, string value)
    {
        SetCookie(name, value, string.Empty, string.Empty, string.Empty, string.Empty);
    }

  
   public static void SetCookie(string name, string value, string expires, string path, string domain, string secure)
    {
        setHttpCookie(name, value, expires, path, domain, secure);
    }
}