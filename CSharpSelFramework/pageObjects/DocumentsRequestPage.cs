using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.pageObjects
{
    public class DocumentsRequestPage
    {
        IWebDriver driver;

        public DocumentsRequestPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "inner-box")]
        private IWebElement title;

        public IWebElement getTitle()
        {
            return title;
        }
    }
}

