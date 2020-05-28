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
            var result = await Browser.EvaluateScriptAsync($"addWMSLayer('http://wms.zh.ch/Raster1000CWMS', 'Landeskarte 1:500000 / web.wms.zh.ch', 'http://web.wms.zh.ch', 'lk500', 'image/png', 'mapserver', '369997', '320000', '280000', '220000')");
            _firstTime = false;
        }
    }
}
