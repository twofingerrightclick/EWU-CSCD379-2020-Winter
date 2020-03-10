using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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

            public void CreateGift_Success()
            {
                Uri giftUri = new Uri(_WebAppURL + "Gifts");

                _Driver.Navigate().GoToUrl(giftUri);

                Click("#createButton.button.is-secondary");

                String giftTitle = "The Princess Bride";
              
                string uniqueGiftTitleInput = giftTitle + Guid.NewGuid();

                InputText(GiftInputField.titleInput.ToString(), uniqueGiftTitleInput);

               
                InputText(GiftInputField.descriptionInput.ToString(), "A good book");

                InputText(GiftInputField.urlInput.ToString(), "https://en.wikipedia.org/wiki/The_Princess_Bride_(novel)");
                
                SelectOptionValueFromDropDown("select", _TestUser.Id.ToString(new System.Globalization.CultureInfo("en-us")));

                Click("#submit.button");

                Thread.Sleep(1500);

                _Driver.FindElement(By.XPath($"//*[text()='{uniqueGiftTitleInput}']"));

               




            }

            private void SelectOptionValueFromDropDown(string listId, string optionValue)
            {
                var s = new SelectElement(_Driver.FindElement(By.CssSelector(listId)));
                s.SelectByValue(optionValue);
            }

            private void Click(string v)
            {
                _Driver.FindElement(By.CssSelector(v)).Click();
            }
            private enum GiftInputField
            {
                titleInput,
                descriptionInput,
                urlInput

            }

            private void InputText(string inputFieldId, string input)
            {
                _Driver.FindElement(By.CssSelector("input#" + inputFieldId + ".input")).SendKeys(input);
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
                _WebAppURL = new Uri("https://localhost:44394/");

                _ApiUri = new Uri("https://localhost:44388/");

                await CreateUserAsync(_ApiUri);



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


                User responseUser = await userClient.PostAsync(userInput);
                if (responseUser is default(User))
                {
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
