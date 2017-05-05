using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ListViewTestLib.Models;

namespace ListViewTestLib.Views.StyleSelectors
{
	public class ListViewItemStyleSelector : StyleSelector
	{
		public override Style SelectStyle(object item, DependencyObject container)
		{
			var st = new Style();
			st.TargetType = typeof(ListViewItem);

			var backGroundSetter = new Setter();
			backGroundSetter.Property = Control.BackgroundProperty;
			backGroundSetter.Value = (item as IMyListViewItems).GetListViewRowColor();

			st.Setters.Add(backGroundSetter);
			return st;
		}
	}
}
