using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.BasketAggregate;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class GuardExtensions
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, Basket basket)
        {
            if (basket == null) throw new BasketNotFoundException(basketId);
        }
    }
}
