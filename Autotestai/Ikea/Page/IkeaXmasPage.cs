using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using VCSproject;

namespace Autotestai.Ikea
{
    public class IkeaXmasPage : BasePage
    {
        private const string PageAddress = "https://www.ikea.lt/lt/rooms/kaledos";


        private IWebElement FilterPriceButton => Driver.FindElement(By.CssSelector(".filterContainerMultiple > .filterBlock:nth-child(1) > .filterTitle"));
        private IWebElement FilterPriceToChoose => Driver.FindElement(By.CssSelector("#filterSelectors > div > div > nav > div > div.filterContainerMultiple > div:nth-child(1) > ul > li:nth-child(2) > span > div > ins"));
        private IWebElement FilterItemNameButton => Driver.FindElement(By.CssSelector(".filterBlock:nth-child(2) > .filterTitle"));
        private IWebElement FilterItemNameToChoose => Driver.FindElement(By.CssSelector("#filterSelectors > div > div > nav > div > div.filterContainerMultiple > div:nth-child(2) > ul > li:nth-child(26) > span > div > ins"));
        private IWebElement ItemNameFromFilter => Driver.FindElement(By.CssSelector("#filterSelectors > div > div > nav > div > div.filterContainerMultiple > div:nth-child(2) > ul > li:nth-child(26) > span > label"));
        private IWebElement DisplayedItemName => Driver.FindElement(By.CssSelector(".itemName > .display-7"));
        private IWebElement DisplayedItemPrice => Driver.FindElement(By.CssSelector(".itemNormalPrice > span:nth-child(1)"));

        public IkeaXmasPage(IWebDriver webdriver) : base(webdriver)
        { }
        public IkeaXmasPage NavigateToDefaltPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }

        public IkeaXmasPage AcceptAllCookies()
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

        public IkeaXmasPage ChoosePriceLimit()
        {
            FilterPriceButton.Click();
            FilterPriceToChoose.Click();
            return this;
        }

        public IkeaXmasPage ChooseItemName()
        {
            FilterItemNameButton.Click();
            FilterItemNameToChoose.Click();

            return this;
        }

        private IkeaXmasPage VerifyResultName()
        {
            string itemNameFromFilter = ItemNameFromFilter.Text;
            string itemNameDisplayed = DisplayedItemName.Text;

            Assert.That(itemNameFromFilter, Is.EqualTo(itemNameDisplayed), "Displayed Item is not the same that have choosed");
            return this;
        }
        private IkeaXmasPage VerifyResultPrice()
        {
            string preparedPriceDisplayed = DisplayedItemPrice.Text.Replace(" €", "");
            decimal itemPriceDisplayed = Convert.ToDecimal(preparedPriceDisplayed);

            Assert.IsTrue(50 > itemPriceDisplayed, "Displayed Item price is not the same that have choosed");
            return this;
        }
        public IkeaXmasPage VerifyResult()
        {
            VerifyResultName();
            VerifyResultPrice();
            return this;
        }
    }
}
