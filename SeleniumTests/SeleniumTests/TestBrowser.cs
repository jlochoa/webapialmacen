using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTests
{
    public class TestBrowser
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Selenium se comunica con el driver de Chrome mediante el driver
            // ChromeDriver chromeDriver = new ChromeDriver();
            // Mediante WebDriverManager, Selenium emplea "al vuelo" el driver del navegador según su versión y configuración
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            // new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new ChromeDriver();
            // driver = new FirefoxDriver();

            driver.Manage().Window.Maximize();

        }

        //[Test]
        //public void Test1()
        //{
        //    driver.Url = "https://elpais.com/";
        //    TestContext.Progress.WriteLine(driver.Title);
        //    TestContext.Progress.WriteLine(driver.Url);
        //    driver.Close();
        //}

        [Test]
        public void Test()
        {
            driver.Url = "http://localhost:4200/login";
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys("jl@gmail.com");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("123456");
            // driver.FindElement(By.ClassName("btn-lg")).Click();
            IWebElement button = driver.FindElement(By.Id("login1"));
            button.Click();
            driver.Close();
        }


        [TearDown]
        public void EndTest()
        {
            driver.Dispose();
        }
    }

}
