﻿using Tests.Data;

namespace Tests.Pages
{
    public abstract class BasePage
    {
        public string CurrentPageName => WebDriver.Driver.Title;
        public virtual string PageName => string.Empty;
        
        public bool IsOpened => CurrentPageName.Equals(PageName);

        public void Open() => WebDriver.Driver.Url = $"{Defaults.BaseUrl}/{PageName}";
    }
}
