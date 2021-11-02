using Tests.API.Models;

namespace Tests.API
{
    public static class Users
    {
        public static void DeleteAll() => ApiClient.Delete("Register/DeleteAll");
        public static void Delete(string login) => ApiClient.Delete("Register/Delete", new UserDto {Login = login});
        public static void Register(UserDto user) => ApiClient.Post("Register/Register", user);
    }
}
