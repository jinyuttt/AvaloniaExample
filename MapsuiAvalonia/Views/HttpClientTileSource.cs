using BruTile;
using BruTile.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapsuiAvalonia.Views
{
    internal class HttpClientTileSource : ITileSource
    {

        private readonly HttpClient _HttpClient;
        private readonly HttpTileSource _WrappedSource;

        public HttpClientTileSource(HttpClient httpClient, ITileSchema tileSchema, string urlFormatter, IEnumerable<string> serverNodes = null, string apiKey = null, string name = null, BruTile.Cache.IPersistentCache<byte[]> persistentCache = null, Attribution attribution = null)
        {
            _HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _WrappedSource = new HttpTileSource(tileSchema, urlFormatter, serverNodes, apiKey, name, persistentCache, ClientFetch, attribution);
        }

        public ITileSchema Schema => _WrappedSource.Schema;

        public string Name => _WrappedSource.Name;

        public Attribution Attribution => _WrappedSource.Attribution;


        public Task<byte[]?> GetTileAsync(TileInfo tileInfo)
        {
           return _WrappedSource.GetTileAsync(tileInfo);
        }

        private Task<byte[]?> ClientFetch(Uri uri) => _HttpClient.GetByteArrayAsync(uri);

    }
}