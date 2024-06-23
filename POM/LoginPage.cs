using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace SwagLab.POM
{
    //IWebElement usernameField = driver.FindElement(By.Id("user-name"));
    //IWebElement passwordField = driver.FindElement(By.Id("password"));
    //IWebElement loginButton = driver.FindElement(By.Id("login-button"));
    internal class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver) { 
        
        this.driver = driver;
        PageFactory.InitElements(driver,this);
        
        }

        //page factorty
        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement username;
        public IWebElement getUserName()
        {

            return username;
        }



        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;
        public IWebElement getPassword()
        {

            return password;
        }


        [FindsBy(How = How.Id, Using = "login-button")]
        private IWebElement loginbutton;
        public IWebElement getLoginButton()
        {

            return loginbutton;
        }





    }
 

}
