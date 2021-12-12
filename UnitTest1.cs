using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace proj
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
            
            pageNavigate.OpenSite();
            pageNavigate.Login();
            pageNavigate.AddUser("ESS", "Harry Kane", username, "ArtemPyshniuk_25");
            Assert.IsTrue(pageNavigate.CheckUser(username));
        }
        [Test()]
        public void TestCase2FindUser()
        {
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
            pageNavigate.DeleteUser(username);
            Assert.IsFalse(pageNavigate.CheckUser(username));
        }
    }
}
