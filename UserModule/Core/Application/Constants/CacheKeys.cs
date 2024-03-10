namespace Users.Application.Constants
{
    public static class CacheKeys
    {
        public const string UsersKey = "users";
        public static string GetUserKey(Guid id)
        {
            return $"user-{id}";
        }
        public static string GetRefreshTokenKey(string token)
        {
            return $"refreshToken-{token}";
        }
    }
}
