using System.Threading.Tasks;
using Microsoft.Playwright; // Playwrightの名前空間をインポート

namespace HotelBookingTests.Pages
{
    public class TopPage
    {
        private readonly IPage _page;
        private readonly string _url = "https://hotel.testplanisphere.dev/ja/";
        private readonly string _plansLink = "a.nav-link[href='./plans.html']"; 
        private readonly string _loginButton = "a.btn[href=\"./login.html\"]"; 

        public TopPage(IPage page)
        {
            _page = page;
        }

        public async Task OpenAsync()
        {
            await _page.GotoAsync(_url);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); // ページの完全な読み込みを待つ
        }

        public async Task GoToPlansPageAsync()
        {
            await _page.ClickAsync(_plansLink);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); 
        }

        public async Task GoToLoginPageAsync()
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "before_click_login_button.png" });
            await _page.ClickAsync(_loginButton);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "after_click_login_button.png" });
        }

        public async Task ClearCookiesAndStorageAsync()
        {
        await _page.Context.ClearCookiesAsync();
        await _page.AddInitScriptAsync(@"() => {
        localStorage.clear();
        sessionStorage.clear();
                            }");
        Console.WriteLine("Local storage and session storage cleared.");
        }

    }
}
