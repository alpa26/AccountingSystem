using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace DK.WebClient.Core.Services;

public class CookieService : ICookieService
{
    private IJSRuntime JsRuntime => Service<IJSRuntime>.GetInstance();
    private string _expires;

    public CookieService()
    {
        _expires = "";
        ExpireDays = 300;
    }

    public async Task SetValue(string key, string value, int? days = null)
    {
        var expires = days != null ? days > 0 ? DateToUtc(days.Value) : "" : _expires;
        await SetCookie($"{key}={value}; expires={expires}; path=/");
    }

    public async Task<string> GetValue(string key)
    {
        var cookieValue = await GetCookie();
        if (string.IsNullOrEmpty(cookieValue))
        {
            return null;
        }

        var cookieValues = cookieValue
            .Split(';')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x));
        foreach (var cookie in cookieValues)
        {
            if (cookie.IndexOf('=') > 0 && cookie.Substring(0, cookie.IndexOf('=')) == key)
            {
                return cookie.Substring(cookie.IndexOf('=') + 1);
            }
        }
        
        return null;
    }

    private async Task SetCookie(string value)
    {
        await JsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
    }

    private async Task<string> GetCookie()
    {
        return await JsRuntime.InvokeAsync<string>("eval", $"document.cookie");
    }

    public int ExpireDays
    {
        set => _expires = DateToUtc(value);
    }

    private static string DateToUtc(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
}