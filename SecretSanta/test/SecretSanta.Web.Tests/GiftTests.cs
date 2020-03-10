using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.Api;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlogEngine.Web
{
    [TestClass]
    public class GiftTests

    {
        /// <summary>
        /// Summary description for MySeleniumTests
        /// </summary>
        [TestClass]
        public class MySeleniumTests
        {
            TestContext testContextInstance;
            private IWebDriver _Driver;
            private Uri _WebAppURL;
            Uri _ApiUri;

            private User _TestUser;

            public MySeleniumTests()
            {
            }

            [TestMethod]
            [TestCategory("Chrome")]
            public void TheBingSearchTest()
            {
              /*  _Driver.Navigate().GoToUrl(appURL + "/");
                _Driver.FindElement(By.Id("sb_for_mq")).SendKeys("Azure Pipelines");
                _Driver.FindElement(By.Id("sb_form_go")).Click();
                _Driver.FindElement(By.XPath("//ol[@id='b_results']/li/h2/a/strong[3]")).Click();
                Assert.IsTrue(_Driver.Title.Contains("Azure Pipelines"), "Verified title of the page");*/
            }


            [TestMethod]

            public void OpenUrl()
            {
                _Driver.Navigate().GoToUrl(_WebAppURL);
            }

            /// <summary>
            ///Gets or sets the test context which provides
            ///information about and functionality for the current test run.
            ///</summary>
            public TestContext TestContext
            {
                get
                {
                    return testContextInstance;
                }
                set
                {
                    testContextInstance = value;
                }
            }


            [TestInitialize()]
            public async Task SetupTestAsync()
            {
                _WebAppURL = new Uri("http://www.bing.com/");

                Uri apiUri = new Uri("https://localhost:44388/");

                await CreateUserAsync(apiUri);

               

                string browser = "Chrome";
                switch (browser)
                {
                    case "Chrome":
                        _Driver = new ChromeDriver();
                        break;
                        /*  case "Firefox":
                              driver = new FirefoxDriver();
                              break;
                          case "IE":
                              driver = new InternetExplorerDriver();
                              break;
                          default:
                              driver = new ChromeDriver();
                              break;*/
                }


               

            }








            async Task CreateUserAsync(Uri apiUri)
            {
                using HttpClient client = new HttpClient();
                client.BaseAddress = apiUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                UserInput userInput = new UserInput() { FirstName = "Inigo", LastName = "Montoya" };

                UserClient userClient = new UserClient(client);

       
                User responseUser =await userClient.PostAsync(userInput);
                if (responseUser is default(User)) {
                    throw new NullReferenceException(nameof(responseUser));
                }
                _TestUser = responseUser;
                
                client.Dispose();
            }

            [TestCleanup()]
            public void MyTestCleanup()
            {
                _Driver.Quit();
            }
        }
    }

}
