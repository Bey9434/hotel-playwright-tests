using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HotelBookingTests.Pages
{
    public class PlansPage
    {
        private readonly IPage _page;
        private readonly string _planTitlesSelector = ".card-title";

        private readonly string _plansLink = "a.nav-link[href='./plans.html']";

        public PlansPage(IPage page)
        {
            _page = page;
        }

        public async Task<string[]> GetPlanTitlesAsync()
        {
            await _page.WaitForSelectorAsync(_planTitlesSelector);
            return await _page.EvaluateAsync<string[]>("Array.from(document.querySelectorAll('.card-title')).map(el => el.textContent)");
        }
         public async Task GoToPlansPageAsync()
        {
            await _page.ClickAsync(_plansLink);
            await _page.WaitForSelectorAsync(".card-title", new PageWaitForSelectorOptions { Timeout = 10000 });
        }
    }
}
