using System;
using System.Collections;
using System.Reflection.Emit;
using System.Xml.Linq;
using AngleSharp.Dom;
using CSharpSelFramework.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumLearning
{
    //[Parallelizable(ParallelScope.Self)]
    public class SortWebTables : Base
    {

        [Test]
        public void SortTable()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";

            ArrayList a = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.Value.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            //step 1 - Get all veggie names into arraylist A
            IList <IWebElement> veggies = driver.Value.FindElements(By.XPath("//tr//td[1]"));

            foreach(IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            //step 2 - Sort this arraylist A
            foreach (String element in a)
            {
                TestContext.Progress.WriteLine(element);
            }

            TestContext.Progress.WriteLine("After sorting!!!");
            a.Sort();

            foreach(String element in a)
            {
                TestContext.Progress.WriteLine(element);
            }

            // CSS selector -> koristi "*" za trazenje po delu imena labele:
            // th[aria-label *= 'fruit name']

            // Xpath -> koristi "contains" za trazenje po delu imena labele:
            // //th[contains(@aria-label, 'fruit name')]

            //step 3 - click into column to sort
            driver.Value.FindElement(By.CssSelector("th[aria-label*='fruit name']")).Click();

            //step 4 - Get all veggie names into arraylist B
            ArrayList b = new ArrayList();

            IList <IWebElement> sortedVeggies = driver.Value.FindElements(By.XPath("//tr//td[1]"));

            foreach(IWebElement veggie in sortedVeggies)
            {
                b.Add(veggie.Text);
            }

            // check arraylist A to B = equal
            Assert.AreEqual(a, b);
        }
    }
}

