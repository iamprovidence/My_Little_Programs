using System.Linq;
using System.Collections.Generic;

namespace SnippetBuilder
{
    class ListLiteral : List<Literal>
    {
        public Literal ContainsReturn(string id)
        {
            foreach (Literal literal in this)
            {
                if (literal.ID == id)
                {
                    return literal;
                }
            }
            return null;
        }
        public bool Contains(string id)
        {
            foreach (Literal literal in this)
            {
                if (literal.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        public Literal this[string id]
        {
            get
            {
                foreach(Literal literal in this)
                {
                    if(literal.ID == id)
                    {
                        return literal;
                    }
                }
                return null;
            }
        }
    }
    class MultiMap<TKey, TValue>
    {
        // FIELDS
        Dictionary<TKey, SortedSet<TValue>> basis;
        int count;
        int keyCount;
        int valueCount;

        // CONSTRUCTORS
        public MultiMap()
        {
            basis = new Dictionary<TKey, SortedSet<TValue>>();
            count = valueCount = keyCount = 0;
        }
        // PROPERTIES

        public int Count => count;
        public int KeyCount => keyCount;
        public int ValueCount => valueCount;
        public List<TKey> Keys => basis.Keys.ToList();
        public List<TValue> Values
        {
            get
            {
                List<TValue> list = new List<TValue>(valueCount);
                foreach (SortedSet<TValue> set in basis.Values)
                {
                    list.AddRange(set);
                }
                return list.Distinct().ToList();
            }
        }
        public List<KeyValuePair<TKey, TValue>> Reflection
        {
            get
            {
                List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>(valueCount);
                foreach(KeyValuePair<TKey, SortedSet<TValue>> node in basis)
                {
                    foreach (TValue value in node.Value)
                    {
                        list.Add(new KeyValuePair<TKey, TValue>(node.Key, value));
                    }
                }
                return list;
            }

        }
        // METHODS
        public bool ContainsValue(TValue item)
        {
            foreach(SortedSet<TValue> set in basis.Values)
            {
                if(set.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
        public bool ContainsKey(TKey key)
        {
            return basis.ContainsKey(key);
        }
        public bool IsEmpty()
        {
            return count == 0;
        }
        public MultiMap<TKey, TValue> Add(TKey key, TValue value)
        {
            this[key] = value;
            return this;
        } 
        
        // INDEXERS
        public TValue this[TKey key]
        {
            set
            {
                if(!basis.ContainsKey(key))
                {
                    basis.Add(key, new SortedSet<TValue>(new TValue[] { value }));
                    ++count;
                    ++keyCount;
                    ++valueCount;
                }
                else
                {
                    basis[key].Add(value);
                    ++count;
                    ++valueCount;
                }
            }
        }
    }
}
