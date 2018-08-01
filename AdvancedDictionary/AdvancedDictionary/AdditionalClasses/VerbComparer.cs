using System;
using System.Collections.Generic;

namespace AdvancedDictionary.AdditionalClasses
{
    internal class VerbComparer : IComparer<Model.Word>
    {
        public enum ComparerType { Text }
        private ComparerType comparer;
        public VerbComparer(ComparerType comparer)
        {
            this.comparer = comparer;
        }

        public int Compare(Model.Word x, Model.Word y)
        {
            switch (comparer)
            {
                case ComparerType.Text:   return x.Text.CompareTo(y.Text);
                default:    throw new ArgumentException(String.Concat("Can not compare in this way.\nComparer " ,comparer , " is not valid."));
            }
        }
    }
}
