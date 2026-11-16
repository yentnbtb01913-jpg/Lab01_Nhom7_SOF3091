
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;
using FirstTests.Core;
using FirstTests.Data;
using FirstTests.Pages;

namespace FirstTests.Tests
{
    [TestFixture]
    public class DataDrivenLoginTests : BaseTests
    {
        // Nguồn dữ liệu phải static
        public static IEnumerable<TestCaseData> DangNhapCases()
        {
            string filePath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "users.json"
            );

            string json = File.ReadAllText(filePath);

            List<UserLoginData> users =
                JsonSerializer.Deserialize<List<UserLoginData>>(json)
                ?? new List<UserLoginData>();

            for (int i = 0; i < users.Count; i++)
            {
                UserLoginData user = users[i];

                yield return new TestCaseData(user)
                    .SetName($"DangNhap_{i:00}_{user.Username}");
            }
        }

        // Một hàm kiểm thử cho nhiều bộ dữ liệu
        [Test]
        [TestCaseSource(nameof(DangNhapCases))]
        public void KiemTraDangNhap(UserLoginData data)
        {
            LoginPage loginPage = new LoginPage(driver);

            InventoryPage inventoryPage =
                loginPage.DangNhap(data.Username, data.Password);

            bool actualSuccess =
                driver.Url.Contains("inventory.html");

            Assert.That(
                actualSuccess,
                Is.EqualTo(data.ThanhCong),
                data.GhiChu
            );

            if (data.ThanhCong)
            {
                Assert.That(
                    inventoryPage.DaHienThi(),
                    Is.True,
                    "Trang sản phẩm phải hiển thị."
                );
            }

            TestContext.WriteLine("Username: " + data.Username);
            TestContext.WriteLine("Ghi chú: " + data.GhiChu);
            TestContext.WriteLine(
                "Kết quả mong đợi: " + data.ThanhCong
            );
        }
    }
}

