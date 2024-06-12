using System.Threading.Tasks;
using Microsoft.Playwright; // Playwrightの名前空間をインポート

namespace HotelBookingTests.Pages
{
    public class TopPage
    {
        private readonly IPage _page;
        private readonly string _url = "https://hotel.testplanisphere.dev/ja/"; 

        public TopPage(IPage page)
        {
            _page = page;
        }

        public async Task OpenAsync()
        {
            await _page.GotoAsync(_url);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); // ページの完全な読み込みを待つ
        }

        public async Task ClearCookiesAndStorageAsync()
        {
            await _page.Context.ClearCookiesAsync();
            await _page.AddInitScriptAsync(@"() => {
            localStorage.clear();
            sessionStorage.clear(); }");
            Console.WriteLine("Local storage and session storage cleared.");
        }

    }
}
