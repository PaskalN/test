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
namespace Resizable
{

    [TestFixture]
    class Test
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
        public void NavigateToResizable()
        {
            IWebElement ResizableMenu = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='sidebar']/aside[1]/ul/li[3]/a"));
            });

            ResizableMenu.Click();
        }
        
        [Test, Order(3)]
        public void ResizeBlock()
        {        
            IWebElement uiResizableButton = wait.Until((d) => {
                return d.FindElement(By.CssSelector("#resizable > div.ui-resizable-handle.ui-resizable-se.ui-icon.ui-icon-gripsmall-diagonal-se"));
            });

            IWebElement ResizableBox = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='resizable']"));
            });

            Actions actions = new Actions(driver);
            actions.ClickAndHold(uiResizableButton).MoveByOffset(167, 167).Release().Build().Perform();
            
            Assert.IsTrue(ResizableBox.Size.Width == 300);
            Assert.IsTrue(ResizableBox.Size.Height == 300);
        }
    }
}
