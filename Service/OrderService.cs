using AutoMapper;
using Buisness;
using Common.DTO.Order;
using Common.DTO.User;
using Common.Model;
using DataAccess.Repository;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private IRepository<Order> _orderRepository;
        private IMapper _mapper;
        private IUnitOfWork _worker;
        
        public OrderService(IRepository<Order> orderRepository, IMapper mapper, IUnitOfWork worker)
        {
            _worker = worker;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> Add(CreateOrderDTO order, UserDTO user)
        {
            var orderEntity = _mapper.Map<Order>(order);

            orderEntity.UserId = user.Id;
            //orderEntity.User = _mapper.Map<User>(user);

            orderEntity.OrderState = OrderState.Pending;

            _orderRepository.Add(orderEntity);
            await _worker.SaveIntoDbContextAsync();
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public async Task Delete(int id)
        {
            var orderToDelete = _orderRepository.Get(id);
            _orderRepository.Delete(orderToDelete);
            await _worker.SaveIntoDbContextAsync();
        }

        public async Task<OrderDTO> Update(UpdateOrderDTO orderDTO, UserDTO user)
        {
            var order = _orderRepository.Get(orderDTO.Id);
            var orderEntity = _mapper.Map(orderDTO, order);

            _orderRepository.Update(orderEntity);
            await _worker.SaveIntoDbContextAsync();
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public async Task<OrderDTO> Get(int id)
        {
            var order = _orderRepository.Get(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}
