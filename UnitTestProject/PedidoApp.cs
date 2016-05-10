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
    public class PedidoApp 
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

                capabilities.SetCapability("AUTOMATION_NAME", "Selendroid");
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
                capabilities.SetCapability("app", @"C:\Apps\android\pedidoapp\platforms\android\build\outputs\apk\android-debug-unaligned.apk");
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
                    capabilities.SetCapability("udid", "b35c1fcf89f97266dac59d48037b52dfabe5fccb");
                    capabilities.SetCapability("platformVersion", "9.3");

                }
                else if (AppiumHelpers.DeviceTesting == AppiumHelpers.Devices.SmartPhone)
                {
                    capabilities.SetCapability("deviceName", "iPhoneAndre");
                    capabilities.SetCapability("platformVersion", "7.1");

                    capabilities.SetCapability("udid", "86d46f396c14bf0ecceaa406657ccf51c613c97f");
                }

                capabilities.SetCapability("appPackage", "com.unoeste.teste");
                capabilities.SetCapability("bundleId", "com.unoeste.teste");
                _driver = new IOSDriver<IWebElement>(defaultUri, capabilities);

            }
            else
            {
                throw new Exception("Plataforma não definida");
            }

            AppiumHelpers.Driver = _driver;
            AppiumHelpers.AppName = "PedidoApp";

        }


        [TestMethod]
        public void OrderTest()
        {

            //-----------------------------------------------------------------------------
            //EVENTO: SELECIONAR O PRIMEIRO EVENTO DA LISTA
            //-----------------------------------------------------------------------------   
            By itemXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
            {
                XPathAndroid4 = "//android.webkit.WebView/android.view.View/android.view.View",
                XPathAndroid5 = "//android.webkit.WebView/android.view.View/android.view.View",
                XPathAndroid6 = "//android.webkit.WebView/android.view.View/android.view.View",
                XPathIOS7 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAWebView[1]",
                XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIALink[1]/UIALink[1]"
            });
            AppiumHelpers.Wait(60, itemXPath, true);
            AppiumHelpers.GetScreenshot();
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource, true);
            AppiumHelpers.Wait(60, itemXPath, true);

            IReadOnlyList<IWebElement> itens = _driver.FindElements(itemXPath);

            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android4)
            {
                try
                {
                    AppiumHelpers.Tap(itens[1]);
                }
                catch
                {
                    AppiumHelpers.Tap(itens[0]);
                }
            }
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5)
            {
                itens[0].Click();
            }
            else AppiumHelpers.Tap(itens[0]);


            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "PEDIR ESSE BOLO AGORA"
            //-----------------------------------------------------------------------------   
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7)
            {
                _driver.Tap(1, 167, 390, 1000);
            }
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android4)
            {
                _driver.Tap(1, 274, 530, 5000);
            }
            else
            {
                By btnOrderXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
                {
                    XPathAndroid4 = "//android.view.View[@content-desc='Pedir esse bolo agora Link']",
                    XPathAndroid5 = "//android.view.View[@content-desc='Pedir esse bolo agora ']",
                    XPathAndroid6 = "//android.view.View[@content-desc='Pedir esse bolo agora ']",
                    XPathIOS7 = "ERROR: elemento não disponível no XML",
                    XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIALink[1]/UIAStaticText[1]"
                });
                AppiumHelpers.Wait(60, btnOrderXPath);
                AppiumHelpers.GetScreenshot();

                if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5)
                {
                    _driver.FindElement(btnOrderXPath).Click();
                }
                else AppiumHelpers.Tap(_driver.FindElement(btnOrderXPath));
            }

            //-----------------------------------------------------------------------------
            //EVENTO: CLICAR NA AÇÃO "CONFIRMA PEDIDO"
            //-----------------------------------------------------------------------------   
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);

            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7)
            {
                _driver.Tap(1, 155, 415, 1000);
            }
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android6)
            {
                _driver.Tap(1, 155, 762, 1000);
            }
            else if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5)
            {
                AppiumHelpers.Tap(295, 579);
            }
            else
            {
                By btnOrderConfirmXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
                {
                    XPathAndroid4 = "//android.widget.Button[@content-desc='Confirmar Pedido!']",
                    XPathAndroid5 = "//android.widget.Button[@content-desc='Confirmar Pedido! ']",
                    XPathAndroid6 = "//android.widget.Button[@content-desc='Confirmar Pedido! ']",
                    XPathIOS7 = "ERROR: elemento não disponível no XML",
                    XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAButton[2]"
                });
                AppiumHelpers.Wait(10, btnOrderConfirmXPath);
                AppiumHelpers.GetScreenshot();
                AppiumHelpers.Tap(_driver.FindElement(btnOrderConfirmXPath));
            }

            //-----------------------------------------------------------------------------
            //EVENTO: VERIFICAR MENSAGEM "PEDIDO CONFIRMADO"
            //-----------------------------------------------------------------------------   
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);

            if (AppiumHelpers.PlatformTesting != AppiumHelpers.Platforms.IOS7)
            {
                By btnOrderAlertConfirmXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
                {
                    XPathAndroid4 = "//android.view.View[@content-desc='Pedido confirmado! Heading']",
                    XPathAndroid5 = "//android.view.View[@content-desc='Pedido confirmado!']",
                    XPathAndroid6 = "//android.view.View[@content-desc='Pedido confirmado!']",
                    XPathIOS7 = "ERROR: Não suportado",
                    XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAStaticText[@name='Pedido confirmado!']"
                });
                AppiumHelpers.Wait(20, btnOrderAlertConfirmXPath);
                AppiumHelpers.GetScreenshot();
                var btnOrderAlertConfirm = _driver.FindElement(btnOrderAlertConfirmXPath);

                Assert.AreNotEqual(btnOrderAlertConfirm, null);
            }


            //-----------------------------------------------------------------------------
            //EVENTO: VOLTAR PARA LISTA DE PRODUTOS
            //-----------------------------------------------------------------------------   
            AppiumHelpers.GetCountXMLNodes(_driver.PageSource);
            if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.IOS7)
            {
                _driver.Tap(1, 167, 282, 1000);
            }
            else
            { 
                By btnOrderAlertConfirmOKXPath = AppiumHelpers.BuildXPathPlatform(new XPathPlatform()
                {
                    XPathAndroid4 = "//android.widget.Button[@content-desc='OK']",
                    XPathAndroid5 = "//android.widget.Button[@content-desc='OK ']",
                    XPathAndroid6 = "//android.widget.Button[@content-desc='OK ']",
                    XPathIOS7 = "ERROR: Não suportado",
                    XPathIOS9 = "//UIAApplication[1]/UIAWindow[1]/UIAScrollView[2]/UIAWebView[1]/UIAStaticText[@name='Pedido confirmado!']"
                });
                AppiumHelpers.Wait(20, btnOrderAlertConfirmOKXPath);
                AppiumHelpers.GetScreenshot();
                var btnOrderAlertConfirmOK = _driver.FindElement(btnOrderAlertConfirmOKXPath);
                AppiumHelpers.GetCountXMLNodes(_driver.PageSource);

                if (AppiumHelpers.PlatformTesting == AppiumHelpers.Platforms.Android5)
                    btnOrderAlertConfirmOK.Click();
                else AppiumHelpers.Tap(btnOrderAlertConfirmOK);
            }
        }


        [TestCleanup]
        public void End()
        {
            //Registra o tempo de execução
            AppiumHelpers.LogTimeExecutation(_beginTimeTest);

            if (_driver != null)
            {
                _driver.CloseApp();
                _driver.Quit();
            }
        }
    }

}
