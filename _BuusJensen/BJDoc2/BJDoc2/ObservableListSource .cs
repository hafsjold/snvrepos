/*
In bbBJDoc.tt:
Find and replace the two occurrences of “ICollection” with “ObservableListSource”. These are located at approximately lines 296 and 484.
Find and replace the first occurrence of “HashSet” with “ObservableListSource”. This occurrence is located at approximately line 50. Do not replace the second occurrence of HashSet found later in the code.
*/
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Data.Entity;


namespace BJDoc2
{
    public class ObservableListSource<T> : ObservableCollection<T>, IListSource
        where T : class
    {
        private IBindingList _bindingList;

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList()
        {
            return _bindingList ?? (_bindingList = this.ToBindingList());
        }
    }

}
