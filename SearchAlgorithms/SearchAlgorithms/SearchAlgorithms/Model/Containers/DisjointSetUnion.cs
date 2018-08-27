using System.Collections.Generic;

namespace SearchAlgorithms.Model.Containers
{
    class DisjointSetUnion<T>
    {
        // INNER CLASSES
        class TreeNode
        {
            public T Data { get; set; }
            public uint Size { get; set; }
            public uint Rank { get; set; }

            public TreeNode(T data, uint Size, uint Rank)
            {
                this.Data = data;
                this.Size = Size;
                this.Rank = Rank;
            }
        }

        // FIELDS
        Dictionary<T, TreeNode> parent;
        uint setsAmount;
        uint elementsAmount;

        // CONSTRUCTORS
        public DisjointSetUnion()
        {
            parent = new Dictionary<T, TreeNode>();
            setsAmount = 0;
            elementsAmount = 0;
        }

        // PROPERTIES
        public uint SetsAmount => setsAmount;
        public uint ElementsAmount => elementsAmount;

        // METHODS
        public void MakeSet(T item)
        {
            parent.Add(key: item, value: new TreeNode(data: item, Rank: 0, Size: 1));

            ++setsAmount;
            ++elementsAmount;
        }
        public T FindSetLeader(T item)
        {
            if (item.Equals(parent[item].Data))
            {
                return item;
            }
            parent[item].Data = FindSetLeader(parent[item].Data);
            return parent[item].Data;
        }

        public bool UnionSets(T a, T b)
        {
            a = FindSetLeader(a);
            b = FindSetLeader(b);

            if (!a.Equals(b))
            {
                if (parent[a].Rank < parent[b].Rank)
                {
                    T temp = a;
                    a = b;
                    b = temp;
                }
                parent[b] = parent[a];
                if (parent[a].Rank == parent[b].Rank)
                {
                    ++parent[a].Rank;
                }
                parent[a].Size += parent[b].Size;
                --setsAmount;

                return true;
            }
            return false;
        }
        public void Clear()
        {
            parent.Clear();

            elementsAmount = 0;
            setsAmount = 0;
        }
        
        public bool InDifferentSets(T a, T b)
        {
            return !FindSetLeader(a).Equals(FindSetLeader(b));
        }
    }
}
