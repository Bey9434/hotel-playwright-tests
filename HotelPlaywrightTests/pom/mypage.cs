using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HotelBookingTests.Pages
{
    public class MyPage
    {
        private readonly IPage _page;
        private readonly string _plansLink = "a.nav-link[href='./plans.html']";

        public MyPage(IPage page)
        {
            _page = page;
        }

        public async Task GoToPlansPageAsync()
        {
            await _page.ClickAsync(_plansLink);
            await _page.WaitForSelectorAsync(".card-title", new PageWaitForSelectorOptions { Timeout = 10000 });
        }
    }
}
