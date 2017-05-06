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
		// 表示用のリストではなく、オリジナルデータのリスト
		List<LogRowData> _origDataList = new List<LogRowData>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ListViewCtrlViewModel()
		{
			IsAutoScroll = true;
		}

		readonly DispatcherCollection<ListViewConverter> _viewItems = new DispatcherCollection<ListViewConverter>(DispatcherHelper.UIDispatcher);
		public DispatcherCollection<ListViewConverter> ViewItems => _viewItems;

		public bool IsAutoScroll { get; set; }

		private ViewModelCommand _clearLogCommand;
		public ViewModelCommand ClearLogCommand => (_clearLogCommand = _clearLogCommand ?? new ViewModelCommand(()=> { _viewItems.Clear(); _origDataList.Clear(); }));

		private bool _isHex;
		public bool IsHex
		{
			get { return _isHex; }
			set
			{
				_isHex = value;
				// 表示しなおし
				RedrawListView();
			}
		}

		private bool _isSelected;
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				_isSelected = value;
				// 表示しなおし
				RedrawListView();
			}
		}

		/// <summary>
		/// ログ追加
		/// </summary>
		/// <param name="rowData"></param>
		public void AddLog(LogRowData rowData)
		{
			if(rowData != null)
			{
				_origDataList?.Add(rowData);
				AddLogRowDataOnViewItems(rowData);
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
		}

		private void RedrawListView()
		{
			// 表示上のログをいったんすべて削除
			_viewItems.Clear();

			// 追加しなおし
			foreach (var data in _origDataList)
			{
				AddLogRowDataOnViewItems(data);
			}
		}

		/// <summary>
		/// 表示用ログリストにデータを追加
		/// </summary>
		/// <param name="rowData"></param>
		private void AddLogRowDataOnViewItems(LogRowData rowData)
		{
			// 状態問い合わせとイベントなしの状態通知は表示しない
			if (_isSelected) {
				// 簡易的にrecvのみ表示としておく
				if (rowData.LogType != ListViewLogType.recv)
				{
					return;
				}
			}

			var item = new ListViewConverter(rowData);
			item.LogBytesToString = _isHex ? (Func<byte[], string>)BytesToStringHex : BytesToStringAscii;
			_viewItems?.Add(item);
		}

		/// <summary>
		/// ログを１６進数で出力する
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string BytesToStringHex(byte[] bytes)
		{
			string ret = "";

			foreach (var val in bytes)
			{
				int integral = Convert.ToInt32(val);
				ret += String.Format("{0:X2} ", integral);
			}

			return ret;
		}

		/// <summary>
		/// ログをASCII文字で出力する
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string BytesToStringAscii(byte[] bytes)
		{
			string ret = "";

			foreach (var val in bytes)
			{
				char charactor = Convert.ToChar(val);
				if (!Char.IsControl(charactor))
				{
					ret += charactor + " ";
				}
				else
				{
					ret += String.Format("{0:X2} ", (int)charactor) + " ";
				}
			}

			return ret;
		}
	}
}
