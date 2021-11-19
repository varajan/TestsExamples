using System.Collections.Generic;
using System.Linq;
using WebSite2.Models;

namespace WebSite2.DB
{
    public static class History
    {
        public static void Clear(string login) => DB.Execute($"DELETE FROM History WHERE Login = '{login}'");

        public static List<SaveHistoryDto> Get(string login) =>
            DB
                .GetRows($"SELECT Amount, Percent, Year, StartDate, EndDate, Days, Interest, Income FROM History WHERE Login = '{login}'")
                .Select(x => new SaveHistoryDto
                {
                    Login = login,
                    Amount = x[0],
                    Percent = x[1],
                    Year = x[2],
                    StartDate = x[3],
                    EndDate = x[4],
                    Days = x[5].ToInt(),
                    Interest = x[6],
                    Income = x[7]
                })
                .ToList();

        public static void Add(SaveHistoryDto dto)
        {
            DB.Execute($"INSERT INTO History VALUES " +
                       $"('{dto.Login}', '{dto.Amount}', '{dto.Percent}', '{dto.Year}', '{dto.StartDate}', '{dto.EndDate}', {dto.Days}, '{dto.Interest.ParseNumber(dto.Login)}', '{dto.Income.ParseNumber(dto.Login)}')");
    }
}
}