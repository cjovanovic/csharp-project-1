using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    public class Locators
    {
        // Xpath, Css, classname, id, name, tagname, linktext

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
        public void LocatorsIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.FindElement(By.Name("password")).SendKeys("123456");


            // CSS selector & xpath
            // CSS: tagname[attribute = 'value'] -> tagname=input attribute=id/name/class value="signInBtn"
            // #id #terms - classname -> CSS .classname
            //driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            // xpath: //tagname[@attribute = 'value'] -> razlika u odnosu na css samo comment tag i @!!!!
            // CSS -> .text-info span:nth-child(1) input
            // xpath -> //label[@class='text-info']/span/input
            driver.FindElement(By.XPath("//input[@value = 'Sign In']")).Click();

            //Explicit wait can be a need to be declared for an element -> koristi se obicno za dodatni wait kod ucitavanja odredjenog elementa!
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
           .TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));

            //Thread.Sleep(3000); - zakucan wait -> nije dobra praksa!
            String errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttr = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";

            Assert.AreEqual(hrefAttr, expectedUrl);

            //Validate url of the link text
        }
    }
}

