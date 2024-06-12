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
        private MyPage _myPage;
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
            _myPage = new MyPage(_page);
            _plansPage = new PlansPage(_page);

            try
            {
                _loginInfo = JsonHelper.LoadJson<LoginInfo>("login_info.json");
                _plansInfo = JsonHelper.LoadJson<PlansInfo>("plans_info.json");

                // デバッグ用出力
                if (_loginInfo != null)
                {
                    Console.WriteLine("LoginInfo デシリアライズに成功しました:");
                    Console.WriteLine("GeneralMember1 Email: " + _loginInfo.GeneralMember1.Email);
                    Console.WriteLine("GeneralMember1 Password: " + _loginInfo.GeneralMember1.Password);
                }
                else
                {
                    Console.WriteLine("LoginInfo のデシリアライズに失敗しました。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SetupAsync: " + ex.Message);
                throw;
            }
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
            try
            {
                await _topPage.OpenAsync();
                await _topPage.GoToLoginPageAsync();
                await _page.WaitForSelectorAsync("form#login-form", new PageWaitForSelectorOptions { Timeout = 30000 });
                await _loginPage.LoginAsync(_loginInfo.GeneralMember1.Email, _loginInfo.GeneralMember1.Password);

                // ページ遷移を待つ
                await _page.WaitForURLAsync("**/mypage.html", new PageWaitForURLOptions { Timeout = 30000 });
                Console.WriteLine("Logged in successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                throw;
            }
        }
    }
}
