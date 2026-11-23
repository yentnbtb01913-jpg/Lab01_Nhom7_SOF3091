using System;
using System.IO;

namespace FirstTests.Reporting;

internal static class ProjectPaths
{
    // Dung AppContext.BaseDirectory, khong phu thuoc thu muc lam viec cua Visual Studio.
    internal static string LabRoot()
    {
        DirectoryInfo? current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current != null)
        {
            if (Directory.Exists(Path.Combine(current.FullName, "FirstTests")) &&
                File.Exists(Path.Combine(current.FullName, "DeBai.pdf")))
                return current.FullName;
            current = current.Parent;
        }
        throw new DirectoryNotFoundException(
            "Khong tim thay thu muc Lab co FirstTests/ va DeBai.pdf. " +
            "Hay giai nen ca thu muc Lab roi mo file Lab.slnx.");
    }

    // Ban ZIP cu dong goi sai: screenshots va TestReport la FILE 0 byte thay vi thu muc.
    // Chi xoa file trung ten NEU do la file trong 0 byte. Tuyet doi khong xoa du lieu that.
    internal static string EnsureOutputDirectory(string folderName)
    {
        if (folderName != "screenshots" && folderName != "TestReport")
            throw new ArgumentException("Thu muc ket qua khong hop le.", nameof(folderName));

        string path = Path.Combine(LabRoot(), folderName);
        if (File.Exists(path))
        {
            var file = new FileInfo(path);
            if (file.Length == 0)
                File.Delete(path);
            else
                throw new IOException(
                    $"Khong the tao thu muc '{path}' vi dang co mot FILE cung ten. " +
                    "Hay doi ten file do de bao toan du lieu, sau do chay lai.");
        }
        Directory.CreateDirectory(path);
        return path;
    }
}
