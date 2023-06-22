using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    public class WindowHandlers
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void WindowHandle()
        {
            String email = "mentor@rahulshettyacademy.com";
            String parentWindowId = driver.CurrentWindowHandle; //definisanje parent prozora pre switch-ovanja na child prozor!

            driver.FindElement(By.ClassName("blinkingText")).Click();

            //assert- jedan nacin!
            Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));

            String childWindowName = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);

            //Please email us at mentor@rahulshettyacademy.com with below template to receive response

            String text = driver.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = text.Split("at"); //split-ovan tekst ide u niz!

            //Please email us at <-> mentor@rahulshettyacademy.com with below template to receive response
            //splittedText[0]        splittedText[1]

            String[] trimmedSplittedText = splittedText[1].Trim().Split(" "); //trimuje se da se ukloni prvi space!

            //assert- drugi nacin!
            Assert.AreEqual(email, trimmedSplittedText[0]);

            driver.SwitchTo().Window(parentWindowId);
            driver.FindElement(By.Id("username")).SendKeys(trimmedSplittedText[0]);
            driver.Quit();
        }

    }
}

