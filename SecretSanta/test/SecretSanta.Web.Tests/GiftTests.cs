using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SecretSanta.Web.Api;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Collections.Generic;


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
            static private Uri _ApiUri = new Uri("https://localhost:5000/");
            static Uri _WebAppUri = new Uri("https://localhost:5001/");
            UserClient _UserClient;
            static private User _TestUser;
            private static Process? ApiHostProcess { get; set; }
            private static Process? WebHostProcess { get; set; }


            /*    ApiHostProcess = Process.Start("dotnet", $"run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj --urls={_ApiUri.ToString()}");
                     WebHostProcess = Process.Start("dotnet", $"run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj --urls={_WebAppUri.ToString()}");
                     ApiHostProcess.WaitForExit(20000);
                     Thread.Sleep(1000);
    */

            [ClassInitialize]
            public static void ClassInitialize(TestContext testContext)
            {
                using WebClient webClient = new WebClient();
                ApiHostProcess = StartWebHost("SecretSanta.Api", 5000, "Swagger", new string[] { "ConnectionStrings:DefaultConnection='Data Source=SecretSanta.db'" });

                WebHostProcess = StartWebHost("SecretSanta.Web", 5001, "", "ApiUrl=https://localhost:5000");

                Process StartWebHost(string projectName, int port, string urlSubDirectory, params string[] args)
                {

                    string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, projectName + ".exe");
                    Process[] alreadyExecutingProcesses = Process.GetProcessesByName(projectName);
                    if (alreadyExecutingProcesses.Length != 0)
                    {
                        foreach (Process item in alreadyExecutingProcesses)
                        {
                            item.Kill();
                        }
                    }

                    string testAssemblyLocation = Assembly.GetExecutingAssembly().Location;
                    string testAssemblyName = Path.GetFileNameWithoutExtension(testAssemblyLocation);
                    string projectExe = testAssemblyLocation.RegexReplace(testAssemblyName, projectName)
                        .RegexReplace(@"\\test\\", @"\src\").RegexReplace("dll$", "exe");

                    string argumentList = $"{string.Join(" ", args)} Urls=https://localhost:{port}";

                    ProcessStartInfo startInfo = new ProcessStartInfo(projectExe, argumentList)
                    {
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        LoadUserProfile = true
                    };

                    string stdErr = "";
                    string stdOut = "";
                    // Justification: Dispose invoked by caller on Process object returned.
#pragma warning disable CA2000 // Dispose objects before losing scope
                    Process host = new Process
                    {
                        EnableRaisingEvents = true,
                        StartInfo = startInfo
                    };
#pragma warning restore CA2000 // Dispose objects before losing scope

                    host.ErrorDataReceived += (sender, args) =>
                        stdErr += $"{args.Data}\n";
                    host.OutputDataReceived += (sender, args) =>
                        stdOut += $"{args.Data}\n";
                    host.Start();
                    host.BeginErrorReadLine();
                    host.BeginOutputReadLine();

                    for (int seconds = 20; seconds > 0; seconds--)
                    {
                        if (stdOut.Contains("Application started."))
                        {
                            _ = webClient.DownloadString(
                                $"https://localhost:{port}/{urlSubDirectory.TrimStart(new char[] { '/', '\\' })}");
                            return host;
                        }
                        else if (host.WaitForExit(1000))
                        {
                            break;
                        }
                    }

                    if (!host.HasExited) host.Kill();
                    host.WaitForExit();
                    throw new InvalidOperationException($"Unable to execute process successfully: {stdErr}") { Data = { { "StandardOut", stdOut } } };

                }
            }

            [ClassCleanup]
            public static void ClassCleanup()
            {
                ApiHostProcess?.CloseMainWindow();
                ApiHostProcess?.Close();
                WebHostProcess?.CloseMainWindow();
                WebHostProcess?.Close();
            }



            [TestInitialize]
            public void TestInitialize()
            {

                string browser = "Chrome";
                var chromeOptions = new ChromeOptions();
                chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                switch (browser)
                {
                    case "Chrome":
                        _Driver = new ChromeDriver(chromeOptions);
                        break;
                    default:
                        _Driver = new ChromeDriver(chromeOptions);
                        break;
                }
                _Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
            }


            [TestMethod]
            [TestCategory("Chrome")]

            public async Task CreateGift_SuccessAsync()
            {
                await CreateUserAsync(_ApiUri);

                //arrange
                Uri giftUri = new Uri(_WebAppUri + "Gifts");

                _Driver.Navigate().GoToUrl(giftUri);

                Thread.Sleep(2000);
              
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


        }
    }

    public static class StringRegExExtension
    {
        static public string RegexReplace(this string input, string findPattern, string replacePattern)
        {
            return Regex.Replace(input, findPattern, replacePattern);
        }
    }

}
