using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class MemesPlay
    {

        AppiumDriver<IWebElement> _driver = null;
        DateTime _beginTimeTest = DateTime.MinValue;

        [TestInitialize]
        public void Begin()
        {
            //Tempo de início do teste
            _beginTimeTest = DateTime.Now;

            DesiredCapabilities capabilities = new DesiredCapabilities();

            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android4 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android6)
            {
                Uri defaultUri = new Uri(AppiumHelpers.ServerAndroid1);

                capabilities.SetCapability("device", "Android");
                capabilities.SetCapability(CapabilityType.Platform, "Windows");

                if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Emulador)
                {
                    capabilities.SetCapability("deviceName", "emulator-5554");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "6.0");
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Tablet && AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android4)
                {
                    capabilities.SetCapability("deviceName", "3004d4afca618200");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "4.4.4");
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.SmartPhone && AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5)
                {
                    capabilities.SetCapability("deviceName", "0427123814"); // "0936a287021e8bd3");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "5.1");
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.SmartPhone && AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android6)
                {
                    capabilities.SetCapability("deviceName", "0936a287021e8bd3");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "6.0.1");
                }
                capabilities.SetCapability("takesScreenshot", "true");
                capabilities.SetCapability("appPackage", "com.unoeste.teste");
                capabilities.SetCapability("appActivity", "com.unoeste.teste.MainActivity");
                capabilities.SetCapability("app", @"C:\Apps\android\memesplay\platforms\android\build\outputs\apk\android-debug-unaligned.apk");
                _driver = new AndroidDriver<IWebElement>(defaultUri, capabilities);
            }
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
            {
                Uri defaultUri = new Uri(AppiumHelpers.ServerIOS1);

                capabilities.SetCapability("platformName", "iOS");
                capabilities.SetCapability("platform", "Mac");

                if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Emulador)
                {
                    //não suportado
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Tablet)
                {
                    capabilities.SetCapability("deviceName", "iPadWeb");
                    capabilities.SetCapability("platformVersion", "9.0.0");
                    capabilities.SetCapability("udid", "b35c1fcf89f97266dac59d48037b52dfabe5fccb");
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.SmartPhone)
                {
                    capabilities.SetCapability("deviceName", "iPhoneAndre");
                    capabilities.SetCapability("platformVersion", "7.1.2");
                    capabilities.SetCapability("udid", "86d46f396c14bf0ecceaa406657ccf51c613c97f");
                }

                capabilities.SetCapability("appPackage", "com.unoeste.teste");
                capabilities.SetCapability("appActivity", "com.unoeste.teste.MainViewController");
                //capabilities.SetCapability("app", @"/Users/userweb/Desktop/Andre-Mestrado/Fresh-Food-Finder.app");
                capabilities.SetCapability("bundleId", "com.unoeste.teste");
                _driver = new IOSDriver<IWebElement>(defaultUri, capabilities);

            }
            else
            {
                throw new Exception("Plataforma não definida");
            }

            AppiumHelpers.Driver = _driver;
            AppiumHelpers.AppName = "MP";
        }


        [TestMethod]
        public void SearchTest()
        {

            //-----------------------------------------------------------------------------
            //EVENTO:CLICAR NA AÇÃO "PESQUISAR"
            //-----------------------------------------------------------------------------
            By btnSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//*/android.widget.Button",
                XPathAndroid5 = "//*/android.widget.Button",
                XPathAndroid6 = "//*/android.widget.Button",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAButton[3]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAButton[3]"

            });
            AppiumHelpers.Wait(20, btnSearchXPath, true);
            AppiumHelpers.GetScreenshot();
            var btnSearch = _driver.FindElements(btnSearchXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource, true);

            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                btnSearch[0].Click();
            else btnSearch[2].Click();

            //-----------------------------------------------------------------------------
            //EVENTO: INSERIR UM VALOR INEXISTENTE
            //-----------------------------------------------------------------------------
            By inputSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View[@content-desc='Pesquisar']",
                XPathAndroid5 = "//android.view.View[@content-desc='Pesquisar']",
                XPathAndroid6 = "//android.view.View[@content-desc='Pesquisar']",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIATextField[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIATextField[1]"

            });
            AppiumHelpers.Wait(10, inputSearchXPath);
            AppiumHelpers.GetScreenshot();
            var inputSearch = _driver.FindElement(inputSearchXPath);
            inputSearch.Click();
            inputSearch.SendKeys("xpto");

            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "PESQUISAR"
            //-----------------------------------------------------------------------------
            btnSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//*/android.widget.Button",
                XPathAndroid5 = "//*/android.widget.Button",
                XPathAndroid6 = "//*/android.widget.Button",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAButton[4]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAButton[3]"
            });

            AppiumHelpers.Wait(20, btnSearchXPath);
            AppiumHelpers.GetScreenshot();
            btnSearch = _driver.FindElements(btnSearchXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                btnSearch[0].Click();
            else btnSearch[2].Click();

            //-----------------------------------------------------------------------------
            //EVENTO: VERIFICAR MENSAGEM "SEM RESULTADO"
            //-----------------------------------------------------------------------------
            By noDataFoundXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//*/android.view.View[@content-desc='Nenhum Registro']",
                XPathAndroid5 = "//*/android.view.View[@content-desc='Nenhum Registro']",
                XPathAndroid6 = "//*/android.view.View[@content-desc='Nenhum Registro']",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAStaticText[24][@label='Nenhum Registro']",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAStaticText[24][@label='Nenhum Registro']"

            });
            AppiumHelpers.Wait(20, noDataFoundXPath);
            AppiumHelpers.GetScreenshot();
            var noDataFound = _driver.FindElement(noDataFoundXPath);
            Assert.AreNotEqual(noDataFound, null);


            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "PESQUISAR"
            //-----------------------------------------------------------------------------
            btnSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//*/android.widget.Button",
                XPathAndroid5 = "//*/android.widget.Button",
                XPathAndroid6 = "//*/android.widget.Button",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAButton[3]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAButton[3]"

            });

            AppiumHelpers.Wait(20, btnSearchXPath);
            AppiumHelpers.GetScreenshot();
            btnSearch = _driver.FindElements(btnSearchXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                btnSearch[0].Click();
            else btnSearch[2].Click();


            //-----------------------------------------------------------------------------
            //EVENTO: INSERIR UM VALOR EXISTENTE
            //-----------------------------------------------------------------------------
            inputSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View[@content-desc='Pesquisar']",
                XPathAndroid5 = "//android.view.View[@content-desc='Pesquisar']",
                XPathAndroid6 = "//android.view.View[@content-desc='Pesquisar']",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIATextField[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIATextField[1]"

            });
            AppiumHelpers.Wait(20, inputSearchXPath);
            AppiumHelpers.GetScreenshot();
            inputSearch = _driver.FindElement(inputSearchXPath);
            inputSearch.Click();
            inputSearch.Clear();
            inputSearch.SendKeys("video");


            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "PESQUISAR"
            //-----------------------------------------------------------------------------
            btnSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//*/android.widget.Button",
                XPathAndroid5 = "//*/android.widget.Button",
                XPathAndroid6 = "//*/android.widget.Button",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAButton[4]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAButton[3]"
            });
            AppiumHelpers.Wait(20, btnSearchXPath);
            AppiumHelpers.GetScreenshot();
            btnSearch = _driver.FindElements(btnSearchXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                btnSearch[0].Click();
            else btnSearch[2].Click();


            //-----------------------------------------------------------------------------
            //EVENTO: VERIFICAR LISTA DE RESULTADO
            //-----------------------------------------------------------------------------
            By listViewItemsXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.webkit.WebView/android.view.View[@content-desc='MemesPlay']/android.view.View/android.widget.Button",
                XPathAndroid5 = "//android.webkit.WebView[@content-desc='MemesPlay']/android.widget.Image",
                XPathAndroid6 = "//android.webkit.WebView[@content-desc='MemesPlay']/android.widget.Image",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAButton[9]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAButton[6]"

            });
            AppiumHelpers.Wait(60, listViewItemsXPath);
            AppiumHelpers.GetScreenshot();
            IReadOnlyCollection<IWebElement> listViewItems = _driver.FindElements(listViewItemsXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);

            Assert.AreNotEqual(listViewItems.Count, 0);

        }


        [TestCleanup]
        public void End()
        {
            //Registra o tempo de execução
            AppiumHelpers.LogTimeExecutation(_beginTimeTest);

            if (_driver != null)
            {
                _driver.Quit();
            }
        }
    }

}
