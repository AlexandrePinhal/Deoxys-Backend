using Buisness;
using Common.DTO.Order;
using Common.DTO.User;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NegoSud.Controllers
{

    // POST 127.0.0.1:5000/orders
    [ApiController]
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _OrderBusiness;
        private readonly IUserService _userBusiness;

        public OrderController(ILogger<OrderController> logger, IOrderService OrderBusiness, IUserService userService)
        {
            _logger = logger;
            _OrderBusiness = OrderBusiness;
            _userBusiness = userService;
        }

        [HttpGet("{id}", Name = "Get Order")]
        public async Task<OrderDTO> Get(int id)
        {
            return await _OrderBusiness.Get(id);
        }

        [HttpGet("", Name = "Get all Order")]
        public async Task<List<OrderDTO>> GetAll()
        {
            return await _OrderBusiness.GetAll();
        }

        [HttpPost("", Name = "Create Order")]
        public async Task<OrderDTO> Add(CreateOrderDTO Order)
        {

            UserDTO self = await GetLoggedUser();

            if (self == null || self.Role != (int)Role.Admin)
            {
                return null;
            }

            return await _OrderBusiness.Add(Order, self);
        }

        [HttpPut("", Name = "Update Order")]
        public async Task<OrderDTO> Update(UpdateOrderDTO Order)
        {
            UserDTO self = await GetLoggedUser();

            if (self == null || self.Role != (int)Role.Admin)
            {
                return null;
            }
            return await _OrderBusiness.Update(Order, self);
        }

        [HttpDelete("{id}", Name = "Delete Order")]
        public async Task Delete(int id)
        {
            UserDTO self = await GetLoggedUser();

            if (self == null || self.Role != (int)Role.Admin)
            {
                return;
            }
            await _OrderBusiness.Delete(id);
        }

        private async Task<UserDTO> GetLoggedUser()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string b64 = token.ToString().Split(" ")[1];
            string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(b64));
            string email = decoded.Split(":")[0];
            string pass = decoded.Split(":")[1];

            if (!(await _userBusiness.Login(email, pass)))
            {
                return null;
            }
            return await _userBusiness.GetSelfUser(email);
        }
    }
}
