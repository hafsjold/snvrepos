using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BJDoc3
{
    public class MyStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var itemsControl = ItemsControl.ItemsControlFromItemContainer(container);
            var index = itemsControl.ItemContainerGenerator.IndexFromContainer(container);

            if (index == 0)
                return (Style)itemsControl.FindResource("FirstItemStyle");
            if (index == 1)
                return (Style)itemsControl.FindResource("SecondItemStyle");

            return base.SelectStyle(item, container);
        }
    }
}
