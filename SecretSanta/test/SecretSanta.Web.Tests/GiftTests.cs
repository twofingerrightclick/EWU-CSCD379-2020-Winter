using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests

    {
  
        [TestClass]
        public class MySeleniumTests
        {
            TestContext testContextInstance;
            private IWebDriver _Driver;
            static private Uri _ApiUri = new Uri("https://localhost:44388/");
            Uri _WebAppUri = new Uri("https://localhost:44394/");
            UserClient _UserClient;
            static private User _TestUser;
            private static Process? ApiHostProcess { get; set; }
            private static Process? WebHostProcess { get; set; }


            [ClassInitialize]
            public static async Task ClassInitalize(TestContext testContext)
            {
                if (testContext is null)
                    throw new ArgumentNullException(nameof(testContext));

                

                ApiHostProcess = Process.Start("dotnet", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj");
                WebHostProcess = Process.Start("dotnet", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj");
                ApiHostProcess.WaitForExit(16000);

                await CreateUserAsync(_ApiUri);


            }



            [TestInitialize()]
            public async Task SetupTestAsync()
            {

                string browser = "Chrome";
                switch (browser)
                {
                    case "Chrome":
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("headless");
                        _Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),chromeOptions);
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


            [TestMethod]
            [TestCategory("Chrome")]

            public async Task CreateGift_SuccessAsync()
            {


                //arrange
                Uri giftUri = new Uri(_WebAppUri + "Gifts");

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

                //Assert

                string giftId=_Driver.FindElement(By.XPath($"//*[text()='{uniqueGiftTitleInput}']/parent::tr/child::td")).Text;

                //cleanup

               /* string path = $"{System.IO.Directory.GetCurrentDirectory()}CreateGiftTest.png";
                System.Diagnostics.Trace.WriteLine(path);
                ((ITakesScreenshot)_Driver).GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
                this.TestContext.AddResultFile(path);*/


                await UseGiftClientAsync("DeleteAsync", int.Parse(giftId));


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


            //more for practice than practical
            private async Task UseGiftClientAsync(String methodName, params Object?[]? parameters)
            {
                if (string.IsNullOrEmpty(methodName))
                {
                    throw new ArgumentException("no such method", nameof(methodName));
                }

                using HttpClient client = new HttpClient();
                client.BaseAddress = _ApiUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                GiftClient giftClient = new GiftClient(client);

                System.Reflection.MethodInfo theMethod = typeof(GiftClient)
        .GetMethods()
        .Where(x => x.Name == methodName)
        .FirstOrDefault(x => x.GetParameters().Length == parameters.Length);

                Task result = (Task)theMethod.Invoke(giftClient, parameters);
                await result;


                client.Dispose();

            }

            static async Task CreateUserAsync(Uri apiUri)
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


            static async Task DeleteUserAsync(Uri apiUri)
            {
                using HttpClient client = new HttpClient();
                client.BaseAddress = apiUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                UserClient userClient = new UserClient(client);

                await userClient.DeleteAsync(_TestUser.Id);
               

                client.Dispose();
            }

            [TestCleanup()]
            public void MyTestCleanup()
            {
                _Driver.Quit();
            }


            [ClassCleanup()]
            public static async Task ClassCleanupAsync()
            {
                await DeleteUserAsync(_ApiUri);

                if (ApiHostProcess != null)
                {
                    ApiHostProcess.Kill();
                    //ApiHostProcess.CloseMainWindow();
                    ApiHostProcess.Close();
                }
                if (WebHostProcess != null)
                {
                    WebHostProcess.Kill();
                    //WebHostProcess.CloseMainWindow();
                    WebHostProcess.Close();
                }

            }
        }
    }

}
