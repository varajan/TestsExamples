using Tests.API.Models;
using Tests.Data;

namespace Tests.API
{
    public static class History
    {
        public static void Clear(string login) => ApiClient.Post($"{Defaults.BaseUrl}/History/Clear", new SaveHistoryDto{Login = login});

        public static void Save(SaveHistoryDto history) => ApiClient.Post($"{Defaults.BaseUrl}/History/Save", history);
    }
}
