using EnjoyCodes.eShopOnContainers.WebMVC.Models;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public interface ILocationService
    {
        Task CreateOrUpdateUserLocation(LocationDTO location);
    }
}
