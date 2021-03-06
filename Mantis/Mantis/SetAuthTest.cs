// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class SetAuthTest
{
    private IWebDriver driver;
    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;
    [SetUp]
    public void SetUp(int type, string adminUsername, string adminPassword)
    {
        if (type == 0)
        {
            driver = new ChromeDriver(@"C:\Users\vdcuo\OneDrive\Desktop\Nam 4\Kiem thu phan mem\BTCN10\1712314-Selenium\1712314-Source\Mantis\Mantis\bin\Debug\", new ChromeOptions(), TimeSpan.FromMinutes(5));
        }
        else
        {
            driver = new FirefoxDriver(@"C:\Users\vdcuo\OneDrive\Desktop\Nam 4\Kiem thu phan mem\BTCN10\1712314-Selenium\1712314-Source\Mantis\Mantis\bin\Debug\", new FirefoxOptions(), TimeSpan.FromMinutes(5));
        }
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
        // Login
        driver.Navigate().GoToUrl("http://127.0.0.1/mantis/login_page.php");
        driver.FindElement(By.Id("username")).Click();
        driver.FindElement(By.Id("username")).SendKeys(adminUsername);
        driver.FindElement(By.CssSelector(".width-40")).Click();
        driver.FindElement(By.Id("password")).SendKeys(adminPassword);
        driver.FindElement(By.CssSelector(".width-40")).Click();
    }
    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }
    [Test]
    public void setAuth(string username, string role)
    {
        driver.FindElement(By.XPath("//div[@id=\'main-container\']/div[2]/div[2]/div/ul/li[2]/a")).Click();
        IReadOnlyCollection<IWebElement> userList  = driver.FindElements(By.XPath("//*[@id=\"main-container\"]/div[2]/div[2]/div/div/div[4]/div[2]/div[2]/div/table/tbody/tr"));
        foreach(IWebElement webEl in userList)
        {
            IWebElement userTag = webEl.FindElement(By.XPath(".//td[1]/a"));
            if (userTag.Text.Equals(username))
            {
                    userTag.Click();
                driver.FindElement(By.Id("edit-access-level")).Click();
                {
                    var dropdown = driver.FindElement(By.Id("edit-access-level"));
                    dropdown.FindElement(By.XPath("//option[. = '" + role + "']")).Click();
                }
                driver.FindElement(By.Id("edit-access-level")).Click();
                driver.FindElement(By.CssSelector(".widget-box:nth-child(1) .btn")).Click();
                break;
            }
        }
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }
}
