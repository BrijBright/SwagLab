using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLab.POM
{
    internal class CartPage
    {

        private IWebDriver driver;
        public CartPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

        //page factorty
        //var ContinueShopping = driver.FindElement(By.XPath("//a[contains(text(),'Continue Shopping')]"));

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Continue Shopping')]")]
        private IWebElement ContinueShopping;
        public IWebElement getContinueShopping()
        {

            return ContinueShopping;
        }

        //IWebElement CheckOut = driver.FindElement(By.XPath("//a[contains(text(),'CHECKOUT')]"));

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'CHECKOUT')]")]
        private IWebElement CheckOut;
        public IWebElement getCheckOut()
        {

            return CheckOut;
        }


        //IWebElement firstNameInput = driver.FindElement(By.Id("first-name"));
        //IWebElement lastNameInput = driver.FindElement(By.Id("last-name"));
        //IWebElement postalCodeInput = driver.FindElement(By.Id("postal-code"));

        [FindsBy(How = How.Id, Using = "first-name")]
        private IWebElement firstName;
        public IWebElement getFirstName()
        {

            return firstName;
        }

        [FindsBy(How = How.Id, Using = "last-name")]
        private IWebElement lastName;
        public IWebElement getLastName()
        {

            return lastName;
        }

        [FindsBy(How = How.Id, Using = "postal-code")]
        private IWebElement postalCode;
        public IWebElement getPostalCode() { 

            return postalCode;
        }

        //IWebElement continueButton = driver.FindElement(By.CssSelector("input.btn_primary.cart_button"));

        [FindsBy(How = How.CssSelector, Using = "input.btn_primary.cart_button")]
        private IWebElement continueButton;
        public IWebElement getSubmitInfo()
        {

            return continueButton;
        }

        //IWebElement finish = driver.FindElement(By.XPath("//a[contains(text(),'FINISH')]"));

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'FINISH')]")]
        private IWebElement finish;
        public IWebElement getFinishShopping()
        {

            return finish;
        }





    }
}
