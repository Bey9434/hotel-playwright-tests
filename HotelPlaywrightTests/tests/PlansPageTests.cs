using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using HotelBookingTests.Pages;
using System.Collections.Generic;

namespace HotelBookingTests.Tests
{
    [TestFixture]
    public class PlansPageTests
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
            Console.WriteLine("SetupAsync started.");

            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            _topPage = new TopPage(_page);
            _loginPage = new LoginPage(_page);
            _myPage = new MyPage(_page);
            _plansPage = new PlansPage(_page);

            _loginInfo = JsonHelper.LoadJson<LoginInfo>("login_info.json");
            _plansInfo = JsonHelper.LoadJson<PlansInfo>("plans_info.json");

            // デバッグ用出力
            Console.WriteLine("PlansInfo - NotLoggedIn: " + string.Join(", ", _plansInfo.NotLoggedIn));
            Console.WriteLine("PlansInfo - GeneralMember: " + string.Join(", ", _plansInfo.GeneralMember));
            Console.WriteLine("PlansInfo - PremiumMember: " + string.Join(", ", _plansInfo.PremiumMember));
            Console.WriteLine("SetupAsync completed.");
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

        private async Task TakeScreenshotAsync(string path)
        {
            try
            {
                await _page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
                Console.WriteLine($"スクリーンショットが正常に保存されました: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"スクリーンショットの保存に失敗しました: {path} - エラー: {ex.Message}");
            }
        }

        [Test]
        public async Task ShouldDisplayPlansWhenNotLoggedIn()
        {
            Console.WriteLine("ShouldDisplayPlansWhenNotLoggedIn started.");
            await _topPage.OpenAsync();
            await _topPage.GoToPlansPageAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (未ログイン): " + string.Join(", ", planTitles));

            Assert.That(planTitles, Is.EquivalentTo(_plansInfo.NotLoggedIn));
            Console.WriteLine("ShouldDisplayPlansWhenNotLoggedIn completed.");
        }

        [Test]
        public async Task ShouldDisplayPlansWhenLoggedInAsGeneralMember()
        {
            Console.WriteLine("ShouldDisplayPlansWhenLoggedInAsGeneralMember started.");
            await _topPage.OpenAsync();
            await _topPage.GoToLoginPageAsync();
            await _page.WaitForSelectorAsync("form#login-form", new PageWaitForSelectorOptions { Timeout = 10000 });
            await _loginPage.LoginAsync(_loginInfo.GeneralMember1.Email, _loginInfo.GeneralMember1.Password);
            await TakeScreenshotAsync("after_login_general_member.png");
            Console.WriteLine("Login completed for General Member.");
            Console.WriteLine("Current URL: " + _page.Url);
            await _page.WaitForURLAsync("**/mypage.html");
            await TakeScreenshotAsync("after_navigate_mypage_general_member.png");
            Console.WriteLine("Navigation to mypage completed.");
            await _myPage.GoToPlansPageAsync();
            await _page.WaitForSelectorAsync(".card-title", new PageWaitForSelectorOptions { Timeout = 10000 });
            await TakeScreenshotAsync("general_member_plans_page.png");
            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (一般会員): " + string.Join(", ", planTitles));
            Assert.That(planTitles, Is.EquivalentTo(_plansInfo.GeneralMember));
            Console.WriteLine("ShouldDisplayPlansWhenLoggedInAsGeneralMember completed.");
        }

        [Test]
        public async Task ShouldDisplayPlansWhenLoggedInAsPremiumMember()
        {
            Console.WriteLine("ShouldDisplayPlansWhenLoggedInAsPremiumMember started.");
            await _topPage.OpenAsync();
            await _topPage.GoToLoginPageAsync();

            var loginButton = await _page.QuerySelectorAsync("a.btn[href='./login.html']");
            await loginButton.ClickAsync();
            await TakeScreenshotAsync("before_login_premium_member.png");
            Console.WriteLine("Navigation to login page completed.");

            await _page.WaitForSelectorAsync("form#login-form", new PageWaitForSelectorOptions { Timeout = 10000 });
            await _loginPage.LoginAsync(_loginInfo.PremiumMember1.Email, _loginInfo.PremiumMember1.Password);
            await TakeScreenshotAsync("after_login_premium_member.png");
            Console.WriteLine("Login completed for Premium Member.");
            Console.WriteLine("Current URL: " + _page.Url);

            await _page.WaitForURLAsync("**/mypage.html");
            await TakeScreenshotAsync("after_navigate_mypage_premium_member.png");
            Console.WriteLine("Navigation to mypage completed.");

            await _myPage.GoToPlansPageAsync();

            await _page.WaitForSelectorAsync(".card-title", new PageWaitForSelectorOptions { Timeout = 10000 });
            await TakeScreenshotAsync("premium_member_plans_page.png");

            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (プレミアム会員): " + string.Join(", ", planTitles));

            Assert.That(planTitles, Is.EquivalentTo(_plansInfo.PremiumMember));
            Console.WriteLine("ShouldDisplayPlansWhenLoggedInAsPremiumMember completed.");
        }
    }
}
