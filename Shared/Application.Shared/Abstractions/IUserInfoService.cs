namespace Application.Shared.Abstractions
{
    public interface IUserInfoService
    {
        string GetUsernameFromJwtToken(string token);
        string GetRoleFromJwtToken(string token);
    }
}
