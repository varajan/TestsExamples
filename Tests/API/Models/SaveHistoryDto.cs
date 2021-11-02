namespace Tests.API.Models
{
    public class SaveHistoryDto
    {
        public string Login { get; set; }
        public string Amount { get; set; }
        public string Percent { get; set; }
        public int Days { get; set; }
        public string Year { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Interest { get; set; }
        public string Income { get; set; }
    }
}
