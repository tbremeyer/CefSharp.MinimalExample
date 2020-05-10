using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CefSharp.MinimalExample.Wpf
{
    public class ECBSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public ECBSchemeHandlerFactory()
        {
            Debug.WriteLine("Constructor");
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var uri = new Uri(request.Url);
            var file = uri.Authority + uri.AbsolutePath;
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resourcePath = "CefSharp.MinimalExample.Wpf." + file.Replace("/", ".");

            if (executingAssembly.GetManifestResourceInfo(resourcePath) != null)
            {
                var resourceStream = executingAssembly.GetManifestResourceStream(resourcePath);
                var memoryStream = new MemoryStream();
                resourceStream.CopyTo(memoryStream);
                resourceStream.Close();
                memoryStream.Position = 0;
                return ResourceHandler.FromStream(memoryStream);
            }

            return new ResourceHandler();
        }
    }
}