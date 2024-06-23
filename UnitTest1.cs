using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SwagLab.POM;
using SwagLab.utilities;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;

namespace SwagLab
{
    [TestFixture]
    public class SwagLab
    {
        private IWebDriver driver;
        private readonly string baseUrl = "https://www.saucedemo.com/v1/";
        private readonly TimeSpan waitTime = TimeSpan.FromSeconds(2);

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        //----------------validate_login for valid user -----------------------------------

        private static string[] GetTestUsernames()
        {
            return new string[] { "standard_user", "locked_out_user", "problem_user", "performance_glitch_user" };
        }

        [TestCaseSource(nameof(GetTestUsernames))]
        public void verifyLogin_PositiveScenario(string username)  // verify login for different user
        {
            driver.Navigate().GoToUrl(baseUrl);

            WaitFor(waitTime);

            LoginPage loginPage = new LoginPage(driver);         //POM 
            InventoryPage inventoryPage = new InventoryPage(driver);  


            loginPage.getUserName().SendKeys(username);
            loginPage.getPassword().SendKeys("secret_sauce");

            WaitFor(waitTime);

            loginPage.getLoginButton().Click();
           

            string error_msg = "";
            if (driver.Url== baseUrl)
            {
                       
                error_msg=inventoryPage.getErrorMessage().Text;
            }
            
            Assert.That(driver.Url, Is.EqualTo(baseUrl + "inventory.html"), $"not able to logoin for username: {username} ,due to error: {error_msg}");

            //--------verify logo start--------------

            //IWebElement logoElement = driver.FindElement(By.ClassName("app_logo"));

            string logo_backgroundImage = inventoryPage.getLogoElement().GetCssValue("background-image");
            string logo_imageUrl = logo_backgroundImage.Replace("url(\"", "").Replace("\")", "");

            Assert.That(ImageUtils.IsImageLoaded(logo_imageUrl), Is.EqualTo(true), $"logo image not loaded");

            //-----------------end of verify logo ---------------

            WaitFor(waitTime);
        }




        //--------start validation for  invalid user----------------

        private static object[] GetTestUsernames_NegativeScenario()
        {
            return new object[]
            {
            new object[] { "standard_user", "invalidPass1" },             //  valid user name , invalid pass
            new object[] { "problem_user", "invalidPass2" },              // valid user name , invalid pass
            new object[] { "performance_glitch_user", "invalidPass3" },  // valid user name , invalid pass
            new object[] { "user1", "secret_sauce" },                    // invalid user, valid pass
            new object[] { "user2", "pass" },                            // invalid user, invalid pass
            

            };
        }

        [TestCaseSource(nameof(GetTestUsernames_NegativeScenario))]
        public void VerifyLogin_NegativeScenario(string username, string password)
        {
            driver.Navigate().GoToUrl(baseUrl);
            LoginPage loginPage = new LoginPage(driver); //POM
            loginPage.getUserName().SendKeys(username);
            loginPage.getPassword().SendKeys(password);
            loginPage.getLoginButton().Click();
            WaitFor(waitTime);

            IWebElement errorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']"));
            // Assert that the error message is displayed
            Assert.IsTrue(errorMessage.Displayed, "Error message is not displayed");
            // Assert the text content of the error message
            string expectedErrorMessage = "Epic sadface: Username and password do not match any user in this service";
            Assert.That(errorMessage.Text.Trim(), Is.EqualTo(expectedErrorMessage), $"Expected error message: '{expectedErrorMessage}', Actual: '{errorMessage.Text.Trim()}'");




        }


        //-------------end validation for  invalid user-----------


        //---------start of validate menu bar--------------


        private static string[] GetMenuItems()
        {
            return new string[] { "All Items", "About", "Logout", "Reset App State" };
        }

        [TestCaseSource(nameof(GetMenuItems))]
        public void verifyMenuBar(string menuItemName)
        {

            driver.Navigate().GoToUrl(baseUrl + "inventory.html");
            
            //IWebElement menuButton = driver.FindElement(By.CssSelector(".bm-burger-button"));
            InventoryPage inventoryPage = new InventoryPage(driver);
            WaitFor(waitTime);
            inventoryPage.getMenuButton().Click();
            WaitFor(waitTime);
            //IWebElement menuItem = driver.FindElement(By.XPath($"//a[contains(text(), '{menuItemName}')]"));

            inventoryPage.getMenuItem(menuItemName).Click();
            WaitFor(waitTime);

            if (menuItemName != "All Items")
            {

                Assert.That(driver.Url, ! Is.EqualTo(baseUrl), $"Menu Items without action");

            }







        }





        //------------------end of menubar logic--------------------

        




        //<<--------------add to cart---------->>
        [Test]
        public void VerifyAddToCart()
        {

            driver.Navigate().GoToUrl(baseUrl + "inventory.html");
            InventoryPage inventoryPage = new InventoryPage(driver);
            WaitFor(waitTime);

            int total_items = 0;

            //----------varify add to cart---------

            foreach (var item in inventoryPage.getItems())
            {
                item.Click();
                total_items++;
                WaitFor(waitTime);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", inventoryPage.getCart());
                WaitFor(waitTime);
                Assert.That(total_items, Is.EqualTo(inventoryPage.getNumberOfItemsINCart()), $"Not able to add to cart");


            }

            //----------remove from cart---------


            foreach (var item in inventoryPage.getItems())
            {
                item.Click();
                total_items--;
                WaitFor(waitTime);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", inventoryPage.getCart());
                WaitFor(waitTime);

                Assert.That(total_items, Is.EqualTo(inventoryPage.getNumberOfItemsINCart()), $"Not able to add to cart");

            }


        }






        [Test]
        public void VerifyShoppingCart()
        {

            driver.Navigate().GoToUrl(baseUrl + "inventory.html");
            InventoryPage inventoryPage = new InventoryPage(driver);
            CartPage cartPage = new CartPage(driver);
            
            int total_items = 0;

            foreach (var item in inventoryPage.getItems())
            {
                item.Click();
                total_items++;
                WaitFor(waitTime);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", inventoryPage.getCart());
                WaitFor(waitTime);
                Assert.That(total_items, Is.EqualTo(inventoryPage.getNumberOfItemsINCart()), $"Not able to add to cart");
            }


            inventoryPage.getCart().Click();
            WaitFor(waitTime);


            foreach (var item in inventoryPage.getRemoveItems())
            {
                item.Click();
                total_items--;
                WaitFor(waitTime);

                Assert.That(total_items, Is.EqualTo(inventoryPage.getNumberOfItemsINCart()), $"Not able to add to cart");

            

            }
            WaitFor(waitTime);

            cartPage.getContinueShopping().Click();
           
            WaitFor(waitTime);


        }


        [Test]
        public void VerifyCheckout()
        {

            driver.Navigate().GoToUrl(baseUrl + "inventory.html");
            InventoryPage inventoryPage = new InventoryPage(driver);
            CartPage cartPage= new CartPage(driver);

            foreach (var item in inventoryPage.getItems())
            {
                item.Click();

                WaitFor(waitTime);
               

            }

            inventoryPage.getCart().Click();

            WaitFor(waitTime);

            cartPage.getCheckOut().Click();
            WaitFor(waitTime);
            cartPage.getFirstName().SendKeys("brij");
            cartPage.getLastName().SendKeys("gupta");
            cartPage.getPostalCode().SendKeys("12345");
            WaitFor(waitTime);
            cartPage.getSubmitInfo().Click();

            WaitFor(waitTime);

           
            cartPage.getFinishShopping().Click();
            WaitFor(waitTime);
           


        }

        [Test]
        public void verifySorting()
        {
            driver.Navigate().GoToUrl(baseUrl + "inventory.html");

            WaitFor(waitTime);

            IWebElement selectOptions = driver.FindElement(By.ClassName("product_sort_container"));


            //-------verify sorting in ascending order(a to z)------
            selectOptions.Click();
            WaitFor(waitTime);
            var az = driver.FindElement(By.CssSelector("option[value='az']"));
            az.Click();
            selectOptions.Click();
            IList<IWebElement> productElements = driver.FindElements(By.CssSelector(".inventory_item_name"));
            List<string> productNames = productElements.Select(e => e.Text).ToList();
            Assert.That(SortingChecker.IsSortedAscending(productNames), Is.EqualTo(true), $"product are not in ascending order");

            //------- end of  sorting in ascending order(a to z) ------

            WaitFor(waitTime);

            //-------verify sorting in decending order(z to a)------
            selectOptions.Click();
            WaitFor(waitTime);
            var za = driver.FindElement(By.CssSelector("option[value='za']"));
            za.Click();
            selectOptions.Click();

            IList<IWebElement> productElements_za = driver.FindElements(By.CssSelector(".inventory_item_name"));
            List<string> productNames_za = productElements_za.Select(e => e.Text).ToList();

            WaitFor(waitTime);
            Assert.That(SortingChecker.IsSortedDescending(productNames_za), Is.EqualTo(true), $"product are not in decending order");

            //------- end of  sorting in decending order(a to z) ------

            WaitFor(waitTime);

            //-------verify how to high price(1 to 999..)------
            selectOptions.Click();
            WaitFor(waitTime);
            var lohi = driver.FindElement(By.CssSelector("option[value='lohi']"));
            lohi.Click();
            selectOptions.Click();

            IList<IWebElement> productElement_lohi = driver.FindElements(By.CssSelector(".inventory_item_price"));
            List<string> productPrice_lohi = productElement_lohi.Select(e => e.Text).ToList();
            List<float> floatPrices_lohi = productPrice_lohi.Select(p => float.Parse(p.TrimStart('$'))).ToList();

            WaitFor(waitTime);
            Assert.That(SortingChecker.IsLowToHigh(floatPrices_lohi), Is.EqualTo(true), $"product are not in low to high price");

            //------- end of  verify low to high price (999.. to 1) ------

            WaitFor(waitTime);


            //-------verify High to Low price(1 to 999..)------
            selectOptions.Click();
            WaitFor(waitTime);
            var hilo = driver.FindElement(By.CssSelector("option[value='hilo']"));
            hilo.Click();
            selectOptions.Click();

            IList<IWebElement> productElement_hilo = driver.FindElements(By.CssSelector(".inventory_item_price"));
            List<string> productPrice_hilo = productElement_hilo.Select(e => e.Text).ToList();
            List<float> floatPrices_hilo = productPrice_hilo.Select(p => float.Parse(p.TrimStart('$'))).ToList();

            WaitFor(waitTime);
            Assert.That(SortingChecker.IsHightoLow(floatPrices_hilo), Is.EqualTo(true), $"product are not in high to low  price");

            //------- end of  verify high to low  price (999.. to 1) ------




            
            WaitFor(waitTime);
            WaitFor(waitTime);
            WaitFor(waitTime);




        }


        //------------verify images --------




        [Test]
        public void veryImage()
        {
            driver.Navigate().GoToUrl(baseUrl + "inventory.html");
            InventoryPage inventoryPage=new InventoryPage(driver);
            InventoryItemPage inventoryItemPage=new InventoryItemPage(driver);
           

            int NumberOfItems = inventoryPage.getInventoryItems().Count;

            int totalItems = 0;  // current items in cart
            Random random = new Random(); // for provability

            
            for (int i = 0; i < NumberOfItems; i++)
            {
                IWebElement img_div = inventoryPage.getInventoryItems()[i];
                IWebElement ImageLink=inventoryPage.getInventoryImageLink(img_div);
                IWebElement imgTag =inventoryPage.getInventoryImageTag(ImageLink);
                Assert.That(ImageUtils.IsImageLoaded(imgTag), Is.EqualTo(true), $"image not loaded");  //check image is loaded or not

                WaitFor(waitTime);
                imgTag.Click();

                Assert.That(ImageUtils.IsImageLoaded(inventoryPage.getInventoryDetailsImg(driver)), Is.EqualTo(true), $"image not loaded");  //check image is loaded or not

                var item = inventoryItemPage.getItem(driver);

                WaitFor(waitTime);

                // add to cart logic -----------
                item.Click();
                totalItems++;
                Assert.That(item.Text, Is.EqualTo("REMOVE"), $"add to cart not changed to remove ");  // verify AddTo cart cange to remove
                                                                                                      //--------------end of add to cart logic ----------------------

                WaitFor(waitTime);


                // -------------remove from cart logic -------------------

                int p = random.NextDouble() < 0.4 ? 0 : 1;
                if (p == 1)
                {

                    item.Click();
                    totalItems--;
                    Assert.That(item.Text, Is.EqualTo("ADD TO CART"), $"remove not changed to add to cart");  // verify remove change to add to cart
                    WaitFor(waitTime);
                }
                // -----------------end of remove from cart logic --------------------





                //--- verify number of items in cart logic------------


                var Ncart = driver.FindElement(By.CssSelector("a[href=\"./cart.html\"]:last-child"));

                int n = Ncart.Text == "" ? 0 : int.Parse(Ncart.Text);

                Assert.That(n, Is.EqualTo(totalItems), $"problem with cart add, remove");
                //------------------end of number of items in cart logic----------------------


                var back = driver.FindElement(By.CssSelector("button[class='inventory_details_back_button']"));

                back.Click();
                driver.Navigate().Refresh();  //refresh ..




            }

            WaitFor(waitTime);


        }



        //------------ end of verify images---------------












        // Helper method to wait for a specified amount of time
        private void WaitFor(TimeSpan timeSpan)
        {
            Thread.Sleep(timeSpan);
        }
    }
}