using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int[] ProductList { get; set; }
        public int OrderType { get; set; }
        public int OrderState { get; set; }
    }
}
