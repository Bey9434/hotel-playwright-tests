using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using NUnit.Framework;

namespace HotelBookingTests.Pages
{
    public static class JsonHelper
    {
        private static readonly string ProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
        public static void PrintPaths()
        {
            Console.WriteLine($"AppDomain BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
            Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
        }



        private static readonly string BasePath = Path.Combine(ProjectDirectory, "Info");
        public static T LoadJson<T>(string fileName)
        {
            var filePath = Path.Combine(BasePath, fileName);
            var jsonString = File.ReadAllText(filePath);
            var result = JsonSerializer.Deserialize<T>(jsonString);
            if (result == null)
            {
                throw new InvalidOperationException($"ファイル '{filePath}' の JSON データをデシリアライズに失敗しました。");
            }
            return result;
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
