using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.TestE2E.Pages
{
    public class FamiliasPage
    {
        private IWebDriver driver;

        public FamiliasPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        // Elementos de la página

        [FindsBy(How = How.Id, Using = "nombre")]
        private IWebElement nombre;

        [FindsBy(How = How.Id, Using = "aceptar")]
        private IWebElement aceptarButton;
        
        public IWebElement getNombre()
        {
            return nombre;
        }


        public void agregarFamilia(string nombreFamilia)
        {
            nombre.SendKeys(nombreFamilia);
            aceptarButton.Click();
        }
    }

}
