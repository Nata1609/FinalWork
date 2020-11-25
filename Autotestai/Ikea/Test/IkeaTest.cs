using Autotestai.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using VCSproject;

namespace Autotestai.Test
{
    public class IkeaTest : BaseTest
    {
        [Test]
        public void CheckSearchFieldTest()
        {
            ikeaSearchPage.NavigateToDefaltPage()
                          .ClickPopUp()
                          .InputItemAlbertToSearchField()
                          .IsChoosenItemAlbertDisplaed();
        }

        [Test]
        public void CheckSearchFieldWithColorChoiceTest()
        {
            ikeaSearchPage.NavigateToDefaltPage()
                          .ClickPopUp()
                          .InputItemMinnenToSearchField()
                          .ChoiceColor()
                          .VerifyResultMinnen();
        }

        [Test]
        public void ClientAccountTest()
        {
            ikeaClientAccountPage.NavigateToDefaltPage()
                                 .AcceptAllCookies()
                                 .InputEmailAddress()
                                 .InputPassword()
                                 .ClickSubmitLoginButton()
                                 .ClickClientNameButton()
                                 .VerifyFullName();
        }

        [Test]
        public void CheckTheBuyerBasketListTest()
        {
            ikeaBuyerBasketPage.NavigateToDefaltPage()
                               .PutIntoBasketFirstItem()
                               .ContinuePutInBasket()
                               .PutIntoTheBasketSecondItem()
                               .GoToBuyerBasket()
                               .ChekcFirstItemAmount()
                               .ChekcSecondItemAmount()
                               .VerifyResult();
        }

        [TestCase("06321", "19,90 €", TestName = "Pastu Indeksas 06321, Vilnius = 19,90€")]
        [TestCase("32121", "75 €", TestName = "Pastu Indeksas 32121, Zarasai = 75€")]
        [TestCase("50218", "19,90 €", TestName = "Pastu Indeksas 50218, Kaunas = 19,90€")]
        public void ChekIkeaDeliveryTest(string postCode, string deliverySum)
        {
            ikeaDeliveryPage.NavigateToDefaltPage()
                            .AcceptAllCookies()
                            .PutIntoBuyerBasket()
                            .PutThePostCode(postCode)
                            .VerifyResultDeliverySum(deliverySum);
        }

        [Test]
        public void CheckChoosedItemByPriceUnder50AndName()
        {
            IkeaXmasPage.NavigateToDefaltPage()
                        .AcceptAllCookies()
                        .ChoosePriceLimit()
                        .ChooseItemName()
                        .VerifyResult();
        }
    }
}
