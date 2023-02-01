using System.Threading.Tasks;

namespace Acme.BookStore.healperclass
{
    public interface IAppUrlProviders
    {
        string GetResetPasswordUrl(string appName);
    }
}