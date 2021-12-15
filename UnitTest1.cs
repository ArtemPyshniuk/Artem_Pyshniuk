using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumHomework
{
    [TestFixture()]
    public class Test
    {
        public static Random rand = new Random();
        public static int value = rand.Next();
        public static string username = "ArtemPyshniuk" + Convert.ToString(value);
        PageNavigate pageNavigate = new PageNavigate("https://opensource-demo.orangehrmlive.com/");

        [Test()]
        public void TestCase1AddUser()
        {

            if (pageNavigate.IsOpenSite())
            {
                pageNavigate.driver.FindElement(By.Id("menu_admin_viewAdminModule")).Click();
            }

            else
            {
                pageNavigate.OpenSite();
                pageNavigate.Login();
            }

            pageNavigate.AddUser("ESS", "Harry Kane", username, "ArtemPyshniuk_25");
            
            Assert.IsTrue(pageNavigate.CheckUser(username));

        }
        [Test()]
        public void TestCase2FindUser()
        {

            if (pageNavigate.IsOpenSite())
            {
                pageNavigate.driver.FindElement(By.Id("menu_admin_viewAdminModule")).Click();
            }

            else
            {
                pageNavigate.OpenSite();
                pageNavigate.Login();
            }

            pageNavigate.FindUser(username);
            Assert.IsTrue(pageNavigate.CheckUser(username));
            Assert.IsTrue(pageNavigate.CheckUser("ESS"));
            Assert.IsTrue(pageNavigate.CheckUser("Harry Kane"));
            Assert.IsTrue(pageNavigate.CheckUser("Enabled"));
            pageNavigate.ResetAfterFind();
            Assert.IsTrue(pageNavigate.CheckUser(username));

           
        }
        [Test()]
        public void TestCase3DeleteUser()
        {
            if (pageNavigate.IsOpenSite())
            {
                pageNavigate.driver.FindElement(By.Id("menu_admin_viewAdminModule")).Click();
            }

            else
            {
                pageNavigate.OpenSite();
                pageNavigate.Login();
            }

            pageNavigate.DeleteUser(username);
            Assert.IsFalse(pageNavigate.CheckUser(username));

        }
    }
}