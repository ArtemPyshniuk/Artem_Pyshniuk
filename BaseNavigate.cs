using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium
{
    public class BaseNavigate
    {
 
        public IWebDriver driver;
        public string siteUrl;

        public BaseNavigate(string siteUrl)
        {
            driver = new ChromeDriver();
            this.siteUrl = siteUrl;
            //siteUrl = "https://www.automatetheplanet.com/test-automation-reporting-allure/";

            //ChromeOptions options = new ChromeOptions();

            //options.AddArgument("no-sandbox");

            //driver  = new ChromeDriver(options);
            //drv.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
        }

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(this.siteUrl);
        }


    }


}

