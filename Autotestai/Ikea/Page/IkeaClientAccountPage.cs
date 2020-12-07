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

namespace Autotestai.Page
{
    public class IkeaClientAccountPage : BasePage
    {
        private string personalName = "Eduard";
        private string personalSurname = "Eduard";
        private string clientEmail = "eduardtest4@gmail.com";
        private string clientPassword = "passpass";
        
        private const string PageAddress = "https://www.ikea.lt/lt/client";
        private IWebElement EmailBox => Driver.FindElement(By.Id("loginForm_email"));
        private IWebElement PasswordBox => Driver.FindElement(By.Id("loginForm_password"));
        private IWebElement SubmitLoginButton => Driver.FindElement(By.Id("btnSubmitLogin"));
        private IWebElement ClientBoxButton => Driver.FindElement(By.CssSelector("#hideOnScroll > ul.navbar.navbar-nav.py-0.ml-lg-auto.align-items-start.px-0.userMenu > li:nth-child(2)"));
        private IWebElement PersonalNameDisplayed => Driver.FindElement(By.Id("personalName"));
        private IWebElement PersonalSurnameDisplayed => Driver.FindElement(By.Id("last_name"));

        public IkeaClientAccountPage(IWebDriver webdriver) : base(webdriver)
        { }
        public IkeaClientAccountPage NavigateToDefaltPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
            return this;
        }

        public IkeaClientAccountPage AcceptAllCookies()
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
        
        public IkeaClientAccountPage InputEmailAddress()
        {
            EmailBox.Click();
            EmailBox.Clear();
            EmailBox.SendKeys(clientEmail);
            return this;
        }
        public IkeaClientAccountPage InputPassword()
        {
            PasswordBox.Click();
            PasswordBox.Clear();
            PasswordBox.SendKeys(clientPassword);
            return this;
        }
        public IkeaClientAccountPage ClickSubmitLoginButton()
        {
            SubmitLoginButton.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            IWebElement popupX = Driver.FindElement(By.Id("pdv4close"));
            wait.Until(d => popupX.Displayed);
            popupX.Click(); 
            return this; 
        }
        public IkeaClientAccountPage ClickClientNameButton()
        {
            ClientBoxButton.Click();
            return this;
        }
        private IkeaClientAccountPage VerifyClientName()
        {
            Assert.That(PersonalNameDisplayed.GetAttribute("value"), Is.EqualTo(personalName), "Name is wrong");
            return this;
        }
        private IkeaClientAccountPage VerifyClientSurname()
        {
            Assert.That(PersonalSurnameDisplayed.GetAttribute("value"), Is.EqualTo(personalSurname), "Surname is wrong");
            return this;
        }
        public IkeaClientAccountPage VerifyFullName()
        {
            VerifyClientName();
            VerifyClientSurname();
            return this;
        }
    }
}
