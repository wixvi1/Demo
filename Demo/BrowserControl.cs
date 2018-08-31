using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        //Opens browser
        private void OpenBrowser(object sender, EventArgs e)
        {
            if (Browser == null)
            {
                Browser = new ChromeDriver();// opens browser
                Browser.Manage().Window.Maximize();
                Browser.Navigate().GoToUrl("http://google.com");
            }
        }

        //Closes browser
        private void CloseBrowser(object sender, EventArgs e)
        {
            Browser.Quit();//Closes browser
        }

        //Example of how to execute javascript on client
        private void ExecuteJs(object sender, EventArgs e)
        {
            if (Browser == null)
                Browser = new ChromeDriver();
            Browser.Manage().Window.Maximize();
            IJavaScriptExecutor jse = Browser as IJavaScriptExecutor;
            jse.ExecuteScript("alert('test')");
        }

        //Gets all actual news from main yandex page
        private void GetNews(object sender, EventArgs e)
        {
            int i = 1;//counter of news
            TextBox.Clear();
            if (Browser == null)
                Browser = new ChromeDriver();
            Browser.Manage().Window.Maximize();           
            Browser.Navigate().GoToUrl("http://yandex.ru");
            List<IWebElement> News = Browser.FindElements(By.CssSelector("#tabnews_newsc a")).Where(x => x.Text != "").ToList();// finds all the news with given css selector
            foreach (var item in News)
            {
                TextBox.AppendText(i + ") " + item.Text + "\n");
                i++;
            }
        }

        // Logs in to yandex mail
        private void Login(object sender, EventArgs e)
        {
            if(Browser == null)
                Browser = new ChromeDriver();
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("http://yandex.ru");
            Browser.FindElement(By.ClassName("desk-notif-card__login-enter-expanded")).Click();
            Browser.FindElement(By.Name("login")).SendKeys("Zodiacasti");//provide login
            Browser.FindElement(By.Name("passwd")).SendKeys("princpersii");//prodivde password
            Browser.FindElement(By.ClassName("passport-Button")).Click();
        }

        //Simple example of threads on client side
        private void Thread(object sender, EventArgs e)
        {
            if (Browser == null)
                Browser = new ChromeDriver();
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("https://www.degraeve.com/reference/simple-ajax-example.php");
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Browser.FindElement(By.CssSelector("input[value='Go']")).Click();
            TextBox.Text = Browser.FindElement(By.CssSelector("#result p")).Text;
        }

        //Browser that works on the background
        private void InvisibleBrowser(object sender, EventArgs e)
        {
            if (Browser != null)
                Browser.Quit();
            Browser = new PhantomJSDriver();
            Browser.Navigate().GoToUrl("http://google.com");
            (Browser as PhantomJSDriver).GetScreenshot().SaveAsFile("d:\\page1.jpg", ScreenshotImageFormat.Jpeg);
            Browser.FindElement(By.Name("q")).SendKeys("Пирамиды" + OpenQA.Selenium.Keys.Return);
            (Browser as PhantomJSDriver).GetScreenshot().SaveAsFile("d:\\page2.jpg", ScreenshotImageFormat.Jpeg);
            MessageBox.Show("Screenshots are saved on disk D", "Done");
        }
    }
}
