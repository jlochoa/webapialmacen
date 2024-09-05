using SeleniumTests.TestE2E.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.TestE2E
{
    public class Tester : Browser
    {
        [Test]
        public void Test()
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.getEmail().Clear();
            loginPage.getPassword().Clear();
            //loginPage.getUsuario().SendKeys("jl@gmail.com");
            //loginPage.getPassword().SendKeys("123456");
            var familiasPage = loginPage.validLogin("jl@gmail.com", "123456");
            familiasPage.getNombre().Clear();
            familiasPage.agregarFamilia("Tablets");
        }
    }
}
