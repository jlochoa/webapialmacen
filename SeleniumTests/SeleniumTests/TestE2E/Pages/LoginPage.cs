using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.TestE2E.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        // Elementos de la página

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement email;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "login1")]
        private IWebElement loginButton;

        public IWebElement getEmail()
        {
            return email;
        }

        public IWebElement getPassword()
        {
            return password;
        }

        public FamiliasPage validLogin(string user, string pass)
        {
            email.SendKeys(user);
            password.SendKeys(pass);
            loginButton.Click();
            return new FamiliasPage(driver);
        }
    }

}
