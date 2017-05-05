using ListViewTestLib.Models;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ListViewTestLib.ViewModels
{
	public class ListViewCtrlViewModel : ViewModel
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ListViewCtrlViewModel()
		{
			IsAutoScroll = true;
		}

		readonly DispatcherCollection<ListViewItems> _items = new DispatcherCollection<ListViewItems>(DispatcherHelper.UIDispatcher);
		public DispatcherCollection<ListViewItems> Items { get { return _items; } }

		public bool IsAutoScroll { get; set; } 

		/// <summary>
		/// ログ追加
		/// </summary>
		/// <param name="rowData"></param>
		public void AddLog(ListViewItems rowData)
		{
			if(rowData != null)
			{
				Items?.Add(rowData);
			}
		}

		/// <summary>
		/// 最新行表示
		/// </summary>
		/// <param name="lv"></param>
		public void AutoScroll(ListView lv)
		{
			if (IsAutoScroll)
			{
				lv?.ScrollIntoView(lv.Items[lv.Items.Count - 1]);
			}
			//foreach (GridViewColumn column in (lv.View as GridView).Columns)
			//{
			//	column.Width = 0;
			//	column.Width = double.NaN;
			//}
		}
	}
}
