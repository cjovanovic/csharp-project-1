using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    public class E2ETest
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
        public void EndToEndFlow()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            Actions a = new Actions(driver);

            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
            driver.FindElement(By.XPath("//input[@value=\"Sign In\"]")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList <IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach(IWebElement product in products)
            {
                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);

                if(expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
            }

            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            IList <IWebElement> checkoutCards = driver.FindElements(By.CssSelector("h4 a"));

            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
            //Assert.AreEqual(expectedProducts, actualProducts);
            driver.FindElement(By.ClassName("btn-success")).Click();
            driver.FindElement(By.Id("country")).SendKeys("ser");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Serbia")));
            driver.FindElement(By.LinkText("Serbia")).Click();
            a.MoveToElement(driver.FindElement(By.XPath("//div/input[@type='checkbox']"))).Click().Perform();
            driver.FindElement(By.CssSelector("input[value='Purchase']")).Click();
            String confirmedText =  driver.FindElement(By.ClassName("alert-success")).Text;
            StringAssert.Contains("Success!", confirmedText);
            driver.Quit();
        }
    }
}
