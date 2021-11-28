﻿using Atata;
using NUnit.Framework;

namespace Tests.Tests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext
                .GlobalConfiguration
                .UseChrome()
                .ApplyJsonConfig("chrome_options.json")
                .UseBaseUrl(Defaults.BaseUrl)
                .UseCulture("en-US")
                .UseAllNUnitFeatures();

            AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
        }

        [OneTimeTearDown]
        public void DeleteUsers()
        {
            Go.ToUrl($"{Defaults.BaseUrl}/api/Register/DeleteAll");

            AtataContext.Current?.CleanUp();
        }
    }
}
