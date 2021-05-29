namespace Identity.Services.Interfaces.Contracts
{
    public interface IPasswordService
    {
        string GeneratePassword(int lenght);
    }
}
