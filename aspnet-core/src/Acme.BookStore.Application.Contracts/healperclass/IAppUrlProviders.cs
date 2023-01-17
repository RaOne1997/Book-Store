using System.Threading.Tasks;

namespace Acme.BookStore.healperclass
{
    public interface IAppUrlProviders
    {
        Task<string> GetResetPasswordUrlAsync(string appName);
    }
}