using System.Text.Json;
using FirstTests.Core;
using FirstTests.Data;
using FirstTests.Pages;

namespace FirstTests.Tests;

public class DataDrivenLoginTests : BaseTest
{
    // BẮT BUỘC static: NUnit gọi hàm này lúc DÒ test, trước khi tạo đối tượng của class.
    // Dữ liệu nằm trong Data/users.json: thêm một bộ vào JSON là có thêm một ca test.
    private static IEnumerable<TestCaseData> TuJson()
    {
        // AppContext.BaseDirectory trỏ đúng bin/Debug/..., nơi csproj đã chép users.json ra
        string duongDan = Path.Combine(AppContext.BaseDirectory, "Data", "users.json");

        var tuyChon = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<UserData> boDuLieu =
            JsonSerializer.Deserialize<List<UserData>>(File.ReadAllText(duongDan), tuyChon)
            ?? throw new InvalidOperationException("users.json rỗng hoặc sai định dạng");

        int stt = 0;
        foreach (UserData u in boDuLieu)
        {
            stt++;

            // Chỉ số stt đảm bảo tên DUY NHẤT: NUnit không cho 2 ca test trùng tên
            yield return new TestCaseData(u.Username, u.Password, u.ThanhCong, u.GhiChu)
                .SetName($"DangNhap_{stt:00}_{TaoNhan(u.Username)}");
        }
    }

    // Nhãn dễ đọc trong Test Explorer, tránh đưa ký tự lạ hoặc chuỗi quá dài vào tên test
    private static string TaoNhan(string username)
    {
        if (username.Length == 0) return "trong";
        if (string.IsNullOrWhiteSpace(username)) return "chi_khoang_trang";
        if (username.Length > 30) return "chuoi_dai";
        if (!username.All(c => char.IsLetterOrDigit(c) || c == '_' || c == ' '))
            return "ky_tu_dac_biet";

        return username.Replace(' ', '_');
    }

    // KHÔNG thêm [Test] ở đây: [TestCaseSource] đã tự đánh dấu đây là test
    [TestCaseSource(nameof(TuJson))]
    public void DangNhap_TuFileJson(string tk, string mk, bool mongDoiThanhCong, string ghiChu)
    {
        InventoryPage trangSanPham = new LoginPage(driver).DangNhap(tk, mk);

        // Kỳ vọng cũng là DỮ LIỆU: một hàm phục vụ cả luồng thuận lẫn nghịch
        Assert.That(trangSanPham.DaHienThi(), Is.EqualTo(mongDoiThanhCong), ghiChu);
    }
}
