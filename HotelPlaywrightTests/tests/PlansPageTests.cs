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

        /*スクリーンショットをとってデバッグしたいときに使用する
        private async Task TakeScreenshotAsync(string fileName)
        {   
            string directoryPath = @"C:\Users\Tuyug\Desktop\hotel-playwright-tests\HotelPlaywrightTests\src"; //ここに絶対パスでスクリーンショットを保存したいフォルダを指定する。
            string fullPath = Path.Combine(directoryPath, fileName);
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = fullPath });
        }*/
        

        [Test]
        public async Task PlansNotLoggedIn()
        {
            await _topPage.OpenAsync();
            await _plansPage.GoToPlansPageAsync();
            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (未ログイン): " + string.Join(", ", planTitles));
            Console.WriteLine("実際のプランタイトル (未ログイン): " + string.Join(", ", _plansInfo.NotLoggedIn));

            Assert.That(planTitles, Is.EquivalentTo(_plansInfo.NotLoggedIn));
        }

        [Test]
        public async Task PlansLoggedInGeneralMember()
        {

            await _topPage.OpenAsync();
            await _loginPage.GoToLoginPageAsync();
            await _loginPage.LoginAsync(_loginInfo.GeneralMember1.Email, _loginInfo.GeneralMember1.Password);
            await _plansPage.GoToPlansPageAsync();
            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (一般会員): " + string.Join(", ", planTitles));
            Console.WriteLine("実際のプランタイトル (一般会員): " + string.Join(", ", _plansInfo.GeneralMember));

            Assert.That(planTitles, Is.EquivalentTo(_plansInfo.GeneralMember));
        }

        [Test]
        public async Task PlansLoggedInPremiumMember()
        {
            await _topPage.OpenAsync();
            await _loginPage.GoToLoginPageAsync();
            await _loginPage.LoginAsync(_loginInfo.PremiumMember1.Email, _loginInfo.PremiumMember1.Password);
            await _plansPage.GoToPlansPageAsync();
            var planTitles = await _plansPage.GetPlanTitlesAsync();

            // デバッグ情報を出力
            Console.WriteLine("取得されたプランタイトル (プレミアム会員): " + string.Join(", ", planTitles));
            Console.WriteLine("実際のプランタイトル (プレミアム会員): " + string.Join(", ", _plansInfo.PremiumMember));

            Assert.That(planTitles, Is.EquivalentTo(_plansInfo.PremiumMember));
        }
    }
}
