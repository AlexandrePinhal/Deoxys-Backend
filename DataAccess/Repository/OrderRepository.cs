using Common.Model;
using DataAccess.dbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private NegoSudDBContext _context;

        public OrderRepository(NegoSudDBContext context)
        {
            _context = context;
        }
        public void Add(Order user)
        {
            _context.Orders.Add(user);
        }

        public void Delete(Order user)
        {
            _context.Orders.Remove(user);
        }

        public Order Get(int id)
        {
            return _context.Orders.First(usr => usr.Id == id);
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void Update(Order user)
        {
            _context.Orders.Update(user);
        }
    }
}
