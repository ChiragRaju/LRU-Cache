using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU_Cache
{
    public class LRUCache<K, V>
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
            //example HEAD <-> A <-> B <-> TAIL
            //Now, we want to insert a new node X right after HEAD, like this:
            //HEAD <-> X <-> A <-> B <-> TAIL
            node.Prev = _head; //Head<-X
            node.Next = _head.Next;// X->A
            _head.Next.Prev = node;// A<-X
            _head.Next = node;// Head->X
        }

        private void RemoveNode(Node<K, V> node)
        {
            //example HEAD <-> A <-> B <-> TAIL
            // Now, we want to remove node B, like this:
            var prevNode = node.Prev;// A
            var nextNode = node.Next;// TAIL
            prevNode.Next = nextNode;// A->TAIL
            nextNode.Prev = prevNode;// TAIL->A
        }

        private void MoveToHead(Node<K, V> node)
        {
            RemoveNode(node);
            AddToHead(node);
        }

        private Node<K, V> RemoveTail()
        {
            try
            {
                var tailNode = _tail.Prev;// Get the last node before the tail
                RemoveNode(tailNode);// Remove it from the linked list
                return tailNode;// Return the removed node
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RemoveTail: {ex.Message}");
                throw;
            }
        }

        public V GetV(K key)
        {
            try
            {
                if (!_cache.TryGetValue(key, out var node))
                {
                    return default; // return null if key not found
                }
                MoveToHead(node); // Move the accessed node to the head
                return node.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetV: {ex.Message}");
                return default;
            }
        }

        public void Put(K key, V value)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Put: {ex.Message}");
                // Optionally rethrow or handle as needed
            }
        }
    }
}
