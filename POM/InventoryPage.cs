using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLab.POM
{
    internal class InventoryPage
    {

        private IWebDriver driver;
        public InventoryPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }
        //error_msg=driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;

        //page factorty
        [FindsBy(How = How.CssSelector, Using = "h3[data-test='error']")]
        private IWebElement error_msg;

        public IWebElement getErrorMessage()
        {
            return error_msg;
        }

        //IWebElement logoElement = driver.FindElement(By.ClassName("app_logo"));

        [FindsBy(How = How.ClassName, Using = "app_logo")]
        private IWebElement logoElement;

        public IWebElement getLogoElement()
        {
            return logoElement;
        }

        // IWebElement menuButton = driver.FindElement(By.CssSelector(".bm-burger-button"));

        [FindsBy(How = How.CssSelector, Using = ".bm-burger-button")]
        private IWebElement menuButton;

        public IWebElement getMenuButton()
        {
            return menuButton;
        }

        //IWebElement menuItem = driver.FindElement(By.XPath($"//a[contains(text(), '{menuItemName}')]"));

        public IWebElement getMenuItem(string menuItemName)
        {
            return driver.FindElement(By.XPath($"//a[contains(text(), '{menuItemName}')]"));
        }


        //ReadOnlyCollection<IWebElement> items = driver.FindElements(By.XPath("//button[contains(text(),'ADD TO CART')]"));


        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'ADD TO CART')]")]
        private IList<IWebElement> items;

        public IList<IWebElement> getItems()
        {
            return items;
        }

        //var Ncart = driver.FindElement(By.CssSelector("a[href=\"./cart.html\"]:last-child"));


        [FindsBy(How = How.CssSelector, Using = "a[href=\"./cart.html\"]:last-child")]
        private IWebElement Ncart;

        public int getNumberOfItemsINCart()
        {
            return Ncart.Text == "" ?0:int.Parse(Ncart.Text);



        }

        //var cart = driver.FindElement(By.CssSelector("a[href=\"./cart.html\"]"));

        [FindsBy(How = How.CssSelector, Using = "a[href=\"./cart.html\"]")]
        private IWebElement cart;

        public IWebElement getCart()
        {
            return cart;
        }

        //ReadOnlyCollection<IWebElement> addes_items = driver.FindElements(By.XPath("//button[contains(text(),'REMOVE')]"));


        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'REMOVE')]")]
        private IList<IWebElement> removed_items;

        public IList<IWebElement> getRemoveItems()
        {
            return removed_items; ;
        }


        //int NumberOfItems = driver.FindElements(By.XPath("//div[@class='inventory_item']")).Count();

        [FindsBy(How = How.XPath, Using = "//div[@class='inventory_item']")]
        private IList<IWebElement> inventory_item;

        public IList<IWebElement> getInventoryItems()
        {
            return inventory_item;
        }

        //IWebElement ImageLink = img_div.FindElement(By.XPath(".//a"));

       

        public IWebElement getInventoryImageLink(IWebElement img_div)
        {
            return img_div.FindElement(By.XPath(".//a"));
        }

        //IWebElement imgTag = ImageLink.FindElement(By.XPath(".//img"));

        public IWebElement getInventoryImageTag(IWebElement ImageLink)
        {
            return ImageLink.FindElement(By.XPath(".//img"));
        }



        //var big_img = driver.FindElement(By.CssSelector("img[class='inventory_details_img']"));

        public IWebElement getInventoryDetailsImg(IWebDriver driver)
        {
            return driver.FindElement(By.CssSelector("img[class='inventory_details_img']"));
        }






    }
}
