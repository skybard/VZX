using System.Collections.Generic;

namespace VZX.MvcCoreUI.Models
{
    public static class FakeRepository<T>
    {
        private static readonly List<T> _entities = new List<T>();

        public static List<T> GetAll() => _entities;

        public static void Add(T entity) => _entities.Add(entity);
    }
}
