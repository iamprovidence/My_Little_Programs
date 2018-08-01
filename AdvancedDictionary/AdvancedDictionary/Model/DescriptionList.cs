using System;
using System.Linq;
using System.Collections.Generic;
using AdvancedDictionary.AdditionalClasses;
using static AdvancedDictionary.AdditionalClasses.Constants;

namespace AdvancedDictionary.Model
{
    [System.Serializable]
    internal abstract class DescriptionList<T> : Interfaces.IDescriptionWordList<T>, Interfaces.ICloneable<DescriptionList<T>> where T: ICloneable
    {
        // FIELDS
        protected List<T> picked;
        protected List<T> unpicked;
        protected string separator;
        // PROPERTIES
        public List<T> Picked => picked;
        public List<T> Unpicked => unpicked;
        public int TotalAmount => picked.Count + unpicked.Count;
        // CONSTRUCTORS
        public DescriptionList() 
            : this(Enumerable.Empty<T>(), Enumerable.Empty<T>())
        { }
        public DescriptionList(IEnumerable<T> range)
            : this(range, Enumerable.Empty<T>())
        { }
        public DescriptionList(IEnumerable<T> rangeUnpicked, IEnumerable<T> rangePicked)
        {
            picked = new List<T>(rangePicked);
            unpicked = new List<T>(rangeUnpicked);
            separator = " ; ";
        }
        public DescriptionList(List<T> Unpicked, List<T> Picked)
        {
            picked = Picked;
            unpicked = Unpicked;
            separator = " ; ";
        }
        // METHODS
        public void Pick(int index)
        {
            picked.Add(unpicked[index]);
            unpicked.RemoveAt(index);
        }
        public void Unpick(int index)
        {
            unpicked.Add(picked[index]);
            picked.RemoveAt(index);
        }
        public void Add(T item)
        {
            unpicked.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            unpicked.AddRange(items);
        }
        public void Replace(T oldValue, T newValue)
        {
            int index = unpicked.IndexOf(oldValue);
            if (index != WRONG_INDEX)
            {
                unpicked[index] = newValue;
                return;
            }
            index = picked.IndexOf(oldValue);
            if (index != WRONG_INDEX)
            {
                picked[index] = newValue;
            }
        }

        public void Remove(T item)
        {
            if(unpicked.Remove(item) == false)
            {
                picked.Remove(item);
            }
        }
        public void Clear()
        {
            picked.Clear();
            unpicked.Clear();
        }
        public string PickedStr()
        {
            return string.Join(separator: " ; ", values: picked);
        }
        public void SetSeparator(string separator)
        {
            this.separator = separator;
        }

        public DescriptionList<T> Clone()
        {
            DescriptionList<T> clone = (DescriptionList<T>)this.MemberwiseClone();

            clone.picked = this.picked.Clone() as List<T>;
            clone.unpicked = this.unpicked.Clone() as List<T>;

            return clone;
        }

    }
}
