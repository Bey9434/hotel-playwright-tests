using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Playwright;

namespace PlaywrightSample.Tests
{
    [TestFixture]
    public class SampleTests
    {
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
        }

        [Test]
        public async Task TestGoogleTitle()
        {
            await _page.GotoAsync("https://www.google.com");
            var title = await _page.TitleAsync();
            Assert.AreEqual("Google", title);
        }
    }
}

