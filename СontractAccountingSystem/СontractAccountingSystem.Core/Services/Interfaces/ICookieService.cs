using System.Threading.Tasks;

namespace DK.WebClient.Core.Services;

public interface ICookieService
{
    public Task SetValue(string key, string value, int? days = null);
    public Task<string> GetValue(string key);
}