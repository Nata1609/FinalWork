using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autotestai.Tools
{
    public class MyScreenshot
    {

        public static void MakeScreenshot(IWebDriver driver)
        {
            Screenshot myBrowserScreenshot = driver.TakeScreenshot();
            string screenshotDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            string screenshotFolder = Path.Combine(screenshotDirectory, "screenshot");
            Directory.CreateDirectory(screenshotFolder);
            string screenshotName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:HH-mm}.png";
            string screenshotPath = Path.Combine(screenshotFolder, screenshotName);
            myBrowserScreenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        }


    }
}
