using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HotelBookingTests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly string _emailInput = "#email";
        private readonly string _passwordInput = "#password";
        private readonly string _submitButton = "#login-button";

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task LoginAsync(string email, string password)
        {
            await _page.FillAsync(_emailInput, email);
            await _page.FillAsync(_passwordInput, password);
            await _page.ClickAsync(_submitButton);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); 
        }
    }
}

