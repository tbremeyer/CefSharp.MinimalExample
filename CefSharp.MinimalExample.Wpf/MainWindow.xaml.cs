using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        private int _maxCalls = 10;
        private int _callsPerformed = 0;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Dialog_Loaded;
            this.Browser.FrameLoadEnd += (sender, args) => { Task.Run(CallWebSite); }; }

        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.RequestHandler = new MinimalExampleHandler();
        }

        private async Task CallWebSite()
        {
            if (_callsPerformed > _maxCalls) return;
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resourcePath = "CefSharp.MinimalExample.Wpf" + ".web.index.html";

            if (executingAssembly.GetManifestResourceInfo(resourcePath) != null)
            {
                var resourceStream = executingAssembly.GetManifestResourceStream(resourcePath);
                var memoryStream = new MemoryStream();
                await resourceStream.CopyToAsync(memoryStream);
                resourceStream.Close();
                memoryStream.Position = 0;
                var fileText = await new StreamReader(memoryStream).ReadToEndAsync();
                Browser.LoadHtml(fileText, true);
            }

            var result = await Browser.EvaluateScriptAsync($"httpGet('http://httpbin.org/basic-auth/undefined/undefined?accept=json')");
            _callsPerformed++;
        }
    }
}
