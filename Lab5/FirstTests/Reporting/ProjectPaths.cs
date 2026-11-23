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
}
