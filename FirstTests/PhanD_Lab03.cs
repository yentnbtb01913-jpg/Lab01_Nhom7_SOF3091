using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces; // cho TestStatus
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests
{
    [TestFixture]
    public class PhanD_Lab03 : FirstTests.Core.BaseTest
    {

        // Chỉ chụp ảnh khi test Failed, tên ảnh = tên ca test, đính kèm bằng AddTestAttachment



        // Cố ý làm rớt để sinh ảnh minh chứng. Chuột phải test này -> Run.
        [Test, Explicit("Cố ý rớt để sinh ảnh minh chứng Phần D")]
        public void D1_CoYRot()
        {
            driver.Navigate().GoToUrl(FirstTests.Config.ConfigReader.Load().Site("TheInternet"));

            Assert.That(driver.Title, Is.EqualTo("TIEU DE SAI CO Y"));
        }
    }
}