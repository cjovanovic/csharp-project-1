using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.pageObjects
{
    public class ProductsPage
    {


        IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a"); //BY - only declaring the locator - not concatenate to the driver!

        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList <IWebElement> cards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        public void waitForPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public IList <IWebElement> getCards()
        {
            return cards;
        }

        public By getCardTitle()
        {
            return cardTitle;
        }

        public CheckoutPage checkout()
        {
            checkoutButton.Click();
            return new CheckoutPage(driver); //whenever any method makes us to land on a new page we need to create new class object for the mentioned page!
        }

        
    }
}

