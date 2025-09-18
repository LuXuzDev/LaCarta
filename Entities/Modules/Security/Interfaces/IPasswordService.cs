namespace Domain.Modules.Security.Interfaces;

public interface IPasswordService
{
    string Hash(string password);
    bool  Verify(string hashedPassword, string providedPassword);
}