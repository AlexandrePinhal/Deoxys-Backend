using AutoMapper;
using Common.DTO.Order;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(x => x.User, x => x.MapFrom(y => y.User.Id))
                .ForMember(x => x.ProductList, x => x.MapFrom(y => y.ProductList.Length > 0 ? y.ProductList.Select(z => z.Id).ToArray() : new int[0]))
                .AfterMap((src, dest) =>
                {
                    dest.ProductList = src.ProductListId.Split(',').Select(x => int.Parse(x)).ToArray();
                })
                .ReverseMap();
            CreateMap<OrderDTO, Order>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.User, opt => opt.Ignore())
                .ForMember(des => des.ProductList, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.User = new User() { Id = src.User };
                    dest.ProductListId = String.Join(",", src.ProductList);
                });


            CreateMap<Order, CreateOrderDTO>();
            CreateMap<CreateOrderDTO, Order>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.User, opt => opt.Ignore())
                .ForMember(des => des.ProductList, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.ProductListId = String.Join(",", src.ProductList);
                });

            CreateMap<Order, UpdateOrderDTO>();
            CreateMap<UpdateOrderDTO, Order>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.User, opt => opt.Ignore())
                .ForMember(des => des.ProductList, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.ProductListId = String.Join(",", src.ProductList);
                });
        }
    }
}
