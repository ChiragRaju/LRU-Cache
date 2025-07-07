using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU_Cache
{
    public class Node<K, V> where K : notnull
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node<K, V>? Next { get; set; }
        public Node<K, V>? Prev { get; set; }
        public Node(K key,V value )
        {
            Key = key;
            Value = value;

        }
    }
}
