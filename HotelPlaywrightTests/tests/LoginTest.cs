using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using HotelBookingTests.Pages;
using System.Collections.Generic;

namespace HotelBookingTests.Tests
{
    [TestFixture]
    public class LoginPageTests
    {
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        private TopPage _topPage;
        private LoginPage _loginPage;
        private PlansPage _plansPage;
        private LoginInfo _loginInfo;
        private PlansInfo _plansInfo;

        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            _topPage = new TopPage(_page);
            _loginPage = new LoginPage(_page);
            _plansPage = new PlansPage(_page);
            _loginInfo = JsonHelper.LoadJson<LoginInfo>("login_info.json");
            _plansInfo = JsonHelper.LoadJson<PlansInfo>("plans_info.json");

        }

        [OneTimeTearDown]
        public async Task TearDownAsync()
        {
            await _browser.CloseAsync();
        }

        [SetUp]
        public async Task SetUpTestAsync()
        {
            await _topPage.ClearCookiesAndStorageAsync();
        }


        [Test]
        public async Task LoggedInDefineUser()
        {
            JsonHelper.PrintPaths();
            await _topPage.OpenAsync();
            await _loginPage.GoToLoginPageAsync();
            await _loginPage.LoginAsync(_loginInfo.GeneralMember1.Email, _loginInfo.GeneralMember1.Password);
            Console.WriteLine("Logged in successfully");
        }

        [Test]
        public async Task ErrorMessageempty()
        {
            await _topPage.OpenAsync();
            await _loginPage.GoToLoginPageAsync();
            await _loginPage.LoginAsync("", "");
            var emailErrorMessage = await _loginPage.GetEmailMessageAsync();
            var passwordErrorMessage = await _loginPage.GetPasswordMessageAsync();
            Assert.That(emailErrorMessage, Is.EqualTo("このフィールドを入力してください。"));
            Assert.That(passwordErrorMessage, Is.EqualTo("このフィールドを入力してください。"));
            Console.WriteLine("Error messages displayed as expected");
        }
    }
}
