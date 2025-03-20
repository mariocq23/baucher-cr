using context.ordenes;

namespace data
{
    public class OrdenRepository
    {
        private static readonly List<Orden> _orders = new List<Orden>();
        private static int _nextId = 1;

        public IEnumerable<Orden> GetAll()
        {
            return _orders;
        }

        public Orden GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void Add(Orden order)
        {
            order.Id = _nextId++;
            _orders.Add(order);
        }

        public void Update(Orden updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
            if (existingOrder != null)
            {
                _orders.Remove(existingOrder);
                _orders.Add(updatedOrder);
            }
        }

        public void Delete(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.Id == id);
            if (orderToRemove != null)
            {
                _orders.Remove(orderToRemove);
            }
        }
    }
}
