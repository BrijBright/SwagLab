using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLab.POM
{
    internal class InventoryItemPage
    {


        private IWebDriver driver;
        public InventoryItemPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

       
        public IWebElement getItem(IWebDriver driver)
        {

            return driver.FindElement(By.XPath("//button[contains(text(),'ADD TO CART')]"));
        }











    }
}
