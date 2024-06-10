using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using HotelBookingTests.Pages;

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
        public async Task ShouldDisplayPlansWhenNotLoggedIn()
        {
            await _topPage.OpenAsync();
            await _topPage.GoToPlansPageAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // スクリーンショットを撮る
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "not_logged_in_plans_page.png" });

            var planTitles = await _plansPage.GetPlanTitlesAsync();
            
            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (未ログイン): " + string.Join(", ", planTitles));

            var expectedTitles = new[] {
                "お得な特典付きプラン", "素泊まり", "出張ビジネスプラン", "エステ・マッサージプラン",
                "貸し切り露天風呂プラン", "カップル限定プラン", "テーマパーク優待プラン",
            };

            Assert.That(planTitles, Is.EquivalentTo(expectedTitles));
        }

        [Test]
        public async Task ShouldDisplayPlansWhenLoggedInAsGeneralMember()
        {
            await _topPage.OpenAsync();
            await _topPage.GoToLoginPageAsync();
            await _page.WaitForSelectorAsync("form#login-form", new PageWaitForSelectorOptions { Timeout = 60000 });
            await _loginPage.LoginAsync("sakura@example.com", "pass1234");
            await _page.WaitForURLAsync("**/mypage.html");
            await _myPage.GoToPlansPageAsync();
            await _page.WaitForSelectorAsync(".card-title", new PageWaitForSelectorOptions { Timeout = 60000 });

            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // スクリーンショットを撮る
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "general_member_plans_page.png" });

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (一般会員): " + string.Join(", ", planTitles));

            var expectedTitles = new[] {
                "お得な特典付きプラン", "ディナー付きプラン", "お得なプラン", "素泊まり", "出張ビジネスプラン",
                "エステ・マッサージプラン", "貸し切り露天風呂プラン", "カップル限定プラン", "テーマパーク優待プラン"
            };

            Assert.That(planTitles, Is.EquivalentTo(expectedTitles));
        }

        [Test]
        public async Task ShouldDisplayPlansWhenLoggedInAsPremiumMember()
        {
            await _topPage.OpenAsync();
            await _topPage.GoToLoginPageAsync();

            var loginButton = await _page.QuerySelectorAsync("a.btn[href='./login.html']");
            if (loginButton == null)
            {
                Console.WriteLine("ログインボタンが見つかりません。ページのスクリーンショットを撮ります。");
                await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "login_button_not_found.png" });
                Assert.Fail("ログインボタンが見つかりません。");
            }

            await loginButton.ClickAsync();

            await _page.WaitForSelectorAsync("form#login-form", new PageWaitForSelectorOptions { Timeout = 60000 });
            await _loginPage.LoginAsync("ichiro@example.com", "password");

            await _page.WaitForURLAsync("**/mypage.html");

            await _myPage.GoToPlansPageAsync();

            await _page.WaitForSelectorAsync(".card-title", new PageWaitForSelectorOptions { Timeout = 60000 });

            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // スクリーンショットを撮る
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "premium_member_plans_page.png" });

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (プレミアム会員): " + string.Join(", ", planTitles));

            var expectedTitles = new[] {
                "お得な特典付きプラン", "プレミアムプラン", "ディナー付きプラン", "お得なプラン", "素泊まり", 
                "出張ビジネスプラン", "エステ・マッサージプラン", "貸し切り露天風呂プラン", "カップル限定プラン", "テーマパーク優待プラン"
            };

            Assert.That(planTitles, Is.EquivalentTo(expectedTitles));
        }
    }
}
