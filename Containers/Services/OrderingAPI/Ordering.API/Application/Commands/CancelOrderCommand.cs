using MediatR;
using System.Runtime.Serialization;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands
{
    public class CancelOrderCommand : IRequest<bool>
    {

        [DataMember]
        public int OrderNumber { get; private set; }

        public CancelOrderCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
