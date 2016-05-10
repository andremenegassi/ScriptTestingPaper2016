using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.MultiTouch;

namespace UnitTestProject
{
    [TestClass]
    public class PedidoApp 
    {

        IWebDriver _driver = null;
        AppiumHelpers.Platforms _platform = AppiumHelpers.Platforms.Android;
        AppiumHelpers.Devices _device = AppiumHelpers.Devices.SmartPhone;
        string _appName = "PedidoApp";

        [TestInitialize]
        public void Begin()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();

            if (_platform == AppiumHelpers.Platforms.Android)
            {
                Uri defaultUri = new Uri(AppiumHelpers.ServerAndroid1);

                capabilities.SetCapability("device", "Android");
                capabilities.SetCapability(CapabilityType.Platform, "Windows");

                if (_device == AppiumHelpers.Devices.Emulador)
                {
                    capabilities.SetCapability("deviceName", "emulator-5554");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "6.0");
                }
                else if (_device == AppiumHelpers.Devices.Tablet)
                {
                    capabilities.SetCapability("deviceName", "0123456789ABCDEF");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "4.0.1");
                }
                else if (_device == AppiumHelpers.Devices.SmartPhone)
                {
                    capabilities.SetCapability("deviceName", "0427123814"); // "0936a287021e8bd3");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "6.0.1");
                }
                capabilities.SetCapability("takesScreenshot", "true");
                capabilities.SetCapability("appPackage", "com.unoeste.teste");
                capabilities.SetCapability("appActivity", "com.unoeste.teste.MainActivity");
                capabilities.SetCapability("app", @"C:\Apps\android\pedidoapp\platforms\android\build\outputs\apk\android-debug-unaligned.apk");
                _driver = new RemoteWebDriver(defaultUri, capabilities);
            }
            else if (_platform == AppiumHelpers.Platforms.IOS)
            {
                Uri defaultUri = new Uri(AppiumHelpers.ServerIOS1);

                capabilities.SetCapability("platformName", "iOS");
                capabilities.SetCapability("platformVersion", "7.1");
                capabilities.SetCapability("platform", "Mac");

                if (_device == AppiumHelpers.Devices.Emulador)
                {
                    //não suportado
                }
                else if (_device == AppiumHelpers.Devices.Tablet)
                {
                    capabilities.SetCapability("deviceName", "iPadCWeb");
                    capabilities.SetCapability("udid", "");
                }
                else if (_device == AppiumHelpers.Devices.SmartPhone)
                {
                    capabilities.SetCapability("deviceName", "iPhoneAndre");
                    capabilities.SetCapability("udid", "86d46f396c14bf0ecceaa406657ccf51c613c97f");
                }

                capabilities.SetCapability("appPackage", "com.unoeste.teste");
                capabilities.SetCapability("appActivity", "com.unoeste.teste.MainViewController");
                //capabilities.SetCapability("app", @"C:\Apps\android\fresh-food-finder\platforms\android\build\outputs\apk\android-debug-unaligned.apk");
                capabilities.SetCapability("bundleId", "com.unoeste.teste");
                _driver = new RemoteWebDriver(defaultUri, capabilities);

            }
            else
            {
                throw new Exception("Plataforma não definida");
            }
        }

       
        [TestMethod]
        public void OrderTest()
        {
   
            By itemXPath = AppiumHelpers.BuildXPathPlatform(_platform, new XPathPlatform()
            {
                XPathAndroid = "//android.webkit.WebView/android.view.View/android.view.View",
                XPathiOS = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]"
            });
            AppiumHelpers.Wait(_driver, 10, itemXPath, _appName, true);
            AppiumHelpers.GetScreenshot(_driver, "PedidoApp1", _platform, _device);
            _driver.FindElements(itemXPath)[0].Click();


            By btnOrderXPath = AppiumHelpers.BuildXPathPlatform(_platform, new XPathPlatform()
            {
                XPathAndroid = "//android.view.View[@content-desc='Pedir esse bolo agora ']",
                XPathiOS = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]"
            });
            AppiumHelpers.Wait(_driver, 10, btnOrderXPath, _appName);
            AppiumHelpers.GetScreenshot(_driver, "PedidoApp2", _platform, _device);
            _driver.FindElement(btnOrderXPath).Click();


            By btnOrderConfirmXPath = AppiumHelpers.BuildXPathPlatform(_platform, new XPathPlatform()
                {
                    XPathAndroid = "//android.widget.Button[@content-desc='Confirmar Pedido! ']",
                    XPathiOS = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]"
            });
            AppiumHelpers.Wait(_driver, 10, btnOrderConfirmXPath, _appName);
            AppiumHelpers.GetScreenshot(_driver, "PedidoApp3", _platform, _device);
            _driver.FindElement(btnOrderConfirmXPath).Click();


            By btnOrderAlertConfirmXPath = AppiumHelpers.BuildXPathPlatform(_platform, new XPathPlatform()
            {
                XPathAndroid = "//android.view.View[@content-desc='Pedido confirmado!']",
                XPathiOS = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]"
            });
            AppiumHelpers.Wait(_driver, 10, btnOrderAlertConfirmXPath, _appName);
            AppiumHelpers.GetScreenshot(_driver, "PedidoApp4", _platform, _device);
            var btnOrderAlertConfirm = _driver.FindElement(btnOrderAlertConfirmXPath);

            Assert.AreNotEqual(btnOrderAlertConfirm, null);

        }


        [TestCleanup]
        public void End()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }
    }

}
