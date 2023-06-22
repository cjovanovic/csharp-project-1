using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    public class FunctionalTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            //chrome browser
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();

            //Implicit wait (5sec default) can be declared globally - globalan wait -> ceka 5sec pre izvrsavanja svakog step-a!
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        [Test]
        public void dropdown()
        {

            IWebElement dropdown = driver.FindElement(By.CssSelector("select.form-control"));

            //staticki select elemenata!
            SelectElement s = new SelectElement(dropdown);
            s.SelectByText("Teacher");
            s.SelectByValue("consult");
            s.SelectByIndex(1);

            //lista elemenata - plural
            IList<IWebElement> rdos = driver.FindElements(By.CssSelector("input[type='radio']"));

            //zakucan radio -> koristeci index!
            //rdos[1].Click();

            //moguce umetnut novi radio -> nadji odgovarajuci element dinamicki!
            foreach (IWebElement radioButton in rdos)
            {
                if (radioButton.GetAttribute("value").Equals("user"))
                {
                    radioButton.Click();
                }
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
            .ElementToBeClickable(By.Id("okayBtn")));

            driver.FindElement(By.Id("okayBtn")).Click();
            Boolean result = driver.FindElement(By.Id("usertype")).Selected;
            Assert.That(result, Is.False);
        }
    }

}