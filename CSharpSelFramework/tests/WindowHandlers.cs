using System;
using CSharpSelFramework.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    //[Parallelizable(ParallelScope.Self)]
    public class WindowHandlers : Base
    {

        [Test]
        public void WindowHandle()
        {
            String email = "mentor@rahulshettyacademy.com";
            String parentWindowId = driver.Value.CurrentWindowHandle; //definisanje parent prozora pre switch-ovanja na child prozor!

            driver.Value.FindElement(By.ClassName("blinkingText")).Click();

            //assert- jedan nacin!
            Assert.That(driver.Value.WindowHandles.Count, Is.EqualTo(2));

            String childWindowName = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector(".red")).Text);

            //Please email us at mentor@rahulshettyacademy.com with below template to receive response

            String text = driver.Value.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = text.Split("at"); //split-ovan tekst ide u niz!

            //Please email us at <-> mentor@rahulshettyacademy.com with below template to receive response
            //splittedText[0]        splittedText[1]

            String[] trimmedSplittedText = splittedText[1].Trim().Split(" "); //trimuje se da se ukloni prvi space!

            //assert- drugi nacin!
            Assert.AreEqual(email, trimmedSplittedText[0]);

            driver.Value.SwitchTo().Window(parentWindowId);
            driver.Value.FindElement(By.Id("username")).SendKeys(trimmedSplittedText[0]);
        }

    }
}

