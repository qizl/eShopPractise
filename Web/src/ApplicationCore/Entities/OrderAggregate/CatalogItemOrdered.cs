using Ardalis.GuardClauses;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate
{
    public class CatalogItemOrdered // ValueObject
    {
        public int CatalogItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUri { get; set; }

        private CatalogItemOrdered() { }

        public CatalogItemOrdered(int catalogItemId, string productName, string pictureUri)
        {
            Guard.Against.OutOfRange(catalogItemId, nameof(catalogItemId), 1, int.MaxValue);
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));

            CatalogItemId = catalogItemId;
            ProductName = productName;
            PictureUri = pictureUri;
        }
    }
}
