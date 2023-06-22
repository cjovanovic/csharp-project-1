using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    public class AlertsActionsAutoSuggestive
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

        }

        [Test]
        public void TestAlert()
        {
            String name = "Aleksandar";
            driver.FindElement(By.Id("name")).SendKeys("Aleksandar");
            driver.FindElement(By.CssSelector("input[onclick = 'displayConfirm()']")).Click();
            String alertText = driver.SwitchTo().Alert().Text;

            //accept -> confirm/yes -> all positive option buttons!
            driver.SwitchTo().Alert().Accept();
            //dismiss -> cancel/no -> all negative option buttons!
            //driver.SwitchTo().Alert().Dismiss();
            //other options
            //driver.SwitchTo().Alert().SendKeys("hello");

            StringAssert.Contains(name, alertText);
            driver.Quit();

        }

        [Test]
        public void AutosuggestiveDropdowns()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("ger");
            Thread.Sleep(3000);

            IList <IWebElement> options = driver.FindElements(By.CssSelector(".ui-menu-item div"));

            foreach(IWebElement option in options)
            {
                if(option.Text.Equals("Germany"))
                {
                    option.Click();
                    Thread.Sleep(3000);
                }
            }
            //For the print output runtime values will not be extracted
            //Za proveru dinamicki pass-ovane vrednosti ide "GetAttribute" umesto "Text", jer se ne refreshuje DOM!
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
            driver.Quit();
        }

        [Test]
        public void TestActions()
        {
            driver.FindElement(By.ClassName("logoClass")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Pass-ujemo argument "driver" klasi "Actions" da bi "a" objekat zadrzao takodje sve "driver" informacije - jako bitno!!!
            Actions a = new Actions(driver);

            //Za svaku akciju ("Actions") se dodaje (kokatenira) "Perform" metod da bi se ista izvrsila!
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            driver.FindElement(By.XPath("//ul[@class = 'dropdown-menu']/li[1]/a")).Click();
            //a.MoveToElement(driver.FindElement(By.XPath("//ul[@class = 'dropdown-menu']/li[1]/a"))).Click().Perform(); //drugi nacin - kao end user!
            driver.Quit();
        }

        [Test]
        public void TestDragDrop()
        {
            String name = "Dropped!";
            driver.Url = "https://demoqa.com/droppable";
            Actions a = new Actions(driver);
            a.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("droppable"))).Perform();
            Assert.That(name, Is.EqualTo("Dropped!"));
            driver.Quit();
        }

        [Test]
        public void Frames()
        {
            //scroll - mora da se doda js skriptica!
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            //iFrame se moze pretraziti preko id, name ili index!
            driver.SwitchTo().Frame("iframe-name");
            driver.FindElement(By.LinkText("All Access Plan")).Click();
            TestContext.Progress.WriteLine(driver.FindElement(By.XPath("//div[@class='inner-box']")).Text);
            //izlaz iz iframe-a i vracanje na default-nu stranu - zapamti mora izlaz!!!
            driver.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            driver.Quit();
        }
    }
}

