using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Reflection;
using System.Linq;

using MyAction;
namespace Accordion
{

    [TestFixture]
    class Tests
    {

        private IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        private WebDriverWait wait;

        private String Site = "https://demoqa.com";

        [SetUp]
        public void SetUp()
        {
            driver.Manage().Window.FullScreen();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Order(1)]
        public void SiteEnter()
        {
            driver.Navigate().GoToUrl(Site);
        }

        [Test, Order(2)]
        public void NavigateToAccordion()
        {
            IWebElement AccordionButton = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='sidebar']/aside[2]/ul/li[14]/a"));
            });

            AccordionButton.Click();
        }


        [Test, Order(3)]
        public void ClickTabTwoAndCheckContent()
        {            
            IWebElement AccordionTabTwo = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='ui-id-3']"));
            });

            AccordionTabTwo.Click();

            Thread.Sleep(3000);

            IWebElement AccordionTabTwoContentOne = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='ui-id-2']"));
            });

            IWebElement AccordionTabTwoContentTwo = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='ui-id-4']"));
            });

            IWebElement AccordionTabTwoContentThree = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='ui-id-6']"));
            });

            IWebElement AccordionTabTwoContentFour = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='ui-id-8']"));
            });

            Assert.IsTrue(AccordionTabTwoContentTwo.Displayed && AccordionTabTwoContentTwo.Enabled);
            Assert.IsFalse(AccordionTabTwoContentOne.Displayed && AccordionTabTwoContentOne.Enabled);
            Assert.IsFalse(AccordionTabTwoContentThree.Displayed && AccordionTabTwoContentThree.Enabled);
            Assert.IsFalse(AccordionTabTwoContentFour.Displayed && AccordionTabTwoContentFour.Enabled);
        }


    }
}
