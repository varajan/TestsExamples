using Tests.API.Models;

namespace Tests.API
{
    public static class History
    {
        public static void Clear(string login) => ApiClient.Post("History/Clear", new SaveHistoryDto{Login = login});

        public static void Save(SaveHistoryDto history) => ApiClient.Post("History/Save", history);
    }
}
