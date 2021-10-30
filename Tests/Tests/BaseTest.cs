﻿using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    public class BaseTest
    {
        public LoginPage LoginPage => new();
        public DepositPage DepositPage => new();


        [TearDown]
        public void TearDown() => WebDriver.Quit();
    }
}
