using System;


using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using OpenQA.Selenium.Appium.MultiTouch;

namespace UnitTestProject
{
    public struct XPathPlatform
    {
        public string XPathAndroid4 { get; set; }
        public string XPathAndroid5 { get; set; }
        public string XPathAndroid6 { get; set; }
        public string XPathIOS7 { get; set; }
        public string XPathIOS9 { get; set; }

    }

    public class AppiumHelpers
    {
        public enum Platforms {

            Android6,
            Android5,
            Android4,
            IOS7,
            IOS9            
        }

        public enum Devices
        {
            Emulador,
            SmartPhone,
            Tablet
        }

        public static AppiumDriver<IWebElement> Driver { get; set; }
        public static string AppName { get; set; }

        public static string ServerAndroid1 { get { return "http://127.0.0.1:4723/wd/hub"; } }
        public static string ServerIOS1 { get { return "http://10.1.191.188:4723/wd/hub"; } }

        public static AppiumHelpers.Platforms PlatformTesting { get { return Platforms.Android4; } }
        public static AppiumHelpers.Devices DeviceTesting { get { return AppiumHelpers.Devices.Tablet; } }

        public static void Wait(int seconds, By xPath, bool newExecutation = false)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(xPath));
        }

        public static void Tap(IWebElement element)
        {
            int x = (element.Size.Width + element.Location.X) / 2;
            int y = element.Location.Y + (element.Size.Height / 2);
            if (x > element.Size.Width)
                x = element.Size.Width;
            else if (x < 0)
            {
                x = element.Size.Width;
            }

            Driver.Tap(1, x, y, 1000);

            //  Driver.Tap(1, element.Location.X, element.Location.Y, 1000);
        }

        public static void Tap(int x, int y)
        {

            Driver.Tap(1, x, y, 1000);
        }



        public static void LogTimeExecutation(DateTime beginTimeTest)
        {
            TimeSpan ts = DateTime.Now.Subtract(beginTimeTest);
            string path = "c:\\lixos\\" + AppName + "-" + PlatformTesting.ToString() + "-" + DeviceTesting.ToString() + ".txt";
            int count = 0;
            if (File.Exists(path))
                count = File.ReadLines(path).Count();
            TextWriter tw = new StreamWriter(path, true);
            
            tw.WriteLine(count + " - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + ts.TotalMilliseconds);
            tw.Close();
        }

        public static void GetScreenshot()
        {
            //Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            //byte[] screenshotAsByteArray = screenshot.AsByteArray;
            //screenshot.SaveAsFile("c:\\lixos\\" + AppName + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "-" + PlatformTesting.ToString() + "-" + DeviceTesting.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static object JavaScriptExecutor(string js)
        {
            IJavaScriptExecutor x = (IJavaScriptExecutor)Driver;
            return x.ExecuteScript(js);
        }

        public static By BuildXPathPlatform(XPathPlatform xpathPlatform)
        {
            if (PlatformTesting == Platforms.Android4)
            {
                return By.XPath(xpathPlatform.XPathAndroid4);
            }
            else if (PlatformTesting == Platforms.Android5)
            {
                return By.XPath(xpathPlatform.XPathAndroid5);
            }
            else if (PlatformTesting == Platforms.Android6)
            {
                return By.XPath(xpathPlatform.XPathAndroid6);
            }
            else if (PlatformTesting == Platforms.IOS7)
            {
                return By.XPath(xpathPlatform.XPathIOS7);
            }
            else if (PlatformTesting == Platforms.IOS9)
            {
                return By.XPath(xpathPlatform.XPathIOS9);
            }
            return By.XPath("");

        }

        public static int GetCountXMLNodes(string xml, bool clearOutPutFile = false)
        {
            XmlDocument readDoc = new XmlDocument();
            readDoc.LoadXml(xml);
            int count = readDoc.SelectNodes("descendant::*").Count;
            TextWriter tw = new StreamWriter("c:\\lixos\\" + AppName + "-" + PlatformTesting.ToString() + "-" + DeviceTesting.ToString() + "-XML-NodeCount.txt", !clearOutPutFile);
            tw.WriteLine(count.ToString());
            tw.Close();

            return count;
        }



    }

}
