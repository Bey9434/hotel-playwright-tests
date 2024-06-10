using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HotelBookingTests.Pages
{
    public class PlansPage
    {
        private readonly IPage _page;
        private readonly string _planTitlesSelector = ".card-title";

        public PlansPage(IPage page)
        {
            _page = page;
        }

        public async Task<string[]> GetPlanTitlesAsync()
        {
            await _page.WaitForSelectorAsync(_planTitlesSelector);
            return await _page.EvaluateAsync<string[]>("Array.from(document.querySelectorAll('.card-title')).map(el => el.textContent)");
        }
    }
}
