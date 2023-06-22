using System;
using CSharpSelFramework.pageObjects;
using CSharpSelFramework.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    //[Parallelizable(ParallelScope.Self)] // parallel on project level -> complete test suite from one folder!
    [Parallelizable(ParallelScope.Children)] // parallel on class level!
    public class E2ETest : Base
    {
        // data set globally on annotation level!
        [Test, TestCaseSource("AddTestDataConfig"), Category("Regression")]
        //[TestCase("rahulshettyacademy", "learning")] // parameterization with multilple test sets
        //[TestCase("rahulshetty", "learn")]           // add new Test Case attribute/annotation with the different data
        // another test set!


        // run all data sets of Test method in parallel - ParallelScope.All
        // run all Test methods in one class parallel - ParallelScope.Children
        // run all test files (in all classes) in project parallel - ParallelScope.Self

        // dotnet test pathto.csproj (Run All Tests!)
        // dotnet test pathto.csproj --filter TestCategory=Smoke //CLI command: dotnet test CSharpSelFramework.csproj --filter TestCategory=Smoke -- TestRunParameters.Parameter\(name=\"browserName\",value=\"Firefox\"\)


        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(String username, String password, String[] expectedProducts)
        {
            //String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            Actions a = new Actions(driver.Value);

            LoginPage loginPage = new LoginPage(getDriver()); //create loginPage object of class LoginPage to use getUserName method!

            //One way -> valid login method -> all in one!
            ProductsPage productPage = loginPage.validLogin(username, password);
            productPage.waitForPageDisplay();

            //Another way -> all separate!
            //loginPage.getUserName().SendKeys("rahulshettyacademy");
            //loginPage.getPassword().SendKeys("learning");
            //loginPage.getAdminRadio().Click();
            //loginPage.getSignInButton().Click();

            IList<IWebElement> products = productPage.getCards();


            foreach (IWebElement product in products)
            {
                
                if (expectedProducts.Contains(product.FindElement(productPage.getCardTitle()).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                TestContext.Progress.WriteLine(product.FindElement(productPage.getCardTitle()).Text);
            }

            CheckoutPage checkoutPage = productPage.checkout(); //checkout button from Product page!
            IList<IWebElement> checkoutCards = checkoutPage.getCards();

            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
            //Assert.AreEqual(expectedProducts, actualProducts);

            PurchasePage purchasePage = checkoutPage.checkOut(); //checkout button from Checkout page!

            purchasePage.getCountry().SendKeys("ser");
            purchasePage.waitForCountryDisplay();
            purchasePage.getCountryName().Click();
            a.MoveToElement(purchasePage.getCheckbox()).Click().Perform();
            purchasePage.getPurchaseButton().Click();

            String confirmedText = purchasePage.getSuccessString().Text;
            StringAssert.Contains("Success!", confirmedText);

        }

        [Test, Category("Smoke")]
        public void LocatorsIdentification(String username_wrong, String password_wrong)
        {

            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.Value.FindElement(By.Name("username")).Clear();
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.Value.FindElement(By.Name("password")).SendKeys("123456");
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
            driver.Value.FindElement(By.XPath("//input[@value = 'Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
           .TextToBePresentInElementValue(driver.Value.FindElement(By.Id("signInBtn")), "Sign In"));

            String errorMessage = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            IWebElement link = driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttr = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";
            Assert.AreEqual(hrefAttr, expectedUrl);

        }

        // wrapping multiple test sets into one method and called via TestCaseSource!
        public static IEnumerable <TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }
}
