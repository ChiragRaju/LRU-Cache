using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU_Cache
{
    public class LRUCache<K,V>
    {
        private readonly int _capacity;
        private readonly Dictionary<K, Node<K, V>> _cache;
        private readonly Node<K, V>? _head;
        private readonly Node<K, V>? _tail;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cache = new Dictionary<K, Node<K, V>>(capacity);

            _head = new Node<K, V>(default!, default!);
            _tail = new Node<K, V>(default!, default!);
            _head.Next = _tail;
            _tail.Prev = _head;

        }
        private void AddToHead(Node<K, V> node)
        {
            node.Prev = _head;
            node.Next = _head.Next;
            _head.Next.Prev = node;
            _head.Next = node;
        }
        private void RemoveNode(Node<K, V> node)
        {
           var prevNode = node.Prev;
            var nextNode = node.Next;
            prevNode.Next = nextNode;
            nextNode.Prev = prevNode;
        }

        private void MoveToHead(Node<K, V> node)
        {
            RemoveNode(node);
            AddToHead(node);
        }

        private Node<K,V> RemoveTail()
        {
            var tailNode = _tail.Prev;
            RemoveNode(tailNode);
            return tailNode;
        }

        public V GetV(K key)
        {
            if (!_cache.TryGetValue(key, out var node))
            {
                return default; //return null if key not found
            }
            MoveToHead(node); // Move the accessed node to the head
            return node.Value;
        }

        public void Put(K key, V value)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                node.Value = value; // Update the value
                MoveToHead(node);
            }
            else
            {
                var newNode = new Node<K, V>(key, value);
                _cache[key] = newNode;
                AddToHead(newNode);

                if (_cache.Count > _capacity)
                {
                    var removedNode = RemoveTail();
                    _cache.Remove(removedNode.Key);
                }
            }
        }


    }
}
