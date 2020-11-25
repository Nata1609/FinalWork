using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCSproject;

namespace Autotestai.Page 
{
    public class IkeaSearchPage : BasePage
    {
        private const string PageAddress = "https://www.ikea.lt/lt";
        private const string nameToFind = "ALBERT";
        private const string nameToFindMinnen = "MINNEN";
        private const string color = "turkio sp.";
        private IWebElement SearchFieldArea => Driver.FindElement(By.Id("header_searcher_desktop_input"));
        private IWebElement SearchInput => Driver.FindElement(By.CssSelector(".input-group-append > .btn"));
        private IWebElement ItemToDisplayAlbert => Driver.FindElement(By.CssSelector(".itemName > .display-7"));
        private IWebElement ItemToDisplayMinnen => Driver.FindElement(By.CssSelector(".col-6:nth-child(2) .display-7"));
        private IWebElement ColorFilter => Driver.FindElement(By.Id("colorFilter"));
        private IWebElement ColorToChooseBalta => Driver.FindElement(By.CssSelector(".col-6:nth-child(1) .itemFacts:nth-child(3)"));
        private IWebElement ItemColorToDisplay => Driver.FindElement(By.CssSelector(".col-6:nth-child(1) .card-body span:nth-child(2)"));
        public IkeaSearchPage(IWebDriver webdriver) : base(webdriver) 
        {
        }
        public IkeaSearchPage NavigateToDefaltPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }
        public IkeaSearchPage ClickPopUp()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement popupX = Driver.FindElement(By.Id("pdv4close"));
            wait.Until(d => popupX.Displayed);
            popupX.Click();
            IWebElement cookie = Driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonAccept"));
            wait.Until(d => cookie.Displayed);
            cookie.Click();
            return this;
        }
        public IkeaSearchPage InputItemAlbertToSearchField()
        {
            SearchFieldArea.SendKeys(nameToFind);
            SearchInput.Click();
                return this;
        }
        public IkeaSearchPage IsChoosenItemAlbertDisplaed()
        {
            Assert.That(ItemToDisplayAlbert.Text, Is.EqualTo(nameToFind));
                return this;
        }
        public IkeaSearchPage InputItemMinnenToSearchField()
        {
            SearchFieldArea.SendKeys(nameToFindMinnen);
            SearchInput.Click();
            return this;
        }
        public IkeaSearchPage ChoiceColor()
        { 
            ColorFilter.Click();
            ColorToChooseBalta.Click();
                return this;
        }
        private IkeaSearchPage VerifyResultItemItemMinnen()
        {
            Assert.That(ItemToDisplayMinnen.Text, Is.EqualTo(nameToFindMinnen),"Displayed Item name is different that have sent");
            return this;
        }
        private IkeaSearchPage VerifyResultColorMinnen()
        {
            Assert.That(ItemColorToDisplay.Text, Is.EqualTo(color), "Displayed Item color is different that have choosed");
            return this;
        }
        public IkeaSearchPage VerifyResultMinnen()
        {
            VerifyResultItemItemMinnen();
            VerifyResultColorMinnen();
            return this;
        }
    }
}
