using Autotestai.Drivers;
using Autotestai.Ikea;
using Autotestai.Page;
using Autotestai.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autotestai.Test
{
    public class BaseTest
    {
        public static IWebDriver driver;
        public static DropdownDemoPage dropdownDemoPage;
        public static IkeaSearchPage ikeaSearchPage;
        public static IkeaClientAccountPage ikeaClientAccountPage;
        public static IkeaBuyerBasketPage ikeaBuyerBasketPage;
        public static IkeaDeliveryPage ikeaDeliveryPage;
        public static IkeaXmasPage IkeaXmasPage;

        [OneTimeSetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetChromeDriver();
            
            dropdownDemoPage = new DropdownDemoPage(driver);
            ikeaSearchPage = new IkeaSearchPage(driver);
            ikeaClientAccountPage = new IkeaClientAccountPage(driver);
            ikeaBuyerBasketPage = new IkeaBuyerBasketPage(driver);
            ikeaDeliveryPage = new IkeaDeliveryPage(driver);
            IkeaXmasPage = new IkeaXmasPage(driver);
        }

        [TearDown]
        public static void TakeScreenshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MyScreenshot.MakeScreenshot(driver);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            driver.Quit();
        }
    }
}
