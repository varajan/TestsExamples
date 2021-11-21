using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Tests
{
    public static class Reporter
    {
        private static ExtentReports _report;
        public static ExtentReports ExtentReport
        {
            get
            {
                if (_report == null)
                {
                    var reportsDir = $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Reports";
                    var htmlReporter = new ExtentV3HtmlReporter($"{reportsDir}/Regression_{DateTime.Now:yyyMMdd_hhmm}.html");

                    Directory.CreateDirectory(reportsDir);

                    _report = new ExtentReports();
                    _report.AddSystemInfo("Tester", Environment.UserName);
                    _report.AddSystemInfo("Computer", Environment.MachineName);
                    _report.AttachReporter(htmlReporter);
                }

                return _report;
            }
        }

        private static ExtentTest _test;
        public static void InitTest()
        {
            _test = ExtentReport.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        public static void AppendReport()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                _test.Log(Status.Fail, TestContext.CurrentContext.Result.Message);
                _test.Log(Status.Fail, TestContext.CurrentContext.Result.StackTrace);
            }
            else
            {
                _test.Log(Status.Pass, "Passed");
            }

            _test = null;
        }
    }
}
