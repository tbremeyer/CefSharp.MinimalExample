using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        private bool _firstTime = true;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Dialog_Loaded;
            this.Browser.FrameLoadEnd += (sender, args) => { Task.Run(CallWebSite); }; 
        }

        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.RequestHandler = new MinimalExampleHandler();
        }

        private async Task CallWebSite()
        {
            if (!_firstTime) return;
            var result = await Browser.EvaluateScriptAsync($"httpGet('http://httpbin.org/basic-auth/undefined/undefined?accept=json')");
            _firstTime = false;
        }
    }
}
