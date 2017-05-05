using ListViewTestLib.Models;
using Livet;
using Livet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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

		readonly DispatcherCollection<ListViewConverter> _items = new DispatcherCollection<ListViewConverter>(DispatcherHelper.UIDispatcher);
		public DispatcherCollection<ListViewConverter> Items { get { return _items; } }

		public bool IsAutoScroll { get; set; }

		private ViewModelCommand _clearLogCommand;
		public ViewModelCommand ClearLogCommand => (_clearLogCommand = _clearLogCommand ?? new ViewModelCommand(()=> { _items.Clear(); }));

		public bool IsHex { get; set; }

		/// <summary>
		/// ログ追加
		/// </summary>
		/// <param name="rowData"></param>
		public void AddLog(LogRowData rowData)
		{
			if(rowData != null)
			{
				_items?.Add(new ListViewConverter(rowData, IsHex));
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
				if(lv?.Items.Count > 0)
				{
					lv?.ScrollIntoView(lv.Items[lv.Items.Count - 1]);
				}
			}
			//foreach (GridViewColumn column in (lv.View as GridView).Columns)
			//{
			//	column.Width = 0;
			//	column.Width = double.NaN;
			//}
		}
	}
}
