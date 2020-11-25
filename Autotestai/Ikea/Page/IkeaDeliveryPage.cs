using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VCSproject;

namespace Autotestai.Ikea
{
    public class IkeaDeliveryPage : BasePage
    {
        private const string PageAddress = "https://www.ikea.lt/lt/rooms////vinter-2020-dirbtinis-vazoninis-augalas-zalia-art-30474851";

        private IWebElement PutToBusketButton => Driver.FindElement(By.CssSelector(".addToCart > span:nth-child(2)"));
        private IWebElement GoToBuyerBasketButton => Driver.FindElement(By.CssSelector(".goToCart > span:nth-child(1)"));
        private IWebElement PostCodeBox => Driver.FindElement(By.CssSelector(".select2-selection__placeholder"));
        private IWebElement DeliverySumCountButton => Driver.FindElement(By.CssSelector("#district-form > div > div.col-4.d-flex.align-items-end > button"));
        private IWebElement DeliverySumCountInputButton => Driver.FindElement(By.CssSelector(".select2-search__field"));
        private IWebElement ResultDeliverySum => Driver.FindElement(By.CssSelector("#payment > div.secure-box.col-12.col-lg-5.offset-lg-2.order-lg-2.order-1 > div.summaryDetails > div.summary > div > div:nth-child(2) > div > div.col-4 > p"));
        public IkeaDeliveryPage(IWebDriver webdriver) : base(webdriver)
        { }

        public IkeaDeliveryPage NavigateToDefaltPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }
        public IkeaDeliveryPage AcceptAllCookies()
        {
            Cookie myCookie = new Cookie
            ("CookieConsent",
            "{stamp:%27wAJW+NemTeuqu1nqUjwV7BCY5tGDPhnkuGAts+rOBigDH8KrCZYibQ==%27%2Cnecessary:true%2Cpreferences:false%2Cstatistics:false%2Cmarketing:false%2Cver:1%2Cutc:1606236608725%2Cregion:%27lt%27}",
            "www.ikea.lt",
             "/",
             DateTime.Now.AddDays(7)
              );
            Driver.Manage().Cookies.AddCookie(myCookie);
            Driver.Navigate().Refresh();
            return this;
        }

        public IkeaDeliveryPage PutIntoBuyerBasket()
        {
            PutToBusketButton.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(GoToBuyerBasketButton));
            GoToBuyerBasketButton.Click();
            return this;
        }

        public IkeaDeliveryPage PutThePostCode(string postCode)
        {
            PostCodeBox.Click();
            DeliverySumCountInputButton.SendKeys(postCode);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(DeliverySumCountButton));
            DeliverySumCountButton.Click();
            WebDriverWait wait1 = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait1.Until(ExpectedConditions.ElementToBeClickable(DeliverySumCountButton));
            DeliverySumCountButton.Click();
            return this;
        }

        public IkeaDeliveryPage VerifyResultDeliverySum(string deliverySum)
        {
            string finalResultDeliverySum = ResultDeliverySum.Text;
            Assert.That(finalResultDeliverySum, Is.EqualTo(deliverySum),"No, the price for delivery is wrong");
            return this;

        }
    }
}
