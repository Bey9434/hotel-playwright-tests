using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HotelBookingTests.Pages
{
    public class PlansPage
    {
        private readonly IPage _page;
        private readonly string _plansLink = "a.nav-link[href='./plans.html']";

        public PlansPage(IPage page)
        {
            _page = page;
        }

        public async Task<string[]> GetPlanTitlesAsync()
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var elements = await _page.QuerySelectorAllAsync(".card-title");
            var planTitles = new List<string>();
            foreach (var element in elements)
            {
               var text = await element.TextContentAsync();
               if (text != null)
                {
                    planTitles.Add(text);
                }
            }
            return planTitles.ToArray();
        }
         public async Task GoToPlansPageAsync()
        {
            await _page.ClickAsync(_plansLink);
        }
    }
}
