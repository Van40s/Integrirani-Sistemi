using MovieApp.Domain.DTO;
using MovieApp.Domain.Models;
using MovieApp.Repository.Interface;
using MovieApp.Service.Interface;


namespace MovieApp.Service.Implementation
{
    public class OrderServiceImpl : IOrderService
    {

        private readonly IRepository<Order> _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Ticket> _ticketRepository;

        public OrderServiceImpl(IRepository<Order> orderRepository, IUserRepository userRepository, IRepository<Ticket> ticketRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        public Order AddToCart(string userId, AddToOrderDTO dto)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.order;

                var selectedProduct = _ticketRepository.Get(dto.ticketId);

                if (selectedProduct != null && userCart != null)
                {
                    userCart?.ticketInOrder?.Add(new TicketInOrder
                    {
                        ticket = selectedProduct,
                        ticketId = selectedProduct.id,
                        order = userCart,
                        orderId = userCart.id,
                        quantity = dto.quantity
                    });

                    return _orderRepository.Update(userCart);
                }
            }
            return null;

        }

        public Order GetOrder(string userId)
        {
            Order order = _userRepository.Get(userId).order;

            
            var user2 = _userRepository.Get(userId);

            

            if(order != null || user2 != null)
            {
                return order;
            }

            EShopApplicationUser user = _userRepository.Get(userId);
            user.order = new Order();
            _userRepository.Update(user);

            return _userRepository.Get(userId).order;
        }

        public Order payOrder(string userId)
        {
            EShopApplicationUser user = _userRepository.Get(userId);

            user.order = new Order();

            _userRepository.Update(user);

            return user.order;
        }
    }
}
