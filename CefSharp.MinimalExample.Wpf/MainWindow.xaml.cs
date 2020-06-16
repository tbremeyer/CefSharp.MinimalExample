using System;
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
            Browser.ShowDevTools();
            var result = await Browser.EvaluateScriptAsync($"addWMSLayer('http://wms.zh.ch/Raster1000CWMS', 'Landeskarte 1:500000 / web.wms.zh.ch', 'http://web.wms.zh.ch', 'lk500', 'image/png', 'mapserver', '369997', '320000', '280000', '220000')");
            _firstTime = false;
            initializeMap();
        }
        
        private async Task initializeMap()
        {
            await this.addMarker();
            await this.zoomMap();
        }     
        
        private async Task addMarker()
        {
            try
            {
                var response = await Browser.EvaluateScriptAsync("addMarker()", new TimeSpan(0, 0, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }    
   
        private async Task zoomMap()
        {
            try
            {
                var east = 2679144.1499999999m;
                var north = 1247313.99m;
                int defaultZoom = 12;

                //var script = string.Format("zoomMap('{0}', '{1}')", east, north);
                var script = $"setDefaultParameter('{east}', '{north}', '{defaultZoom}')";
                Browser.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }        
   }
}
