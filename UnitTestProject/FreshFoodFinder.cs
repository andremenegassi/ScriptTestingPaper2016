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
    public class FreshFoodFinder
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
                    capabilities.SetCapability("platformVersion", "6.0.1");
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Tablet && AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android4)
                {
                    capabilities.SetCapability("deviceName", "3004d4afca618200");
                    capabilities.SetCapability("platformName", "Android");
                    capabilities.SetCapability("platformVersion", "4.4.4");
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.SmartPhone && AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5)
                {
                    capabilities.SetCapability("deviceName", "0427123814");
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
                capabilities.SetCapability("app", @"C:\Apps\android\fresh-food-finder\platforms\android\build\outputs\apk\android-debug-unaligned.apk");
                _driver = new AndroidDriver<IWebElement>(defaultUri, capabilities);
            }
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
            {
                Uri defaultUri = new Uri(AppiumHelpers.ServerIOS1);

                capabilities.SetCapability("platformName", "iOS");
                capabilities.SetCapability("platformVersion", "7.1.2");
                capabilities.SetCapability("platform", "Mac");

                if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Emulador)
                {
                    //não suportado
                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.Tablet)
                {
                    capabilities.SetCapability("deviceName", "iPadWeb");
                    capabilities.SetCapability("udid", "b35c1fcf89f97266dac59d48037b52dfabe5fccb");
                }

                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.SmartPhone)
                {
                    capabilities.SetCapability("deviceName", "iPhoneAndre");
                    capabilities.SetCapability("udid", "86d46f396c14bf0ecceaa406657ccf51c613c97f");
                }

                capabilities.SetCapability("appPackage", "com.unoeste.teste");
                capabilities.SetCapability("appActivity", "com.unoeste.teste.MainViewController");
                capabilities.SetCapability("bundleId", "com.unoeste.teste");
                _driver = new IOSDriver<IWebElement>(defaultUri, capabilities);

            }
            else
            {
                throw new Exception("Plataforma não definida");
            }

            AppiumHelpers.Driver = _driver;
            AppiumHelpers.AppName = "FFF";
        }


        [TestMethod]
        public void SearchMarketTest()
        {

            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA OPÇÃO "SEARCH FOR A MARKET"
            //-----------------------------------------------------------------------------
            By btnSearchMarketXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View[@content-desc='Search For a Market Link']",
                XPathAndroid5 = "//android.view.View[@content-desc='Search For a Market']",
                XPathAndroid6 = "//android.view.View[@content-desc='Search For a Market']",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIALink[2]/UIAStaticText[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIALink[2]"
            });
            AppiumHelpers.Wait(10, btnSearchMarketXPath, true);
            AppiumHelpers.GetScreenshot();
            var btnSearchMarket = _driver.FindElement(btnSearchMarketXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource, true);
            AppiumHelpers.Tap(btnSearchMarket);


            //-----------------------------------------------------------------------------
            //EVENTO: INSERIR UM VALOR INEXISTENTE
            //-----------------------------------------------------------------------------
            By inputSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View/android.widget.EditText",
                XPathAndroid5 = "//android.view.View/android.widget.EditText",
                XPathAndroid6 = "//android.view.View/android.widget.EditText",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIATextField[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIATextField[1]"
            });
            AppiumHelpers.Wait(20, inputSearchXPath);
            AppiumHelpers.GetScreenshot();
            var inputSeatch = (_driver).FindElement(inputSearchXPath);
            AppiumHelpers.Tap(inputSeatch);
            inputSeatch.SendKeys("xpto");


            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "SEARCH"
            //-----------------------------------------------------------------------------
            By btnSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View[@content-desc='Search Link']",
                XPathAndroid5 = "//android.view.View[@content-desc='Search']",
                XPathAndroid6 = "//android.view.View[@content-desc='Search']",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIALink[1]/UIAStaticText[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIALink[1]"
            });
            AppiumHelpers.Wait(10, btnSearchXPath);
            AppiumHelpers.GetScreenshot();
            var btnSearch = _driver.FindElement(btnSearchXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            AppiumHelpers.Tap(btnSearch);

            //-----------------------------------------------------------------------------
            //EVENTO: MOSTRAR LISTA DE RESULTADO VAZIA
            //-----------------------------------------------------------------------------
            By listViewItemsXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.webkit.WebView/android.widget.ListView/android.view.View",
                XPathAndroid5 = "//android.webkit.WebView/android.widget.ListView/android.view.View",
                XPathAndroid6 = "//android.webkit.WebView/android.widget.ListView/android.view.View",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/*",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/*"
            });
            AppiumHelpers.GetScreenshot();
            IReadOnlyList<IWebElement> listViewItems = _driver.FindElements(listViewItemsXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7)
                Assert.AreEqual(listViewItems.Count, 4); //vazia
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                Assert.AreEqual(listViewItems.Count, 5); //vazia
            else Assert.AreEqual(listViewItems.Count, 0); //vazia


            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "VOLTAR"
            //-----------------------------------------------------------------------------
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7)
            {
                _driver.Tap(1, 25, 25, 1000);
            }
            else
            {
                By btnBackXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
                {
                    XPathAndroid4 = "//android.view.View[@content-desc='Back']",
                    XPathAndroid5 = "//android.webkit.WebView/android.view.View",
                    XPathAndroid6 = "//android.webkit.WebView/android.view.View",
                    XPathIOS7 = "ERROR: elemento não disponível no XML",
                    XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIAStaticText[2]"
                });
                AppiumHelpers.Wait(10, btnBackXPath);
                AppiumHelpers.GetScreenshot();
                IReadOnlyList<IWebElement> btnBack = _driver.FindElements(btnBackXPath);
                AppiumHelpers.GetCountXMLNodes(_driver.PageSource);

                if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android4 || AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                    AppiumHelpers.Tap(btnBack[0]);
                else AppiumHelpers.Tap(btnBack[1]);
            }


            //-----------------------------------------------------------------------------
            //EVENTO: INSERIR UM VALOR EXISTENTE                                           
            //-----------------------------------------------------------------------------
            inputSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View/android.widget.EditText",
                XPathAndroid5 = "//android.view.View/android.widget.EditText",
                XPathAndroid6 = "//android.view.View/android.widget.EditText",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIATextField[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIATextField[1]"
            });

            AppiumHelpers.Wait(10, inputSearchXPath);
            AppiumHelpers.GetScreenshot();
            inputSeatch = _driver.FindElement(inputSearchXPath);
            AppiumHelpers.Tap(inputSeatch);
            inputSeatch.SendKeys("");
            inputSeatch.Clear();
            inputSeatch.SendKeys("alexander");


            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "SEARCH"
            //-----------------------------------------------------------------------------
            btnSearchXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.view.View[@content-desc='Search Link']",
                XPathAndroid5 = "//android.view.View[@content-desc='Search']",
                XPathAndroid6 = "//android.view.View[@content-desc='Search']",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIALink[1]/UIAStaticText[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/UIALink[1]"
            });
            AppiumHelpers.Wait(10, btnSearchXPath);
            AppiumHelpers.GetScreenshot();
            btnSearch = _driver.FindElement(btnSearchXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            AppiumHelpers.Tap(btnSearch);


            //-----------------------------------------------------------------------------
            //EVENTO: MOSTRAR LISTA DE RESULTADO PREENCHIDA
            //-----------------------------------------------------------------------------
            listViewItemsXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.webkit.WebView/android.view.View/android.widget.ListView/android.view.View",
                XPathAndroid5 = "//android.webkit.WebView/android.widget.ListView/android.view.View",
                XPathAndroid6 = "//android.webkit.WebView/android.widget.ListView/android.view.View",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/*",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]/*"
            });

            AppiumHelpers.Wait(10, listViewItemsXPath);
            AppiumHelpers.GetScreenshot();
            listViewItems = _driver.FindElements(listViewItemsXPath);
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            //Verifica se a quantidade de elementos do resultado é diferente de zero (0). Se sim, então foram encontrados resultados.
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7)
                Assert.AreNotEqual(listViewItems.Count, 4); //tem resultado
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS9)
                Assert.AreNotEqual(listViewItems.Count, 5); //tem resultado
            else Assert.AreNotEqual(listViewItems.Count, 0); //tem resultado

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
