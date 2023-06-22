using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.pageObjects
{
    public class PurchasePage
    {
        IWebDriver driver;

        public PurchasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement country;

        [FindsBy(How = How.LinkText, Using = "Serbia")]
        private IWebElement countryName;

        [FindsBy(How = How.XPath, Using = "//div/input[@type='checkbox']")]
        private IWebElement checkbox;

        [FindsBy(How = How.CssSelector, Using = "input[value='Purchase']")]
        private IWebElement purchaseButton;

        [FindsBy(How = How.ClassName, Using = "alert-success")]
        private IWebElement success;

        public IWebElement getCountry()
        {
            return country;
        }

        public void waitForCountryDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Serbia")));
        }

        public IWebElement getCountryName()
        {
            return countryName;
        }

        public IWebElement getCheckbox()
        {
            return checkbox;
        }

        public IWebElement getPurchaseButton()
        {
            return purchaseButton;
        }

        public IWebElement getSuccessString()
        {
            return success;
        }
    }
}

