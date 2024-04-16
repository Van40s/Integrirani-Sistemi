

using MovieApp.Domain.DTO;
using MovieApp.Domain.Models;

namespace MovieApp.Service.Interface
{
    public interface IOrderService
    {
        Order GetOrder(string userId);

        Order AddToCart(string userId, AddToOrderDTO dto);

        Order payOrder(string userId);
    }
}
