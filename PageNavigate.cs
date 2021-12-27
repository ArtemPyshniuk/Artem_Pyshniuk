using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    public class PageNavigate : BaseNavigate
    {
        public PageNavigate(string siteUrl) : base(siteUrl) { }

        public void Login()
        {
            string spanText = this.driver.FindElement(By.CssSelector("#content > div:nth-child(3) > span")).Text;

            (string, string) tuple = ParseLoginData(spanText);
            string username = tuple.Item1;
            string password = tuple.Item2;

            var usernameLogPanel = this.driver.FindElement(By.Id("txtUsername"));
            usernameLogPanel.SendKeys(username);
            var passwordLogPanel = this.driver.FindElement(By.Id("txtPassword"));
            passwordLogPanel.SendKeys(password);
            var loginButt = this.driver.FindElement(By.Id("btnLogin"));
            loginButt.Click();
        }

        public (string, string) ParseLoginData(string spanText)
        {
            string[] words = spanText.Split(' ');
            string username = "";
            string password = "";

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "Username")
                    username = words[i + 2];
                if (words[i] == "Password")
                    password = words[i + 2];
            }

            return (username, password);
        }

        public void AddUser(string role, string empl, string username, string password)
        {
            this.driver.FindElement(By.Id("menu_admin_viewAdminModule")).Click();
            this.driver.FindElement(By.Id("btnAdd")).Click();
            var dropDown = this.driver.FindElement(By.Id("systemUser_userType"));
            new SelectElement(dropDown).SelectByText(role);
            this.driver.FindElement(By.Id("systemUser_employeeName_empName")).SendKeys(empl);
            this.driver.FindElement(By.Id("systemUser_userName")).SendKeys(username);
            this.driver.FindElement(By.Id("systemUser_password")).SendKeys(password);
            this.driver.FindElement(By.Id("systemUser_confirmPassword")).SendKeys(password);

            this.driver.FindElement(By.Id("btnSave")).Click();
            System.Threading.Thread.Sleep(30000);

        }

        public bool CheckUser(string username)
        {
            if (this.driver.PageSource.Contains(username))
                return true;
            return false;
        }


        public void FindUser(string username)
        {
            this.driver.FindElement(By.Name("searchSystemUser[userName]")).SendKeys(username);
            this.driver.FindElement(By.Id("searchBtn")).Click();

        }

        public void ResetAfterFind()
        {
            this.driver.FindElement(By.Id("resetBtn")).Click();

        }

        public void DeleteUser(string username)
        {
            string str = this.driver.FindElement(By.LinkText(username)).GetAttribute("href");
            string[] Str = str.Split('=');
            this.driver.FindElement(By.Id("ohrmList_chkSelectRecord_" + Str[1])).Click();
            this.driver.FindElement(By.Id("btnDelete")).Click();
            this.driver.FindElement(By.Id("dialogDeleteBtn")).Click();
        }
    }
}
