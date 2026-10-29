

namespace FitnessTracker.Domain
{
    public class InMemoryRepository<T>
    {
        private readonly List<T> _items = new();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public IReadOnlyCollection<T> GetAll() => _items.AsReadOnly();
    }
}
