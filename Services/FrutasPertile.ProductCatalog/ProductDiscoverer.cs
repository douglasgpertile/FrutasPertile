using AngleSharp;
using AngleSharp.Dom;

using System.Text.RegularExpressions;

namespace FrutasPertile.ProductCatalog
{
    public class ProductDiscoverer
    {
        private readonly string _googleMyBusinesFrutasPertileProductsUrl = 
            "https://www.google.com/local/place/products/catalog?g2lb=4822981,4914647,4975983,72375911,72431053&hl=pt-BR&gl=br&cs=1&ssta=1&ludocid=12981356646821399990&origin=https://www.google.com";

        private readonly string _productNameSplitToken = "R$";

        private IBrowsingContext _browsingContext = BrowsingContext.New(Configuration
            .Default
            .WithDefaultLoader()
         );

        private static Regex _imagePathRegex = new Regex(@"url\((.*?)=", RegexOptions.Compiled);

        public async Task<IEnumerable<Product>> DiscoverProductsAsync()
        {
            var productsPage = await GetGoogleMyBusinessWebPageAsync();
            var productsCards = SelectProductCardsElements(productsPage);

            return productsCards.Select(CreateProduct);         
        }

        private Task<IDocument> GetGoogleMyBusinessWebPageAsync() =>
            _browsingContext.OpenAsync(_googleMyBusinesFrutasPertileProductsUrl);
        
        private IEnumerable<IElement> SelectProductCardsElements(IDocument document) => 
            document.QuerySelectorAll("div[data-id]").Skip(1);

        private Product CreateProduct(IElement cardElement)
        {
            var nameAndPrice = cardElement.TextContent.Split(_productNameSplitToken);
            var name = nameAndPrice[0].Trim();
            var price = nameAndPrice.ElementAtOrDefault(1)?.Trim();

            var imageElement = cardElement.FirstElementChild?.FirstElementChild;
            var style = imageElement?.Attributes.First(a => a.Name == "style");         

            var match = _imagePathRegex.Match(style.Value);
            var imagePath = match.Groups[1].Value;
            var fullHdImagePath = imagePath + "=w1980-h1020";

            return new Product
            {
                Name = name,
                Price = price,
                ImageUrl = fullHdImagePath
            };
        }


    }
}
