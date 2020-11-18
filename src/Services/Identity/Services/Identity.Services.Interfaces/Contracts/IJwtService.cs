using Microsoft.Win32.SafeHandles;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IJwtService
    {
        string Secret { get; set; }
    }
}
