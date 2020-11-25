using NUnit.Framework;
using OpenQA.Selenium;
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
    public class IkeaBuyerBasketPage : BasePage
    {
        private const string PageAddress = "https://www.ikea.lt/lt/rooms/virtuve/stalo-serviravimas/puodukai";
       
        private IWebElement FargrikCup => Driver.FindElement(By.CssSelector("#contentWrapper > div > div > div.products_list.w-100.d-flex.flex-wrap > div:nth-child(4) > div > div.card-header > a > div.productImg > img"));
        private IWebElement ItemNumber => Driver.FindElement(By.CssSelector(".action:nth-child(3)"));
        private IWebElement PutToBusketButton => Driver.FindElement(By.CssSelector(".addToCart > span:nth-child(2)"));
        private IWebElement ContinuePutInBasketButton => Driver.FindElement(By.CssSelector(".continueShopping > span:nth-child(2)"));
        private IWebElement SelectKitchenButton => Driver.FindElement(By.CssSelector(".breadcrumb-item:nth-child(3) > a"));
        private IWebElement SortGarbageButton => Driver.FindElement(By.CssSelector(".col-6:nth-child(16) img"));
        private IWebElement ChooseGarbageBoxButton => Driver.FindElement(By.CssSelector(".col-6:nth-child(3) .card-img-top"));
        private IWebElement GoToBuyerBasketButton => Driver.FindElement(By.CssSelector(".goToCart > span:nth-child(1)"));
        private IWebElement TotalItemsCount => Driver.FindElement(By.CssSelector(".py-3 > span:nth-child(1)"));
        private IWebElement TotalItemsMoneyToPay => Driver.FindElement(By.CssSelector("#cart > div.text-center.py-3 > b"));
        private IWebElement Item1Price => Driver.FindElement(By.CssSelector(".itemNormalPrice"));
        private IWebElement Item1Count => Driver.FindElement(By.CssSelector(".col-12:nth-child(5) .val"));
        private IWebElement Item1Amount => Driver.FindElement(By.CssSelector(".col-12:nth-child(5) strong"));
        private IWebElement Item2Price => Driver.FindElement(By.CssSelector(".item:nth-child(2) > .row"));
        private IWebElement Item2Count => Driver.FindElement(By.CssSelector(".col-12:nth-child(4) .val"));
        private IWebElement Item2Amount => Driver.FindElement(By.CssSelector(".col-12:nth-child(4) strong"));
        
        public IkeaBuyerBasketPage(IWebDriver webdriver) : base(webdriver)
        { }
        public IkeaBuyerBasketPage NavigateToDefaltPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }
        private IkeaBuyerBasketPage ClickCookie()
        {
            Thread.Sleep(2000);
            Driver.SwitchTo().ParentFrame();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            IWebElement cookie = Driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonAccept"));
            wait.Until(d => cookie.Displayed);
            cookie.Click();
            return this;
        }
        private IkeaBuyerBasketPage FargrikCupClick()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(FargrikCup));
            FargrikCup.Click();
            return this;
        }
        private IkeaBuyerBasketPage ChooseNumberOfItems()
        {
            ItemNumber.Click();
            ItemNumber.Click();
            return this;
        }
        private IkeaBuyerBasketPage PutIntoBuyerBasket()
        {
            PutToBusketButton.Click();
            return this;
        }
        public IkeaBuyerBasketPage PutIntoBasketFirstItem()
        {
            FargrikCupClick();
            ClickCookie();
            ChooseNumberOfItems();
            PutIntoBuyerBasket();
            return this;
        }
        public IkeaBuyerBasketPage ContinuePutInBasket()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(ContinuePutInBasketButton));
            ContinuePutInBasketButton.Click();
            return this;
        }
        private IkeaBuyerBasketPage SelectKitchenButtonClick()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(SelectKitchenButton));
            SelectKitchenButton.Click();
            return this;
        }
        private IkeaBuyerBasketPage GlesItemAddToBasket()
        {
            SortGarbageButton.Click();
            ChooseGarbageBoxButton.Click();
            ItemNumber.Click();
            PutToBusketButton.Click();
            return this;
        }
        public IkeaBuyerBasketPage PutIntoTheBasketSecondItem()
        {
            SelectKitchenButtonClick();
            GlesItemAddToBasket();
            return this;
        }
        public IkeaBuyerBasketPage GoToBuyerBasket()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(GoToBuyerBasketButton));
            GoToBuyerBasketButton.Click();
            return this;
        }
        public IkeaBuyerBasketPage ChekcFirstItemAmount()
        {
            string preparedSumItem1 = Item1Price.Text.Trim().Replace(" € / 4 vnt.","");
            decimal firstItemPrice = Convert.ToDecimal(preparedSumItem1);
            string preparedCountItem1 = Item1Count.GetAttribute("value");
            int firstItemCount = Int32.Parse(preparedCountItem1);
            string preparedAmountItem1 = Item1Amount.Text.Trim().Replace(" €","");
            decimal firstItemAmount = Convert.ToDecimal(preparedAmountItem1);
            Assert.IsTrue(firstItemPrice*firstItemCount == firstItemAmount, "Amount counting is wrong");
            return this;
        }
        public IkeaBuyerBasketPage ChekcSecondItemAmount()
        {
            string preparedSumItem2 = Item2Price.Text.Trim().Replace("GLES\r\ndėžė\r\n604.292.87\r\n", "").Replace(" €\r\nPakuotės: 1\r\n-\r\n+\r\n2,58 €", "");
            decimal secondItemPrice = Convert.ToDecimal(preparedSumItem2);
            string preparedCountItem2 = Item2Count.GetAttribute("value");
            int secondItemCount = Int32.Parse(preparedCountItem2);
            string preparedAmountItem2 = Item2Amount.Text.Trim().Replace(" €", "");
            decimal secondItemAmount = Convert.ToDecimal(preparedAmountItem2);
            Assert.IsTrue(secondItemPrice * secondItemCount == secondItemAmount, "Amount counting is wrong");
            return this;
        }
        public IkeaBuyerBasketPage CheckItemsCountResult()
        {
            string preparedCountItem1 = Item1Count.GetAttribute("value");
            int firstItemCount = Int32.Parse(preparedCountItem1);
            string preparedCountItem2 = Item2Count.GetAttribute("value");
            int secondItemCount = Int32.Parse(preparedCountItem2);
            int buyerBasketCountResult = Int32.Parse(TotalItemsCount.Text);
            Assert.That(buyerBasketCountResult, Is.EqualTo(firstItemCount + secondItemCount), "Result is wrong");
            return this;
        }
        public IkeaBuyerBasketPage CheckItemsMoneyToPayResult()
        {
            string preparedAmountItem1 = Item1Amount.Text.Trim().Replace(" €", "");
            decimal firstItemAmount = Convert.ToDecimal(preparedAmountItem1);
            string preparedAmountItem2 = Item2Amount.Text.Trim().Replace(" €", "");
            decimal secondItemAmount = Convert.ToDecimal(preparedAmountItem2);
            string preparedbuyerBasketAmountResult = TotalItemsMoneyToPay.Text.Trim().Replace(" €", "");
            decimal buyerBasketAmountResult = Convert.ToDecimal(preparedbuyerBasketAmountResult);
            Assert.That(buyerBasketAmountResult, Is.EqualTo(firstItemAmount+ secondItemAmount), "Result is wrong");
            return this;
        }
        public IkeaBuyerBasketPage VerifyResult()
        {
            CheckItemsCountResult();
            CheckItemsMoneyToPayResult();
            return this;
        }
    }
}
