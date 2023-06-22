using System;
using System.Configuration;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace CSharpSelFramework.utilities
{
    public class Base
    {
        //public IWebDriver driver;

        //saving the thread by creating the 'driver' as a separate instance -> for the proper execution of parallel tests!
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>(); // TL has to know what is the return type -> IWebDriver!
                                                                               // IWebDriver -> generic return reference!
        public ExtentReports extent;
        public ExtentTest test;
        String browserName;

        //report file
        [OneTimeSetUp] //execute as first before setup or child test and ONLY ONCE in entire project!
        public void Setup()
        {
            String workingDirectory = Environment.CurrentDirectory;
            String projectDirecory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirecory + "index.html";
            var htmlReporter =  new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Aleksandar Jovanovic");
        }


        [SetUp] //execute every time before child test!
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name); //get the name of every test dinamically!
                                                                            //object of entry "test" - reporting on that, particular "test" object! 
            //Configuration - set up to be able to send global vars (e.g. browserName) via CLI
            browserName = TestContext.Parameters["browserName"];
            if(browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
                InitBrowser(browserName);
            }

            //CLI command: dotnet test CSharpSelFramework.csproj --filter TestCategory=Smoke -- TestRunParameters.Parameter\(name=\"browserName\",value=\"Firefox\"\)

            //Implicit wait (5sec default) can be declared globally - globalan wait -> ceka 5sec pre izvrsavanja svakog step-a!
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        //get driver method!
        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        //Factory design pattern
        public void InitBrowser(String browserName)
        {
            switch(browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    driver.Value = new ChromeDriver();
                    break;

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                    driver.Value = new FirefoxDriver();
                    break;

                case "Edge":
                    driver.Value = new EdgeDriver();
                    break;

                case "Safari":
                    driver.Value = new SafariDriver();
                    break;
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void afterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace; //error log


            //timestamp for the file name
            DateTime time = DateTime.Now;
            String fileName = "Screenshot " + time.ToString("h_mm_ss") + ".png";

            if(status == TestStatus.Failed)
            {
                //we need explicitly to say that the test is failed - by default will always be reported as passed!
                test.Fail("Test failed!", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace); //log is attached only to test entry
            }
            else if(status == TestStatus.Passed)
            {

            }

            //main primary class object need to be cleaned/flushed in order to be ready for the next execution
            extent.Flush();
            driver.Value.Quit();
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName) //dynamic solution!
                                                                           //all drivers that belong to specific test will come in runtime
        {
            //switching driver mode to screenshot mode
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            //converting base64 format to compatible Media entity via Builder option!
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();

        }
    }
}

