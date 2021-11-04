﻿namespace Tests
{
    public static class Defaults
    {
        public static readonly string BaseUrl = "http://localhost:64177";
        public static readonly int PageLoad = 15;
        public static readonly int ImplicitWait = 3;


        public static readonly string Login = "test";
        public static readonly string Email = "test@test.com";
        public static readonly string Password = "newyork1";

        public static readonly string Currency = "$ - US dollar";
        public static readonly string NumberFormat = "123,456,789.00";
        public static readonly string DateFormat = "dd/MM/yyyy";

        public static readonly string[] HistoryHeaders = {"Amount", "%", "Term", "Year", "From", "To", "Interest", "Income"};
}
}
