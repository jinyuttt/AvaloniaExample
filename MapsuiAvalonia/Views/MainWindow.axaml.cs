using Avalonia;
using Avalonia.Controls;
using BruTile.Predefined;
using Mapsui.Tiling.Layers;
using System.Net.Http;
using BruTile;
using Mapsui.Providers.Wms;
using Mapsui.Widgets.ScaleBar;
using System;
using BruTile.MbTiles;
using SQLite;

namespace MapsuiAvalonia.Views
{
    public partial class MainWindow : Window
    {
        private const string V = @"Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)";
        Mapsui.UI.Avalonia.MapControl Map;
       // private IEnumerable<string?> USER_AGENT= V;

        public MainWindow()
        {

            InitializeComponent();

#if DEBUG
            this.AttachDevTools();
#endif
             Map = this.FindControl<Mapsui.UI.Avalonia.MapControl>("map");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", V);

            var osmAttribution = new Attribution("© OpenStreetMap contributors", "https://www.openstreetmap.org/copyright");
            //  var osmSource = new HttpClientTileSource(httpClient, new GlobalSphericalMercator(), "http://online{s}.map.bdimg.com/onlinelabel/?qt=tile&x={x}&y={y}&z={z}&styles=pl&udt=20141103&scaler=1", new[] { "a", "b", "c" }, name: "OpenStreetMap", attribution: osmAttribution);
          //  string url = "https://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer/tile/{z}/{y}/{x}";
            string url = "http://online3.map.bdimg.com/onlinelabel/?qt=tile&x={0}&y={1}&z={2}&styles=pl&udt=20200727&scaler=1&p=1";
           // var osmSource = new HttpClientTileSource(httpClient, new GlobalSphericalMercator(), url);
           // var osmLayer = new TileLayer(osmSource) { Name = "百度地图" };

            var mbtilesTilesource = new MbTilesTileSource(new SQLiteConnectionString("Resources/china.mbtiles", false));
          //  var mbTilesTile =  mbtilesTilesource.GetTileAsync(new TileInfo { Index = new TileIndex(0, 0, 0) });
            var osmLayer = new TileLayer(mbtilesTilesource) { Name = "百度地图" };
            // Map.Map.Widgets.Enqueue(new ScaleBarWidget(Map.Map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            //   Map.Map.Widgets.Enqueue(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 20, MarginY = 40 });


            Map.Map.Layers.Add(osmLayer);
           

        }
        private static WmsProvider CreateWmsProvider()
        {
            const string wmsUrl = "https://geodata.nationaalgeoregister.nl/windkaart/wms?request=GetCapabilities";
            var provider = WmsProvider.CreateAsync(wmsUrl).GetAwaiter().GetResult();
            //provider.
            //{
            //    ContinueOnError = true,
            //    TimeOut = 20000,
            //    CRS = "EPSG:28992"
            //};

            //provider.AddLayer("windsnelheden100m");
            //provider.SetImageFormat(provider.OutputFormats[0]);
            return provider;
        }
    }

}
