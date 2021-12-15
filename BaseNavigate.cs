using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumHomework
{
    public class BaseNavigate
    {

        public IWebDriver driver;
        public string siteUrl;

        public BaseNavigate(string siteUrl)
        {
            driver = new ChromeDriver();
            this.siteUrl = siteUrl;
        }

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(this.siteUrl);
        }

        public void CloseSite()
        {
            driver.Quit();
        }

        public bool IsOpenSite()
        {
            if (driver.Url.Contains("index.php"))
                return true;
            return false;
        }

    }


}