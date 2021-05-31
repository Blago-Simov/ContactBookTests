using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ContactBook_Web_App
{
    public class ContactBookWebTests
    {
        
        IWebDriver driver;
        const string baseUrl = "https://contactbook-3.blagosimov.repl.co/";
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
           

        }
        
        [Test]
        public void List_Contacts()
        {
            
            string contactsUrl = baseUrl + "/contacts";
            driver.Navigate().GoToUrl(contactsUrl);

            var textBoxFirstName = driver.FindElement
                (By.XPath("/html/body/main/div/a[1]/table/tbody/tr[1]/td"));
            Assert.AreEqual("Steve", textBoxFirstName.Text);
            var textboxLastName = driver.FindElement
                (By.XPath("/ html / body / main / div / a[1] / table / tbody / tr[2] / td"));
            Assert.AreEqual("Jobs", textboxLastName.Text);
        }
       

        [Test]
        public void Find_Contact_By_Keyword()
        {
            string keyword = "albert";
            string searchUrl = baseUrl + "/contacts/search";
            driver.Navigate().GoToUrl(searchUrl);

            var searchpanel = driver.FindElement(By.Id("keyword"));
            searchpanel.SendKeys(keyword);
            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var divresultField = driver.FindElement(By.CssSelector("#searchResult"));
            Assert.AreEqual("1 contacts found.", divresultField.Text);
            var textBoxFName = driver.FindElement
                (By.XPath("/html/body/main/div/a[1]/table/tbody/tr[1]/td"));
            Assert.AreEqual("Albert", textBoxFName.Text);
            var textboxLName = driver.FindElement
                (By.XPath("/ html / body / main / div / a[1] / table / tbody / tr[2] / td"));
            Assert.AreEqual("Einstein", textboxLName.Text);

        }
        [Test]
        public void Find_Contact_By_Keyword_Invalid_data()
        {
            string keyword = "invalid2635";
            string searchUrl = baseUrl + "/contacts/search";
            driver.Navigate().GoToUrl(searchUrl);

            var searchPanel = driver.FindElement(By.Id("keyword"));
            searchPanel.SendKeys(keyword);
            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var divresultField = driver.FindElement(By.CssSelector("#searchResult"));
            Assert.AreEqual("No contacts found.", divresultField.Text);
            
        }
        [Test]
        public void Create_Contact_By_Invalid_data()
        {
            string firstName = "Maria";
            string lastName = "Green";

            string createContactchUrl = baseUrl + "/contacts/create";
            driver.Navigate().GoToUrl(createContactchUrl);

            var textBoxFirstName = driver.FindElement(By.Id("firstName"));
            textBoxFirstName.SendKeys(firstName);
            var textBoxLastName = driver.FindElement(By.Id("lastName"));
            textBoxLastName.SendKeys(lastName);
            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();


            var errDiv = driver.FindElement(By.CssSelector("body > main > div"));
            Assert.AreEqual("Error: Invalid email!", errDiv.Text);


        }
        [Test]
        public void Create_Contact_By_Valid_data()
        {
            string firstName = "Maria";
            string lastName = "Green";
            string email = "mgreen@yahoo.com";
            string phone = "+359 888 888";
            string comments = "my friend";


            string createContactchUrl = baseUrl + "/contacts/create";
            driver.Navigate().GoToUrl(createContactchUrl);

            var textBoxFirstName = driver.FindElement(By.Id("firstName"));
            textBoxFirstName.SendKeys(firstName);
            var textBoxLastName = driver.FindElement(By.Id("lastName"));
            textBoxLastName.SendKeys(lastName);
            var textBoxemail = driver.FindElement(By.Id("email"));
            textBoxemail.SendKeys(email);
            var textBoxPhone = driver.FindElement(By.Id("phone"));
            textBoxPhone.SendKeys(phone);
            var textBoxComments = driver.FindElement(By.Id("comments"));
            textBoxComments.SendKeys(comments);
            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            var contactTables = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastContactTalble = contactTables[contactTables.Count - 1];
            var textFieldFirstname = lastContactTalble.FindElement(By.CssSelector("tr.fname td"));
            Assert.AreEqual(firstName, textFieldFirstname.Text);
            var textFieldLastname = lastContactTalble.FindElement(By.CssSelector("tr.lname td"));
            Assert.AreEqual(lastName, textFieldLastname.Text);
            var textFieldEmail = lastContactTalble.FindElement(By.CssSelector("tr.email td"));
            Assert.AreEqual(email, textFieldEmail.Text);
            var textFieldPhone = lastContactTalble.FindElement(By.CssSelector("tr.phone td"));
            Assert.AreEqual(phone, textFieldPhone.Text);
            var textFieldComments = lastContactTalble.FindElement(By.CssSelector("tr.comments  td"));
            Assert.AreEqual(comments,textFieldComments.Text);



        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

    }
}
