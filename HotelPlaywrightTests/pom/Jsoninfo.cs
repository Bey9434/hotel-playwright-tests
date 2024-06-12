using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace HotelBookingTests.Pages
{
    public static class JsonHelper
    {
        private static readonly string BasePath = @"C:\Users\Tuyug\Desktop\hotel-playwright-tests\HotelPlaywrightTests\Info\";

        public static T LoadJson<T>(string fileName)
        {
            var filePath = Path.Combine(BasePath, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"ファイルが見つかりません: {filePath}");
            }

            var jsonString = File.ReadAllText(filePath);
            Console.WriteLine($"JSONファイルの内容: {jsonString}");

            try
            {
                var result = JsonSerializer.Deserialize<T>(jsonString);
                if (result == null)
                {
                    throw new InvalidOperationException("デシリアライズに失敗しました。結果が null です。");
                }
                Console.WriteLine($"JSON デシリアライズ成功: {fileName}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON デシリアライズに失敗しました: {ex.Message}");
                throw;
            }
        }
    }

    public class LoginInfo
    {
        public UserCredentials GeneralMember1 { get; set; } = new UserCredentials();
        public UserCredentials GeneralMember2 { get; set; } = new UserCredentials();
        public UserCredentials PremiumMember1 { get; set; } = new UserCredentials();
        public UserCredentials PremiumMember2 { get; set; } = new UserCredentials();
    }

    public class UserCredentials
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class PlansInfo
    {
        public List<string> NotLoggedIn { get; set; } = new List<string>();
        public List<string> GeneralMember { get; set; } = new List<string>();
        public List<string> PremiumMember { get; set; } = new List<string>();
    }
}
