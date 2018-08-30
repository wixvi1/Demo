using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class BrowserControl : Form
    {
        IWebDriver Browser;
        public BrowserControl()
        {
            InitializeComponent();
        }

        private void OpenBrowser(object sender, EventArgs e)
        {
            Browser = new ChromeDriver();
            Browser.Navigate().GoToUrl("http://google.com");//Go to google;
            Browser.FindElement(By.Id("lst-ib")).SendKeys("Lego" + OpenQA.Selenium.Keys.Enter);//Find needed element and enter text, then click enter
        }

        private void CloseBrowser(object sender, EventArgs e)
        {
            Browser.Quit();//Closes browser
        }

        private void Test(object sender, EventArgs e)
        {
            Browser = new ChromeDriver();
            Browser.Navigate().GoToUrl("http://yandex.ru");
            Browser.FindElement(By.ClassName("desk-notif-card__login-enter-expanded")).Click();
            Browser.FindElement(By.Name("login")).SendKeys("Zodiacasti");
            Browser.FindElement(By.Name("passwd")).SendKeys("princpersii");
            Browser.FindElement(By.ClassName("passport-Button")).Click();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Browser = new ChromeDriver();
            int i = 1;
            Browser.Navigate().GoToUrl("http://yandex.ru");
            List<IWebElement> News = Browser.FindElements(By.CssSelector("#tabnews_newsc a")).Where(x => x.Text != "").ToList();
            foreach(var item in News)
            {
                textBox1.AppendText(i + ") " + item.Text + "\n");
                i++;
            }
        }

        private void ExecuteJs(object sender, EventArgs e)
        {
            IJavaScriptExecutor jse = Browser as IJavaScriptExecutor;
            jse.ExecuteScript("alert('ti che?')");
        }

        private void SwitchTab(object sender, EventArgs e)
        {
            string tab = FindWindow("habr");
            Browser.SwitchTo().Window(tab);
            System.Windows.Forms.MessageBox.Show(Browser.Title + "\r\n" + Browser.Url);
        }

        private string FindWindow(string value)
        {
            string currentWindow = Browser.CurrentWindowHandle;
            string result = "";

            for (int i = 0; i < Browser.WindowHandles.Count; i++)
            {
                if (Browser.WindowHandles[i] != currentWindow)
                {
                    Browser.SwitchTo().Window(Browser.WindowHandles[i]);
                    if (Browser.Url.Contains(value))
                    {
                        result = Browser.WindowHandles[i];
                        break;
                    }
                }
            }
            Browser.SwitchTo().Window(currentWindow);
            return result;
        }
    }
}
