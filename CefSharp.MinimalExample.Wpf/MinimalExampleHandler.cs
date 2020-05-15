using CefSharp.Handler;

namespace CefSharp.MinimalExample.Wpf
{
    public class MinimalExampleHandler : RequestHandler
    {
        private static MinimalExampleResourceRequestHandler _minimalExampleResourceRequestHandler;
        private IResourceRequestHandler ResourceRequestHandler => _minimalExampleResourceRequestHandler ?? (_minimalExampleResourceRequestHandler = new MinimalExampleResourceRequestHandler());

        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return ResourceRequestHandler;
        }

        protected override bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            const string user = "user";
            const string passw = "passwd";
            
            callback.Continue(user,passw);
            return true;
        }

        protected override void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {
            chromiumWebBrowser.Reload();
        }
    }
}