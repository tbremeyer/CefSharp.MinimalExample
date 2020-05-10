using CefSharp.Wpf;
using System;
using System.IO;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
#if !NETCOREAPP
            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };
            settings.SetOffScreenRenderingBestPerformanceArgs();
            settings.MultiThreadedMessageLoop = true;

            //Example of setting a command line argument
            //Enables WebRTC
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "ecb",
                SchemeHandlerFactory = new ECBSchemeHandlerFactory()
            });

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            
            Cef.AddCrossOriginWhitelistEntry("ecb://web", "http" ,string.Empty,true);
            Cef.AddCrossOriginWhitelistEntry("ecb://web", "https" ,string.Empty,true);
#endif
        }
    }
}
