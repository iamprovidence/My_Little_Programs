using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using AdvancedDictionary.AdditionalClasses;

namespace AdvancedDictionary.View
{
    internal class VerbsView
    {
        // FIELDS
        Model.Words verbs;
        List<Model.Word> viewItems;
        FilterWordFunctor filter;

        // PROPERTIES
        public int Count => viewItems.Count;
        public Model.Words Verbs => verbs;
        public FilterWordFunctor Filter => filter;
        // INDEXERS
        public Model.Word this[int index] => viewItems[index];
        // EVENT
        public event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;
        // CONSTRUCTORS
        public VerbsView(Model.Words verbs)
        {
            this.verbs = verbs;
            this.viewItems = new List<Model.Word>();
            filter = new FilterWordFunctor();
            verbs.CollectionChanged += DataCollectionChange;
            Build();
        }

        

        // METHODS
        public void Build()
        {
            viewItems.Clear();
            viewItems.AddRange(verbs);
        }
        public void SortBy(VerbComparer.ComparerType comparer)
        {
            viewItems.Sort(new VerbComparer(comparer));
        }

        public void FilterBy(Predicate<Model.Word> predicate)
        {
            viewItems.Clear();
            foreach(Model.Word verb in verbs)
            {
                if(predicate(verb))
                {
                    viewItems.Add(verb);
                }
            }
        }
        // EVENT
        private void DataCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            Build();
            OnCollectionChange(e);
        }
        protected void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(null, e);
        }
    }
}
