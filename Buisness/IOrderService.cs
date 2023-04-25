using Common.DTO.Order;
using Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public interface IOrderService
    {
        Task<OrderDTO> Add(CreateOrderDTO order, UserDTO user);
        Task Delete(int id);
        Task<OrderDTO> Update(UpdateOrderDTO orderDTO, UserDTO userDTO);
        Task<OrderDTO> Get(int id);
        Task<List<OrderDTO>> GetAll();
    }
}
