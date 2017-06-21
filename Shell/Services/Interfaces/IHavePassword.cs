using System.Security;

namespace Shell.Services.Interfaces
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}