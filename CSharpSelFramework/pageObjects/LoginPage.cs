using System;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
namespace CSharpSelFramework.pageObjects
{
    public class LoginPage
    {
        //driver.FindElement(By.Id("username"))

        //method default constructor!
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver; //assign to the local driver!
            PageFactory.InitElements(driver, this); // "this" reffers to the current class object!
        }

        //Pageobject factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username; //assign to the private variable
                                      //set private to avoid possible var modifing by calling fields directly and causing failed tests!
                                      //for that reason we are setting and using method!
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement adminRadio;

        [FindsBy(How = How.CssSelector, Using = "input[value='Sign In']")]
        private IWebElement signInButton;

        [FindsBy(How = How.ClassName, Using = "alert-danger")]
        private IWebElement errorMessage;

        [FindsBy(How = How.LinkText, Using = "Free Access to InterviewQues/ResumeAssistance/Material")]
        private IWebElement link;

        //One way -> valid login method -> all in one!
        public ProductsPage validLogin(String user, String pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            adminRadio.Click();
            signInButton.Click();
            return new ProductsPage(driver); //give the object of the next page!
                                             //catching the object and continue execution the test!
        }

        //Another way -> all separate!
        //public IWebElement getUserName()
        //{
        //    return username;
        //}

        //public IWebElement getPassword()
        //{
        //    return password;
        //}

        //public IWebElement getAdminRadio()
        //{
        //    return adminRadio;
        //}


        //public IWebElement getSignInButton()
        //{
        //    return signInButton;
        //}

    }
}

