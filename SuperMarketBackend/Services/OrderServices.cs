using SuperMarketBackend.Data;

namespace SuperMarketBackend.Services
{
    public class OrderServices
    {
        private readonly SuperMrktDbContext _context;
        public OrderServices()
        {
            _context = new SuperMrktDbContext();
        }

        public Order? AddOrder(Order order)
        {
            if(!order.OrderItems.Any())
                return new Order();
            order.OrderDate = DateTime.Now.ToString();
            foreach( var item in order.OrderItems)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                if(product != null)
                {
                    if (item.Quantity <= product.StockQuantity)
                        product.StockQuantity -= item.Quantity;
                    else
                        return null;
                }
                _context.Products.Update(product);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();                                   
            return _context.Orders.OrderBy(o => o.OrderId).LastOrDefault();
        }

        public bool DeleteOrder(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId && !x.IsDeleted);
            if(order == null)
                return false;
            order.IsDeleted = true;
            var orderItems = _context.OrderItems.Where(oi => oi.OrderId == order.OrderId && !oi.IsDeleted).ToList();
            foreach (var item in orderItems)
                item.IsDeleted = true;
            _context.OrderItems.UpdateRange(orderItems);
            order.OrderItems.Clear();
            _context.Orders.Update(order);
            _context.SaveChanges();
            return true;
        }

        public Order? GetOrderDetails(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId && !x.IsDeleted);
            if (order == null)
                return null;            
            var orderItems = _context.OrderItems.Where(oi => oi.OrderId == order.OrderId && !oi.IsDeleted).ToList();
            order.OrderItems = orderItems;
            return order;
        }

        public List<Order>? GetOrders()
        {
            var orders = new List<Order>();
            if (_context.Orders.Any())
            {
                orders = _context.Orders.Where(o => !o.IsDeleted).ToList();
                foreach (var o in orders)
                {
                    o.OrderItems = _context.OrderItems.Where(oi => oi.OrderId == o.OrderId && !oi.IsDeleted).ToList();
                }
                return orders;
            }
            else return orders;
        }

        public bool ChangeOrderStatus(int id, int status)
        {
            var order = GetOrderDetails(id);
            if (order == null)
                return false;
            order.Status = status;
            _context.Update(order);
            _context.SaveChanges();
            return true;
        }

        public int GetInvNo()
        {
            var lastOrder = _context.Orders.OrderBy(o => o.OrderId).LastOrDefault();
            if(lastOrder == null)
                return 1;
            return (lastOrder.OrderId + 1);
        }
    }
}
