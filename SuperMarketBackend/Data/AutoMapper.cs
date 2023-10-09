using AutoMapper;
using SuperMarketBackend.DTO;

namespace SuperMarketBackend.Data
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();            
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO , Product>();
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductCategoryDTO, ProductCategory>();            
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
        }
    }
}
