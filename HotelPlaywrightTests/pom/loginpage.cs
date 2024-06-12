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

            // フィールドに値が設定されたことを確認するために値を取得
            var emailValue = await _page.GetAttributeAsync(_emailInput, "value");
            var passwordValue = await _page.GetAttributeAsync(_passwordInput, "value");
            Console.WriteLine($"Email field value after fill: {emailValue}");
            Console.WriteLine($"Password field value after fill: {passwordValue}");

            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "fill_in_the_Blank.png" });
            await _page.ClickAsync(_submitButton);
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "after_click_attempt.png" });
        }

        public async Task<string> GetEmailMessageAsync()
        {
            return await _page.InnerTextAsync("#email-message");
        }

        public async Task<string> GetPasswordMessageAsync()
        {
            return await _page.InnerTextAsync("#password-message");
        }
    }
}
