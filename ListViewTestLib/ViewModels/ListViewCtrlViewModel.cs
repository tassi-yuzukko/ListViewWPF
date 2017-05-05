using ListViewTestLib.Models;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ListViewTestLib.ViewModels
{
	public class ListViewCtrlViewModel : ViewModel
	{
		readonly DispatcherCollection<ListViewItems> _items = new DispatcherCollection<ListViewItems>(DispatcherHelper.UIDispatcher);
		public DispatcherCollection<ListViewItems> Items { get { return _items; } }

		public ListViewCtrlViewModel()
		{
		}

		public void AddLog(ListViewItems rowData)
		{
			if(rowData != null)
			{
				Items?.Add(rowData);
			}
		}
	}
}
