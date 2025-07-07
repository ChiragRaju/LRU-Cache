using System;

namespace LRU_Cache
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cache = new LRUCache<int, string>(3);

            cache.Put(1, "Value 1");
            cache.Put(2, "Value 2");
            cache.Put(3, "Value 3");

            Console.WriteLine(cache.GetV(1)); // Output: Value 1
            Console.WriteLine(cache.GetV(2)); // Output: Value 2

            cache.Put(4, "Value 4");

            Console.WriteLine(cache.GetV(3)); // Output: null (as it has been removed)
            Console.WriteLine(cache.GetV(4)); // Output: Value 4

            cache.Put(2, "Updated Value 2");

            Console.WriteLine(cache.GetV(1)); // Output: Value 1
            Console.WriteLine(cache.GetV(2));
        }
    }
}
