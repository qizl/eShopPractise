using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Model
{
    public class CustomerBasket
    {
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
            Items = new List<BasketItem>();
        }
    }

    public class UpdateBasket
    {
        public string BuyerId { get; set; }
        public List<UpdateBasketItem> Updates { get; set; }

        public class UpdateBasketItem
        {
            public string BasketItemId { get; set; }
            public int NewQty { get; set; }
        }
    }
}
