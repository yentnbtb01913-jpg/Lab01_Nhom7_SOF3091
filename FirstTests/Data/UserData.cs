namespace FirstTests.Data;

// Ánh xạ với users.json. Tên thuộc tính PHẢI khớp khóa JSON.
public class UserData
{
    public string Username  { get; set; } = "";
    public string Password  { get; set; } = "";
    public bool   ThanhCong { get; set; }
    public string GhiChu    { get; set; } = "";
}
