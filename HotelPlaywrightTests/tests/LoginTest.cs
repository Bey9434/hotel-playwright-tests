using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using HotelBookingTests.Pages;

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
            await _topPage.OpenAsync();
            await _topPage.GoToLoginPageAsync();
            await _page.WaitForSelectorAsync("form#login-form", new PageWaitForSelectorOptions { Timeout = 60000 });
            await _loginPage.LoginAsync("sakura@example.com", "pass1234");
            await _page.WaitForURLAsync("**/mypage.html");
        }

        [Test]
        public async Task NotLoginUser()
        {
            await _topPage.OpenAsync();
            await _topPage.GoToLoginPageAsync();
            await _loginPage.LoginAsync("","");
            // スクリーンショットを撮る
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "loginpage_notwright_before_wait.png" });
            // メッセージが表示されるまで待機
            await _page.WaitForSelectorAsync("#email-message", new PageWaitForSelectorOptions { Timeout = 30000 });
            await _page.WaitForSelectorAsync("##password-message", new PageWaitForSelectorOptions { Timeout = 30000 });

            var emailMessage = await _loginPage.GetEmailMessageAsync();
            var passwordMessage = await _loginPage.GetPasswordMessageAsync();
             // スクリーンショットを撮る
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "loginpage_notwright_after_wait.png" });

            Assert.That(emailMessage, Is.EqualTo("このフィールドを入力してください。"));
            Assert.That(passwordMessage, Is.EqualTo("このフィールドを入力してください。"));

        }
    }
}
